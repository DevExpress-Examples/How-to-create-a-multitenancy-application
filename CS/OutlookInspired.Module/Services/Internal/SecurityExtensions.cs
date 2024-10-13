using System.Linq.Expressions;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Security;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl.EF;
using DevExpress.Persistent.BaseImpl.EF.PermissionPolicy;
using OutlookInspired.Module.BusinessObjects;
using static DevExpress.ExpressApp.Security.SecurityOperations;
using static DevExpress.ExpressApp.SystemModule.CurrentUserIdOperator;
using static DevExpress.Persistent.Base.SecurityPermissionState;
using static OutlookInspired.Module.ModelUpdaters.DashboardViewsModelUpdater;
using static OutlookInspired.Module.ModelUpdaters.NavigationItemsModelUpdater;
using static OutlookInspired.Module.Services.Internal.MailMergeExtensions;
using static OutlookInspired.Module.Services.Internal.ReportsExtensions;


namespace OutlookInspired.Module.Services.Internal{
    static class SecurityExtensions{
        const string DefaultRoleName = "Default";
        private static readonly Type[] CustomerTypes = { typeof(Customer), typeof(CustomerStore), typeof(CustomerEmployee), typeof(CustomerCommunication) ,typeof(Crest),typeof(Picture)};
        private static readonly Type[] EmployeeTypes = { typeof(Employee), typeof(EmployeeTask), typeof(Evaluation), typeof(Probation),typeof(Picture),typeof(TaskAttachedFile)};
        private static readonly Type[] QuoteTypes = { typeof(Quote), typeof(QuoteItem)};
        private static readonly Type[] OrderTypes = { typeof(Order), typeof(OrderItem)};
        [Obsolete]
        public static bool CanRead(this XafApplication application,Type objectType) 
            => application.Security.IsGranted( new PermissionRequest(objectType, Read));

        public static bool CanRead<T>(this ActionBase action, Expression<Func<T, bool>> expression){
            var objectSpace = action.View()?.ObjectSpace;
            return objectSpace?.CanRead(expression, action.Application) ?? action.Application.CanRead(expression);
        }

        public static bool CanRead<T>(this XafApplication application, Expression<Func<T,bool>> expression){
            using var objectSpace = application.CreateObjectSpace(typeof(T));
            return objectSpace.CanRead( expression,application);
        }

        private static bool CanRead<T>(this IObjectSpace objectSpace, Expression<Func<T, bool>> expression, XafApplication application){
            var target = objectSpace.GetObjectsQuery<T>().FirstOrDefault(expression);
            return target != null && application.CanRead(objectSpace, typeof(T), target);
        }
        
        public static bool CanRead(this XafApplication application,IObjectSpace objectSpace,Type objectType,object targetObject) 
            => application.Security.IsGranted(new PermissionRequest(objectSpace, objectType, Read, targetObject));

        public static bool IsGranted(this ISecurityStrategyBase security, PermissionRequest permissionRequest) 
            => ((IRequestSecurity)security).IsGranted(permissionRequest);
        
        public static PermissionPolicyRole EnsureRole(this IObjectSpace objectSpace,string roleName,Action<PermissionPolicyRole> initialize = null,bool isAdmin=false) 
        => objectSpace.EnsureObject<PermissionPolicyRole>(r => r.Name == "Administrators", role => {
            role.Name = roleName;
            role.IsAdministrative = isAdmin;
            initialize?.Invoke(role);
        });
        public static PermissionPolicyRole EnsureRole(this IObjectSpace objectSpace,EmployeeDepartment department) 
            => objectSpace.EnsureRole(department.ToString(), role => {
                switch (department){
                    case EmployeeDepartment.Sales:
                        role.AddSalesPermissions();
                        break;
                    case EmployeeDepartment.Engineering:
                        role.AddEngineeringPermissions();
                        break;
                    case EmployeeDepartment.Support:
                        role.AddSupportPermissions();
                        break;
                    case EmployeeDepartment.Shipping:
                        role.AddShippingPermissions();
                        break;
                    case EmployeeDepartment.HumanResources:
                        role.AddHRPermissions();
                        break;
                    case EmployeeDepartment.Management:
                        role.AddManagementPermissions();
                        break;
                    case EmployeeDepartment.IT:
                        role.AddITPermissions();
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(department), department, null);
                }
            });

        
        private static void AddSalesPermissions(this PermissionPolicyRole role) 
            => new[]{ typeof(Product) }.Concat(OrderTypes).Concat(QuoteTypes).Concat(CustomerTypes).AddCRUDAccess(role)
                .Concat(EmployeeTypes.AddReadAccess(role)).To<string>()
                .Concat(new[]{ OrderListView, CustomerListView, ProductListView,Opportunities,EmployeeListView}.AddNavigationAccess(role))
                .Finally(() => {
                    role.AddMailMergePermission(data => new[]{ MailMergeOrder, MailMergeOrderItem, FollowUp, MonthAward, ServiceExcellence, ThankYouNote }.Contains(data.Name));
                    role.AddReportPermission(data => new[]{ RevenueAnalysis, RevenueReport, Sales, Contacts, LocationsReport, OrdersReport, ProductProfile, TopSalesPerson }.Contains(data.DisplayName));
                })
                .Enumerate();
        
        private static void AddHRPermissions(this PermissionPolicyRole role) 
            => EmployeeTypes.AddCRUDAccess(role)
                .Concat(CustomerTypes.Prepend(typeof(ApplicationUser)).AddReadAccess(role)).To<string>()
                .Concat(new[]{ EmployeeListView,EvaluationListView}.AddNavigationAccess(role))
                .Finally(() => {
                    role.AddMailMergePermission(data => new[]{ MailMergeOrder, MailMergeOrderItem, ProbationNotice,WelcomeToDevAV }.Contains(data.Name));
                    role.AddReportPermission(data => new[]{ Contacts, LocationsReport }.Contains(data.DisplayName));
                })
                .Enumerate();


        private static void AddManagementPermissions(this PermissionPolicyRole role) 
            => EmployeeTypes.AddCRUDAccess(role)
                .Concat(CustomerTypes.Prepend(typeof(ApplicationUser)).AddReadAccess(role)).To<string>()
                .Concat(new[]{ EmployeeListView,EvaluationListView,CustomerListView}.AddNavigationAccess(role))
                .Finally(() => {
                    role.AddMailMergePermission(data => new[]{ MailMergeOrder, MailMergeOrderItem, ServiceExcellence }.Contains(data.Name));
                    role.AddReportPermission(data => new[]{ RevenueReport, Contacts, LocationsReport, TopSalesPerson }.Contains(data.DisplayName));
                })
                .Enumerate();
        
        private static void AddSupportPermissions(this PermissionPolicyRole role) 
            => QuoteTypes.AddCRUDAccess(role)
                .Concat(CustomerTypes.Concat(OrderTypes).AddReadAccess(role)).To<string>()
                .Concat(new[]{CustomerListView, Opportunities}.AddNavigationAccess(role))
                .Finally(() => {
                    role.ReadCurrentEmployee();
                    role.AddMailMergePermission(data => new[]{ MailMergeOrder, MailMergeOrderItem, FollowUp, MonthAward, ServiceExcellence, ThankYouNote }.Contains(data.Name));
                    role.AddReportPermission(data => new[]{ Sales, OrdersReport }.Contains(data.DisplayName));
                })
                .Enumerate();

        private static void AddITPermissions(this PermissionPolicyRole role) 
            => OrderTypes.AddCRUDAccess(role)
                .Concat(CustomerTypes.AddReadAccess(role)).To<string>()
                .Concat(new[]{OrderListView,CustomerListView}.AddNavigationAccess(role))
                .Finally(() => {
                    role.ReadCurrentEmployee();
                    role.AddMailMergePermission(data => new[]{ MailMergeOrder, MailMergeOrderItem, FollowUp, MonthAward, ServiceExcellence }.Contains(data.Name));
                    role.AddReportPermission(data => new[]{ OrdersReport,FedExGroundLabel }.Contains(data.DisplayName));
                })
                .Enumerate();
        
        private static void AddEngineeringPermissions(this PermissionPolicyRole role) 
            => EmployeeTypes.AddCRUDAccess(role)
                .Concat(CustomerTypes.Prepend(typeof(ApplicationUser)).AddReadAccess(role)).To<string>()
                .Concat(new[]{ EmployeeListView,CustomerListView}.AddNavigationAccess(role))
                .Finally(() => {
                    role.AddMailMergePermission(data => new[]{ MailMergeOrder, MailMergeOrderItem,  ServiceExcellence, ProbationNotice,WelcomeToDevAV }.Contains(data.Name));
                    role.AddReportPermission(data => new[]{ ProductProfile }.Contains(data.DisplayName));
                })
                .Enumerate();

        private static void AddShippingPermissions(this PermissionPolicyRole role) 
            => OrderTypes.AddCRUDAccess(role)
                .Concat(CustomerTypes.AddReadAccess(role))
                .To<string>()
                .Concat(new[]{ OrderListView, CustomerListView }.AddNavigationAccess(role))
                .Finally(() => {
                    role.ReadCurrentEmployee();
                    role.AddMailMergePermission(data => new[]{ MailMergeOrder, MailMergeOrderItem, FollowUp, MonthAward, ServiceExcellence }.Contains(data.Name));
                    role.AddReportPermission(data => new[]{ OrdersReport, FedExGroundLabel }.Contains(data.DisplayName));
                })
                .Enumerate();

        private static void AddMailMergePermission(this PermissionPolicyRole role,Expression<Func<RichTextMailMergeData, bool>> lambda){
            role.AddTypePermission<RichTextMailMergeData>(FullAccess, Deny);
            role.AddObjectPermissionFromLambda(Read, lambda, Allow);
        }
        private static void AddReportPermission(this PermissionPolicyRole role,Expression<Func<ReportDataV2, bool>> lambda){
            role.AddTypePermission<ReportDataV2>(FullAccess, Deny);
            role.AddObjectPermissionFromLambda(Read, lambda, Allow);
        }
        
        private static void ReadCurrentEmployee(this PermissionPolicyRole role){
            role.AddTypePermission<Employee>(FullAccess, Deny);
            role.AddObjectPermissionFromLambda<Employee>(Read, cm => cm.User.ID == (Guid)CurrentUserId(), Allow);
        }
        
        private static IEnumerable<string> AddNavigationAccess(this IEnumerable<string> source, PermissionPolicyRole role,string rootItem="Default")
            => source.Do(navigationId => role.AddNavigationPermission($"Application/NavigationItems/Items/{rootItem}/Items/{navigationId}", Allow));
            
        private static IEnumerable<Type> AddCRUDAccess(this IEnumerable<Type> source,PermissionPolicyRole role,SecurityPermissionState? state=Allow) 
            => source.AddAccess( role,CRUDAccess,state);
        private static IEnumerable<Type> AddReadAccess(this IEnumerable<Type> source,PermissionPolicyRole role,SecurityPermissionState? state=Allow) 
            => source.AddAccess( role,Read,state);

        private static IEnumerable<Type> AddAccess(this IEnumerable<Type> source, PermissionPolicyRole role,string operation,SecurityPermissionState? state=Allow) 
            => source.Do(type => role.AddTypePermission(type, operation, state));

        public static PermissionPolicyRole EnsureDefaultRole(this IObjectSpace objectSpace) 
            => objectSpace.EnsureRole(DefaultRoleName, defaultRole => {
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
        
        public static ApplicationUser EnsureUser(this IObjectSpace objectSpace,string userName,Action<ApplicationUser> configure=null) 
            => objectSpace.EnsureObject<ApplicationUser>(u => u.UserName == userName, user => {
                user.UserName = userName;
                configure?.Invoke(user);
                objectSpace.CommitChanges();
                ((ISecurityUserWithLoginInfo)user).CreateUserLoginInfo(SecurityDefaults.PasswordAuthentication,
                    objectSpace.GetKeyValueAsString(user));
            });

        public static PermissionPolicyRole FindRole(this IObjectSpace objectSpace, EmployeeDepartment department)
            => objectSpace.GetObjectsQuery<PermissionPolicyRole>().First(role => role.Name == department.ToString());
         
    }
}