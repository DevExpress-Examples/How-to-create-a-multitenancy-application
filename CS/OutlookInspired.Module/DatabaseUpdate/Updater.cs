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

public class Updater : ModuleUpdater {
    public Updater(IObjectSpace objectSpace, Version currentDBVersion) :
        base(objectSpace, currentDBVersion){
    }

    public override void UpdateDatabaseBeforeUpdateSchema(){
        base.UpdateDatabaseBeforeUpdateSchema();
        if (ObjectSpace.TenantName() == null) return;
        new[]{ new { ParentTable = nameof(OutlookInspiredEFCoreDbContext.Orders), ChildTable = nameof(Order.OrderItems), DateField = nameof(Order.OrderDate), ForeignKeyField = nameof(OrderItem.OrderID), GroupField = nameof(OrderItem.ProductID) },
            new { ParentTable = nameof(OutlookInspiredEFCoreDbContext.Quotes), ChildTable = nameof(Quote.QuoteItems), DateField = nameof(Quote.Date), ForeignKeyField = nameof(QuoteItem.QuoteID), GroupField = nameof(QuoteItem.ProductId) }
        }.Do(entity => SynchronizeDates(entity.ParentTable, entity.ChildTable, entity.DateField, entity.ForeignKeyField, entity.GroupField)).Enumerate();
    }

    private void SynchronizeDates(string parentTableName, string childTableName, string parentDateFieldName, string childForeignKeyFieldName, string groupingFieldName) {
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
                ObjectSpace.GetObjectsQuery<Employee>().ToArray()
                    .Do(employee => {
                        var employeeName = employee.FirstName.ToLower().Concat(employee.LastName.ToLower().Take(1)).StringJoin("");
                        var userName = $"{employeeName}@{ObjectSpace.TenantName()}";
                        employee.User = ObjectSpace.EnsureUser(userName, user => user.Employee = employee);
                        employee.User.Roles.Add(defaultRole);
                        employee.User.Roles.Add(ObjectSpace.FindRole(employee.Department));
                    })
                    .Enumerate();
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

    private void CreateDepartmentRoles() 
        => Enum.GetValues<EmployeeDepartment>()
            .Do(department => ObjectSpace.EnsureRole(department))
            .Enumerate();

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
        new[]{ "Jim Packard", "Harv Mudd", "Clark Morgan" }
            .Do(name => {
                viewFilter = ObjectSpace.CreateObject<ViewFilter>();
                viewFilter.SetCriteria<Order>(order => order.Employee.FullName == name);
                viewFilter.Name = $"Sales by {name}";
            }).Enumerate();
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
        Enum.GetValues<ProductCategory>().Do(category => {
            var viewFilter = ObjectSpace.CreateObject<ViewFilter>();
            viewFilter.SetCriteria<Product>(product => product.Category == category);
            viewFilter.Name = category.ToString();
        }).Enumerate();
        var viewFilter = ObjectSpace.CreateObject<ViewFilter>();
        viewFilter.SetCriteria<Product>(product => !product.Available);
        viewFilter.Name = "Discontinued";
        viewFilter = ObjectSpace.CreateObject<ViewFilter>();
        viewFilter.SetCriteria<Product>(product => product.CurrentInventory == 0);
        viewFilter.Name = "Out Of Stock";
    }

    private void CustomerFilters(){
        Enum.GetValues<CustomerStatus>().Do(status => {
            var viewFilter = ObjectSpace.CreateObject<ViewFilter>();
            viewFilter.SetCriteria<Customer>(customer => customer.Status == status);
            viewFilter.Name = status.ToString();
        }).Enumerate();
        var viewFilter = ObjectSpace.CreateObject<ViewFilter>();
        viewFilter.SetCriteria<Customer>(customer => customer.TotalEmployees > 10000);
        viewFilter.Name = "Employess > 10000";
        viewFilter = ObjectSpace.CreateObject<ViewFilter>();
        viewFilter.SetCriteria<Customer>(customer => customer.TotalStores > 10);
        viewFilter.Name = "Stores > 10 Location";
        viewFilter = ObjectSpace.CreateObject<ViewFilter>();
        viewFilter.SetCriteria<Customer>(customer => customer.AnnualRevenue > 100000000000);
        viewFilter.Name = "Revenues > 100 Billion";
    }

    private void EmployeeFilters() 
        => Enum.GetValues<EmployeeStatus>().Do(status => {
            var viewFilter = ObjectSpace.CreateObject<ViewFilter>();
            viewFilter.SetCriteria<Employee>(employee => employee.Status == status);
            viewFilter.Name = status.ToString();
        }).Enumerate();

    
}
