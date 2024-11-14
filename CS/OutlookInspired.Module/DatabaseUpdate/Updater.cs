using System.Collections;
using System.Linq.Expressions;
using Aqua.EnumerableExtensions;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.MultiTenancy;
using DevExpress.ExpressApp.MultiTenancy.Internal;
using DevExpress.ExpressApp.Security;
using DevExpress.ExpressApp.Updating;
using DevExpress.Persistent.BaseImpl.EF;
using DevExpress.Persistent.BaseImpl.EF.MultiTenancy;
using DevExpress.Persistent.BaseImpl.EF.PermissionPolicy;
using Microsoft.Extensions.DependencyInjection;
using OutlookInspired.Module.BusinessObjects;
using OutlookInspired.Module.Features;
using static DevExpress.ExpressApp.Security.SecurityOperations;
using static DevExpress.ExpressApp.SystemModule.CurrentUserIdOperator;
using static DevExpress.Persistent.Base.SecurityPermissionState;
using static OutlookInspired.Module.ModelUpdaters.DashboardViewsModelUpdater;
using static OutlookInspired.Module.ModelUpdaters.NavigationItemsModelUpdater;
using static OutlookInspired.Module.OutlookInspiredModule;
namespace OutlookInspired.Module.DatabaseUpdate;

public class Updater(IObjectSpace objectSpace, Version currentDBVersion) : ModuleUpdater(objectSpace, currentDBVersion){
    public const string MailMergeOrder="Order";
    public const string MailMergeOrderItem="OrderItem";
    public const string FollowUp="FollowUp";
    public const string ProbationNotice="Probation Notice";
    public const string ServiceExcellence="Service Excellence";
    public const string ThankYouNote="Thank You Note";
    public const string WelcomeToDevAV="Welcome to DevAV";
    public const string MonthAward="Month Award";
    private static readonly Type[] CustomerTypes =[typeof(Customer), typeof(CustomerStore), typeof(CustomerEmployee), typeof(CustomerCommunication) ,typeof(Crest),typeof(Picture)];
    private static readonly Type[] EmployeeTypes =[typeof(Employee), typeof(EmployeeTask), typeof(Evaluation), typeof(Probation),typeof(Picture),typeof(TaskAttachedFile)];
    private static readonly Type[] QuoteTypes =[typeof(Quote), typeof(QuoteItem)];
    private static readonly Type[] OrderTypes =[typeof(Order), typeof(OrderItem)];
    const string DefaultRoleName = "Default";

    public override void UpdateDatabaseAfterUpdateSchema() {
        base.UpdateDatabaseAfterUpdateSchema();
        if (!ObjectSpace.CanInstantiate(typeof(ApplicationUser))) return;
        
        if (ObjectSpace.ServiceProvider.GetRequiredService<ITenantProvider>().TenantName == null) {
            if (!ObjectSpace.CanInstantiate(typeof(Tenant))) return;
            CreateAdminObjects();
            CreateTenant("company1.com", "OutlookInspired_company1");
            CreateTenant("company2.com", "OutlookInspired_company2");
            ObjectSpace.CommitChanges();
        }
        else {
            var defaultRole = EnsureDefaultRole();
            CreateAdminObjects();
            if (ObjectSpace.ModifiedObjects.Any()){
                var roles = CreateDepartmentRoles().ToArray();
                CreateViewFilters();
                CreateMailMergeTemplates();
                var users = ObjectSpace.GetObjectsQuery<ApplicationUser>().Where(user => user.Employee==null).ToArray();
                var employees = ObjectSpace.GetObjectsQuery<Employee>().Where(employee => employee.User==null).ToArray();
                for (var index = 0; index < employees.Length; index++){
                    var employee = employees[index];
                    employee.User = users[index];
                    employee.User.Employee = employee;
                    var employeeName = employee.FirstName.ToLower().Concat(employee.LastName.ToLower().Take(1)).StringJoin("");
                    var userName = $"{employeeName}@{ObjectSpace.ServiceProvider.GetRequiredService<ITenantProvider>().TenantName}";
                    employee.User.UserName = userName;
                    employee.User.Roles.Add(defaultRole);
                    employee.User.Roles.Add(roles.First(role => role.Name == employee.Department.ToString()));
                }
            }
            ObjectSpace.CommitChanges();
        }
    }

    void CreateMailMergeTemplates(){
        var items = new[]{
            (type: typeof(Order), name: FollowUp), (type: typeof(Order), name: MailMergeOrder),
            (type: typeof(OrderItem), name: MailMergeOrderItem),
            (type: typeof(Employee), name: ProbationNotice), (type: typeof(Employee), name: ServiceExcellence),
            (type: typeof(Employee), name: ThankYouNote), (type: typeof(Employee), name: WelcomeToDevAV),
            (type: typeof(Employee), name: MonthAward),
        };
        foreach (var item in items){
            var assembly = GetType().Assembly;
            var bytes = Bytes(assembly.GetManifestResourceStream(assembly.GetManifestResourceNames().First(s => s.Contains("MailMerge") && s.EndsWith($"{item.name}.docx"))));
            NewMailMergeData(item.name, item.type, bytes);
        }
    }
        
    byte[] Bytes( Stream stream){
        if (stream is MemoryStream memoryStream){
            return memoryStream.ToArray();
        }

        using var ms = new MemoryStream();
        stream.CopyTo(ms);
        return ms.ToArray();
    }

    void NewMailMergeData(string name, Type dataType, byte[] bytes){
        var richTextMailMergeData = ObjectSpace.CreateObject<RichTextMailMergeData>();
        richTextMailMergeData.Name = name;
        richTextMailMergeData.Template = bytes;
        richTextMailMergeData.DataType = dataType;
    }
    T EnsureObject<T>(Expression<Func<T, bool>> criteriaExpression = null, Action<T> initialize = null, Action<T> update = null,
        bool inTransaction = false) where T : class{
        var o = ObjectSpace.FirstOrDefault(criteriaExpression??(arg =>true) ,inTransaction);
        if (o != null) {
            update?.Invoke(o);
            return o;
        }
        var ensureObject = ObjectSpace.CreateObject<T>();
        initialize?.Invoke(ensureObject);
        update?.Invoke(ensureObject);
        return ensureObject;
    }

    ApplicationUser EnsureUser(string userName,Action<ApplicationUser> configure=null) 
        => EnsureObject<ApplicationUser>(u => u.UserName == userName, user => {
            user.UserName = userName;
            configure?.Invoke(user);
            ObjectSpace.CommitChanges();
            ((ISecurityUserWithLoginInfo)user).CreateUserLoginInfo(SecurityDefaults.PasswordAuthentication,
                ObjectSpace.GetKeyValueAsString(user));
        });

    PermissionPolicyRole EnsureRole(string roleName,Action<PermissionPolicyRole> initialize = null,bool isAdmin=false) 
        => EnsureObject<PermissionPolicyRole>(r => r.Name == "Administrators", role => {
            role.Name = roleName;
            role.IsAdministrative = isAdmin;
            initialize?.Invoke(role);
        });

    PermissionPolicyRole EnsureDefaultRole() 
        => EnsureRole(DefaultRoleName, defaultRole => {
            defaultRole.AddObjectPermissionFromLambda<ApplicationUser>(Read, cm => cm.ID == (Guid)CurrentUserId(), Allow);
            defaultRole.AddNavigationPermission($@"Application/NavigationItems/Items/Default/Items/{UserListView}", Allow);
            defaultRole.AddNavigationPermission($@"Application/NavigationItems/Items/Default/Items/{WelcomeDetailView}", Allow);
            defaultRole.AddMemberPermissionFromLambda<ApplicationUser>(Write, "ChangePasswordOnFirstLogon", cm => cm.ID == (Guid)CurrentUserId(), Allow);
            defaultRole.AddMemberPermissionFromLambda<ApplicationUser>(Write, "StoredPassword", cm => cm.ID == (Guid)CurrentUserId(), Allow);
            defaultRole.AddTypePermissionsRecursively<PermissionPolicyRole>(Read, Deny);
            defaultRole.AddTypePermissionsRecursively<ModelDifference>(ReadWriteAccess, Allow);
            defaultRole.AddTypePermissionsRecursively<ModelDifferenceAspect>(ReadWriteAccess, Allow);
            defaultRole.AddTypePermissionsRecursively<ModelDifference>(Create, Allow);
            defaultRole.AddTypePermissionsRecursively<ModelDifferenceAspect>(Create, Allow);
            defaultRole.AddTypePermissionsRecursively<ViewFilter>(CRUDAccess, Allow);
        });

    private void CreateTenant(string tenantName, string databaseName) {
        var tenant = ObjectSpace.FirstOrDefault<Tenant>(t => t.Name == tenantName);
        if (tenant == null) {
            tenant = ObjectSpace.CreateObject<Tenant>();
            tenant.Name = tenantName;
            tenant.ConnectionString = $"Data Source=..\\\\..\\\\data\\\\{databaseName}.db";
        }
        ((TenantNameHelperBase)ObjectSpace.ServiceProvider.GetRequiredService<ITenantNameHelper>()).ClearTenantMapCache();
    }

    private IEnumerable<PermissionPolicyRole> CreateDepartmentRoles(){
        foreach (var department in Enum.GetValues<EmployeeDepartment>()){
            yield return EnsureRole(department);
        }
    }
    
    PermissionPolicyRole EnsureRole(EmployeeDepartment department) 
        => EnsureRole(department.ToString(), role => {
            switch (department){
                case EmployeeDepartment.Sales:
                    AddSalesPermissions(role);
                    break;
                case EmployeeDepartment.Engineering:
                    AddEngineeringPermissions(role);
                    break;
                case EmployeeDepartment.Support:
                    AddSupportPermissions(role);
                    break;
                case EmployeeDepartment.Shipping:
                    AddShippingPermissions(role);
                    break;
                case EmployeeDepartment.HumanResources:
                    AddHRPermissions(role);
                    break;
                case EmployeeDepartment.Management:
                    AddManagementPermissions(role);
                    break;
                case EmployeeDepartment.IT:
                    AddITPermissions(role);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(department), department, null);
            }
        });


    private static void AddPermissions(PermissionPolicyRole role, IEnumerable<Type> crudTypes,
            IEnumerable<Type> readOnlyTypes, IEnumerable navigationIds){
            foreach (var type in crudTypes){
                role.AddTypePermission(type, CRUDAccess, Allow);
            }
            foreach (var type in readOnlyTypes){
                role.AddTypePermission(type, Read, Allow);
            }
            foreach (var navigationId in navigationIds){
                role.AddNavigationPermission($"Application/NavigationItems/Items/Default/Items/{navigationId}", Allow);
            }
    }

    private static void AddSalesPermissions(PermissionPolicyRole role){
        AddPermissions(role, new[]{ typeof(Product) }.Concat(OrderTypes).Concat(QuoteTypes).Concat(CustomerTypes),
            EmployeeTypes, new[]{ OrderListView, CustomerListView, ProductListView, Opportunities, EmployeeListView });
        role.AddTypePermission<RichTextMailMergeData>(FullAccess, Deny);
        role.AddObjectPermissionFromLambda<RichTextMailMergeData>(Read, data => new[]{ MailMergeOrder, MailMergeOrderItem, FollowUp, MonthAward, ServiceExcellence, ThankYouNote }.Contains(data.Name), Allow);
        role.AddTypePermission<ReportDataV2>(FullAccess, Deny);
        role.AddObjectPermissionFromLambda<ReportDataV2>(Read,
            data => new[]{ RevenueAnalysis, RevenueReport, Sales, Contacts, LocationsReport, OrdersReport, ProductProfile, TopSalesPerson
            }.Contains(data.DisplayName), Allow);
    }

    private void AddHRPermissions(PermissionPolicyRole role){
        AddPermissions(role, EmployeeTypes, CustomerTypes.Prepend(typeof(ApplicationUser)), new[]{ EmployeeListView, EvaluationListView });
        role.AddTypePermission<RichTextMailMergeData>(FullAccess, Deny);
        role.AddObjectPermissionFromLambda<RichTextMailMergeData>(Read, data
            => new[]{ MailMergeOrder, MailMergeOrderItem, ProbationNotice, WelcomeToDevAV }.Contains(data.Name), Allow);
        role.AddTypePermission<ReportDataV2>(FullAccess, Deny);
        role.AddObjectPermissionFromLambda<ReportDataV2>(Read, data => new[]{ Contacts, LocationsReport }.Contains(data.DisplayName), Allow);
    }
        
    private void AddManagementPermissions(PermissionPolicyRole role){
        AddPermissions(role, EmployeeTypes, CustomerTypes.Prepend(typeof(ApplicationUser)), new[]{ EmployeeListView, EvaluationListView, CustomerListView });
        role.AddTypePermission<RichTextMailMergeData>(FullAccess, Deny);
        role.AddObjectPermissionFromLambda<RichTextMailMergeData>(Read, data => new[]{ MailMergeOrder, MailMergeOrderItem, ServiceExcellence }.Contains(data.Name), Allow);
        role.AddTypePermission<ReportDataV2>(FullAccess, Deny);
        role.AddObjectPermissionFromLambda<ReportDataV2>(Read, data => new[]{ RevenueReport, Contacts, LocationsReport, TopSalesPerson }.Contains(data.DisplayName), Allow);
    }

    private void AddSupportPermissions( PermissionPolicyRole role){
        AddPermissions(role, QuoteTypes, CustomerTypes.Concat(OrderTypes), new[]{ CustomerListView, Opportunities });
        role.AddTypePermission<Employee>(FullAccess, Deny);
        role.AddObjectPermissionFromLambda<Employee>(Read, cm => cm.User.ID == (Guid)CurrentUserId(), Allow);
        role.AddTypePermission<RichTextMailMergeData>(FullAccess, Deny);
        role.AddObjectPermissionFromLambda<RichTextMailMergeData>(Read, data => new[]{ MailMergeOrder, MailMergeOrderItem, FollowUp, MonthAward, ServiceExcellence, ThankYouNote }
            .Contains(data.Name), Allow);
        role.AddTypePermission<ReportDataV2>(FullAccess, Deny);
        role.AddObjectPermissionFromLambda<ReportDataV2>(Read, data => new[]{ Sales, OrdersReport }.Contains(data.DisplayName), Allow);
    }

    private void AddITPermissions(PermissionPolicyRole role){
        AddPermissions(role, OrderTypes, CustomerTypes, new[]{ OrderListView, CustomerListView });
        role.AddTypePermission<Employee>(FullAccess, Deny);
        role.AddObjectPermissionFromLambda<Employee>(Read, cm => cm.User.ID == (Guid)CurrentUserId(), Allow);
        role.AddTypePermission<RichTextMailMergeData>(FullAccess, Deny);
        role.AddObjectPermissionFromLambda<RichTextMailMergeData>(Read, data => new[]{ MailMergeOrder, MailMergeOrderItem, FollowUp, MonthAward, ServiceExcellence }.Contains(data.Name), Allow);
        role.AddTypePermission<ReportDataV2>(FullAccess, Deny);
        role.AddObjectPermissionFromLambda<ReportDataV2>(Read, data => new[]{ OrdersReport, FedExGroundLabel }.Contains(data.DisplayName), Allow);
    }

    private void AddEngineeringPermissions( PermissionPolicyRole role){
        AddPermissions(role, EmployeeTypes, CustomerTypes.Prepend(typeof(ApplicationUser)), new[]{ EmployeeListView, CustomerListView });
        role.AddTypePermission<RichTextMailMergeData>(FullAccess, Deny);
        role.AddObjectPermissionFromLambda<RichTextMailMergeData>(Read, data
            => new[]{ MailMergeOrder, MailMergeOrderItem, ServiceExcellence, ProbationNotice, WelcomeToDevAV
            }.Contains(data.Name), Allow);
        role.AddTypePermission<ReportDataV2>(FullAccess, Deny);
        role.AddObjectPermissionFromLambda<ReportDataV2>(Read, data => new[]{ ProductProfile }.Contains(data.DisplayName), Allow);
    }

    private void AddShippingPermissions( PermissionPolicyRole role){
        AddPermissions(role, OrderTypes, CustomerTypes, new[]{ OrderListView, CustomerListView });
        role.AddTypePermission<Employee>(FullAccess, Deny);
        role.AddObjectPermissionFromLambda<Employee>(Read, cm => cm.User.ID == (Guid)CurrentUserId(), Allow);
        role.AddTypePermission<RichTextMailMergeData>(FullAccess, Deny);
        role.AddObjectPermissionFromLambda<RichTextMailMergeData>(Read, data
            => new[]{ MailMergeOrder, MailMergeOrderItem, FollowUp, MonthAward, ServiceExcellence }
                .Contains(data.Name), Allow);
        role.AddTypePermission<ReportDataV2>(FullAccess, Deny);
        role.AddObjectPermissionFromLambda<ReportDataV2>(Read, data
            => new[]{ OrdersReport, FedExGroundLabel }.Contains(data.DisplayName), Allow);
    }
    private void CreateAdminObjects() {
        var tenantName = ObjectSpace.ServiceProvider.GetRequiredService<ITenantProvider>().TenantName;
        var adminName = (tenantName != null) ? $"Admin@{tenantName}" : "Admin";
        EnsureUser(adminName).Roles.Add(EnsureRole("Administrators", isAdmin: true));
    }

    private void CreateViewFilters(){
        EmployeeFilters();
        CustomerFilters();
        ProductFilters();
        OrderFilters();
        DateFilters<QuoteAnalysis>(nameof(Quote.Date));
    }

    private void OrderFilters(){
        DateFilters<Order>(nameof(Order.OrderDate));
        var viewFilter = ObjectSpace.CreateObject<ViewFilter>();
        viewFilter.SetCriteria<Order>(order => order.PaymentTotal==0);
        viewFilter.Name = "Unpaid Orders";
        viewFilter = ObjectSpace.CreateObject<ViewFilter>();
        viewFilter.SetCriteria<Order>(order => order.RefundTotal==order.TotalAmount);
        viewFilter.Name = "Refunds";
        viewFilter = ObjectSpace.CreateObject<ViewFilter>();
        viewFilter.SetCriteria<Order>(order => order.TotalAmount>5000);
        viewFilter.Name = "Sales > $5000";
        viewFilter = ObjectSpace.CreateObject<ViewFilter>();
        viewFilter.SetCriteria<Order>(order => order.TotalAmount<5000);
        viewFilter.Name = "Sales < $5000";
        foreach (var name in new[]{ "Jim Packard", "Harv Mudd", "Clark Morgan" }){
            viewFilter = ObjectSpace.CreateObject<ViewFilter>();
            viewFilter.SetCriteria<Order>(order => order.Employee.FullName == name);
            viewFilter.Name = $"Sales by {name}";
        }
    }

    private void DateFilters<T>(string dateProperty) where T:IViewFilter{
        var viewFilter = ObjectSpace.CreateObject<ViewFilter>();
        viewFilter.SetCriteria<T>($"IsOutlookIntervalToday([{dateProperty}])");
        viewFilter.Name = "Today";
        viewFilter = ObjectSpace.CreateObject<ViewFilter>();
        viewFilter.SetCriteria<T>($"IsThisMonth([{dateProperty}])");
        viewFilter.Name = "This Month";
        viewFilter = ObjectSpace.CreateObject<ViewFilter>();
        viewFilter.SetCriteria<T>($"IsOutlookIntervalEarlierThisYear([{dateProperty}])");
        viewFilter.Name = "This Year";
    }

    private void ProductFilters(){
        foreach (var category in Enum.GetValues<ProductCategory>()){
            var viewFilter = ObjectSpace.CreateObject<ViewFilter>();
            viewFilter.SetCriteria<Product>(product => product.Category == category);
            viewFilter.Name = category.ToString();
            
        }
        var filter = ObjectSpace.CreateObject<ViewFilter>();
        filter.SetCriteria<Product>(product => !product.Available);
        filter.Name = "Discontinued";
        filter = ObjectSpace.CreateObject<ViewFilter>();
        filter.SetCriteria<Product>(product => product.CurrentInventory == 0);
        filter.Name = "Out Of Stock";
    }

    private void CustomerFilters(){
        foreach (var status in Enum.GetValues<CustomerStatus>()){
            var viewFilter = ObjectSpace.CreateObject<ViewFilter>();
            viewFilter.SetCriteria<Customer>(customer => customer.Status == status);
            viewFilter.Name = status.ToString();
            
        }
        var filter = ObjectSpace.CreateObject<ViewFilter>();
        filter.SetCriteria<Customer>(customer => customer.TotalEmployees > 10000);
        filter.Name = "Employess > 10000";
        filter = ObjectSpace.CreateObject<ViewFilter>();
        filter.SetCriteria<Customer>(customer => customer.TotalStores > 10);
        filter.Name = "Stores > 10 Location";
        filter = ObjectSpace.CreateObject<ViewFilter>();
        filter.SetCriteria<Customer>(customer => customer.AnnualRevenue > 100000000000);
        filter.Name = "Revenues > 100 Billion";
    }

    private void EmployeeFilters(){
        foreach (var status in Enum.GetValues<EmployeeStatus>()){
            var viewFilter = ObjectSpace.CreateObject<ViewFilter>();
            viewFilter.SetCriteria<Employee>(employee => employee.Status == status);
            viewFilter.Name = status.ToString();            
        }
    }

    public override void UpdateDatabaseBeforeUpdateSchema(){
        base.UpdateDatabaseBeforeUpdateSchema();
        if (ObjectSpace.ServiceProvider.GetRequiredService<ITenantProvider>().TenantName == null) return;

        var data = new[]{
            new{
                ParentTable = nameof(OutlookInspiredEFCoreDbContext.Orders), ChildTable = nameof(Order.OrderItems),
                DateField = nameof(Order.OrderDate), ForeignKeyField = nameof(OrderItem.OrderID),
                GroupField = nameof(OrderItem.ProductID)
            },
            new{
                ParentTable = nameof(OutlookInspiredEFCoreDbContext.Quotes), ChildTable = nameof(Quote.QuoteItems),
                DateField = nameof(Quote.Date), ForeignKeyField = nameof(QuoteItem.QuoteID),
                GroupField = nameof(QuoteItem.ProductId)
            }
        };
        foreach (var entity in data){
            ShiftDatesNearNow(entity.ParentTable, entity.ChildTable, entity.DateField, entity.ForeignKeyField,
                entity.GroupField);
        }
        
    }

    private void ShiftDatesNearNow(string parentTableName, string childTableName, string parentDateFieldName, string childForeignKeyFieldName, string groupingFieldName) {
        using var updateCommand = CreateCommand($@"
        -- Step 1: Calculate the number of days to shift (based on the most recent date in the child table)
        WITH DateShift AS (
            SELECT JULIANDAY('now') - JULIANDAY(MAX(agg.MostRecentDate)) AS DayDifference
            FROM (
                SELECT 
                    c.{groupingFieldName}, 
                    MAX(p.{parentDateFieldName}) AS MostRecentDate
                FROM {childTableName} c
                INNER JOIN {parentTableName} p ON c.{childForeignKeyFieldName} = p.Id
                GROUP BY c.{groupingFieldName}
            ) agg
        )
        
        -- Step 2: Update all parent table dates by shifting them relative to the calculated DayDifference
        UPDATE {parentTableName}
        SET {parentDateFieldName} = DATE(
            {parentDateFieldName}, 
            (SELECT '+' || CAST(DayDifference AS TEXT) || ' days' FROM DateShift)
        )
        WHERE EXISTS (
            SELECT 1
            FROM {childTableName} c
            WHERE {parentTableName}.Id = c.{childForeignKeyFieldName}
        );
    ");
        updateCommand.ExecuteNonQuery();
    }

}
