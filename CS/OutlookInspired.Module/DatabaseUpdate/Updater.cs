﻿using Aqua.EnumerableExtensions;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Updating;
using DevExpress.Persistent.BaseImpl.EF.MultiTenancy;
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
        SynchronizeDatesWithToday();
    }

    private void SynchronizeDatesWithToday(){
        using var updateCommand = CreateCommand($@"
        WITH ProductOrderDates AS (
            SELECT
                oi.{nameof(OrderItem.ProductID)},
                MAX(o.{nameof(Order.OrderDate)}) AS MostRecentOrderDate
            FROM
                {nameof(OutlookInspiredEFCoreDbContext.OrderItems)} oi
                INNER JOIN {nameof(OutlookInspiredEFCoreDbContext.Orders)} o ON oi.{nameof(OrderItem.OrderID)} = o.Id
            GROUP BY
                oi.{nameof(OrderItem.ProductID)}
        )
        UPDATE o
        SET o.{nameof(Order.OrderDate)} = DATEADD(DAY, DATEDIFF(DAY, pod.MostRecentOrderDate, GETDATE()), o.{nameof(Order.OrderDate)})
        FROM {nameof(OutlookInspiredEFCoreDbContext.Orders)} o
        INNER JOIN {nameof(OutlookInspiredEFCoreDbContext.OrderItems)} oi ON o.Id = oi.{nameof(OrderItem.OrderID)}
        INNER JOIN ProductOrderDates pod ON oi.{nameof(OrderItem.ProductID)} = pod.{nameof(OrderItem.ProductID)}");
        updateCommand.ExecuteNonQuery();
        
    }

    public override void UpdateDatabaseAfterUpdateSchema() {
        base.UpdateDatabaseAfterUpdateSchema();
        if (!ObjectSpace.CanInstantiate(typeof(ApplicationUser))) return;
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
            tenant.ConnectionString = $"Integrated Security=SSPI;MultipleActiveResultSets=True;Data Source=(localdb)\\mssqllocaldb;Initial Catalog={databaseName}";
        }
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
