using System.ComponentModel;
using DevExpress.ExpressApp;
using DevExpress.Persistent.BaseImpl.EF;
using OutlookInspired.Blazor.Server.Controllers;
using OutlookInspired.Blazor.Server.Features;
using OutlookInspired.Blazor.Server.Features.Customers;
using OutlookInspired.Blazor.Server.Features.Employees.Maps;
using OutlookInspired.Blazor.Server.Features.Evaluations;
using OutlookInspired.Blazor.Server.Features.Maps;
using OutlookInspired.Blazor.Server.Features.Maps.Sales;
using OutlookInspired.Blazor.Server.Features.Quotes;
using OutlookInspired.Blazor.Server.Features.ViewFilter;
using CellDisplayTemplateController = OutlookInspired.Blazor.Server.Features.Employees.Evaluations.CellDisplayTemplateController;
using DetailRowController = OutlookInspired.Blazor.Server.Features.Customers.DetailRowController;

namespace OutlookInspired.Blazor.Server;
[ToolboxItemFilter("Xaf.Platform.Blazor")]
public sealed class OutlookInspiredBlazorModule : ModuleBase {
    private void Application_CreateCustomUserModelDifferenceStore(object sender, CreateCustomModelDifferenceStoreEventArgs e) {
        e.Store = new ModelDifferenceDbStore((XafApplication)sender, typeof(ModelDifference), false, "Blazor");
        e.Handled = true;
    }

    protected override IEnumerable<Type> GetDeclaredControllerTypes()
        =>[typeof(CellDisplayTemplateController), typeof(SchedulerGroupTypeController), typeof(EnableDashboardMasterItemNewActionController),
            typeof(DxGridListEditorController),typeof(DetailRowController),typeof(RichTextPropertyEditorController),
            typeof(Features.Employees.Tasks.CellDisplayTemplateController),typeof(Features.Orders.DetailRowController),
            typeof(MapsTravelModeViewController),typeof(MapsTravelModeViewController),typeof(MapsViewController),typeof(Features.Orders.RouteMapsViewController),
            typeof(BlazorMapsViewController),typeof(PaletteController),
            typeof(PopupWindowSizeController),typeof(ViewFilterController),typeof(MapsSalesPeriodViewController),
            typeof(FunnelFilterController),typeof(WelcomeController), typeof(DisableInlineRowActionController),
            typeof(SalesMapItemDxChartListEditorController),typeof(MapItemListEditorController),typeof(QuoteMapItemListViewController),
            typeof(QuoteListViewController),typeof(OpportunitiesListViewController)
        ];

    public override void Setup(XafApplication application) {
        base.Setup(application);
        application.CreateCustomUserModelDifferenceStore += Application_CreateCustomUserModelDifferenceStore;
    }

}
