using Aqua.EnumerableExtensions;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.MultiTenancy.Internal;
using DevExpress.ExpressApp.Updating;
using DevExpress.Persistent.BaseImpl.EF.MultiTenancy;
using Microsoft.Extensions.DependencyInjection;
using OutlookInspired.Module.BusinessObjects;
using OutlookInspired.Module.Features.ViewFilter;
using OutlookInspired.Module.Services.Internal;

namespace OutlookInspired.Module.DatabaseUpdate;

public class Updater(IObjectSpace objectSpace, Version currentDBVersion) : ModuleUpdater(objectSpace, currentDBVersion){
    

    public override void UpdateDatabaseAfterUpdateSchema() {
        base.UpdateDatabaseAfterUpdateSchema();
        if (!ObjectSpace.CanInstantiate(typeof(ApplicationUser))) return;
        if (!ObjectSpace.CanInstantiate(typeof(Tenant))) return;
        if (ObjectSpace.TenantName() == null) {
            CreateAdminObjects();
            CreateTenant("company1.com", "OutlookInspired_company1");
            CreateTenant("company2.com", "OutlookInspired_company2");
            ObjectSpace.CommitChanges();
        }
        else {
            var defaultRole = ObjectSpace.EnsureDefaultRole();
            CreateAdminObjects();
            if (ObjectSpace.ModifiedObjects.Any()){
                CreateDepartmentRoles();
                CreateViewFilters();
                ObjectSpace.CreateMailMergeTemplates();
                foreach (var employee in ObjectSpace.GetObjectsQuery<Employee>()){
                    var employeeName = employee.FirstName.ToLower().Concat(employee.LastName.ToLower().Take(1)).StringJoin("");
                    var userName = $"{employeeName}@{ObjectSpace.TenantName()}";
                    employee.User = ObjectSpace.EnsureUser(userName, user => user.Employee = employee);
                    employee.User.Roles.Add(defaultRole);
                    employee.User.Roles.Add(ObjectSpace.FindRole(employee.Department));    
                }
                
            }
            ObjectSpace.CommitChanges();
        }
    }

    private void CreateTenant(string tenantName, string databaseName) {
        var tenant = ObjectSpace.FirstOrDefault<Tenant>(t => t.Name == tenantName);
        if (tenant == null) {
            tenant = ObjectSpace.CreateObject<Tenant>();
            tenant.Name = tenantName;
            tenant.ConnectionString = $"Data Source=..\\\\..\\\\data\\\\{databaseName}.db";
        }
        ((TenantNameHelperBase)ObjectSpace.ServiceProvider.GetRequiredService<ITenantNameHelper>()).ClearTenantMapCache();
    }

    private void CreateDepartmentRoles(){
        foreach (var department in Enum.GetValues<EmployeeDepartment>()){
            ObjectSpace.EnsureRole(department);
        }
        
    }

    private void CreateAdminObjects() {
        var adminName = (ObjectSpace.TenantName() != null) ? $"Admin@{ObjectSpace.TenantName()}" : "Admin";
        ObjectSpace.EnsureUser(adminName).Roles.Add(ObjectSpace.EnsureRole("Administrators", isAdmin: true));
    }

    private void CreateViewFilters(){
        EmployeeFilters();
        CustomerFilters();
        ProductFilters();
        OrderFilters();
        DateFilters<Quote>(nameof(Quote.Date));
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
        if (ObjectSpace.TenantName() == null) return;

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
