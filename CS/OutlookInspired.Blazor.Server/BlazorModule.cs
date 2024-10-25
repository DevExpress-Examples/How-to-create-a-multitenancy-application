using System.ComponentModel;
using DevExpress.ExpressApp;
using DevExpress.Persistent.BaseImpl.EF;
using OutlookInspired.Blazor.Server.Controllers;
using OutlookInspired.Blazor.Server.Features;
using OutlookInspired.Blazor.Server.Features.Customers;
using OutlookInspired.Blazor.Server.Features.Employees;
using OutlookInspired.Blazor.Server.Features.Employees.Evaluations;
using OutlookInspired.Blazor.Server.Features.Employees.Maps;
using OutlookInspired.Blazor.Server.Features.Employees.Tasks;
using OutlookInspired.Blazor.Server.Features.Evaluations;
using OutlookInspired.Blazor.Server.Features.Maps.Sales;
using OutlookInspired.Blazor.Server.Features.Orders;
using OutlookInspired.Blazor.Server.Features.Products;
using OutlookInspired.Blazor.Server.Features.Quotes;
using OutlookInspired.Blazor.Server.Features.ViewFilter;


namespace OutlookInspired.Blazor.Server;
[ToolboxItemFilter("Xaf.Platform.Blazor")]
public sealed class OutlookInspiredBlazorModule : ModuleBase {
    private void Application_CreateCustomUserModelDifferenceStore(object sender, CreateCustomModelDifferenceStoreEventArgs e) {
        e.Store = new ModelDifferenceDbStore((XafApplication)sender, typeof(ModelDifference), false, "Blazor");
        e.Handled = true;
    }

    protected override IEnumerable<Type> GetDeclaredControllerTypes()
        =>[typeof(EmployeeEvaluationsController), typeof(SchedulerGroupTypeController), typeof(DxGridListEditorController),typeof(CustomerListViewDetailRowController),
            typeof(EmployeeTasksController), typeof(MapsTravelModeViewController),typeof(MapsTravelModeViewController),typeof(PopupWindowSizeController),
            typeof(ViewFilterDeleteController),typeof(OpportunitiesFilterController),typeof(WelcomeController), typeof(DisableInlineRowActionController),
            typeof(SalesMapItemDxChartListEditorController),typeof(MapItemListEditorController),typeof(QuoteMapItemListViewController),
            typeof(QuoteAnalysisPivotController),typeof(OpportunitiesChartListViewController),typeof(EmployeeLayoutViewController),typeof(CustomerLayoutViewController),
            typeof(CustomerLayoutViewController),typeof(CustomerStoresLayoutViewController),typeof(ProductLayoutViewController),
            typeof(OrderListViewDetailRowController)
        ];

    public override void Setup(XafApplication application) {
        base.Setup(application);
        application.CreateCustomUserModelDifferenceStore += Application_CreateCustomUserModelDifferenceStore;
    }

}
