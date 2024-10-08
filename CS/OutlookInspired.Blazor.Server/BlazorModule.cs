using System.ComponentModel;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Model.Core;
using DevExpress.Persistent.BaseImpl.EF;
using OutlookInspired.Blazor.Server.Controllers;
using OutlookInspired.Blazor.Server.Editors;
using OutlookInspired.Blazor.Server.Editors.MapItemChart;
using OutlookInspired.Blazor.Server.Features;
using OutlookInspired.Blazor.Server.Features.Customers;
using OutlookInspired.Blazor.Server.Features.Employees;
using OutlookInspired.Blazor.Server.Features.Employees.Maps;
using OutlookInspired.Blazor.Server.Features.Evaluations;
using OutlookInspired.Blazor.Server.Features.Maps;
using OutlookInspired.Blazor.Server.Features.Quotes;
using OutlookInspired.Blazor.Server.Features.ViewFilter;
using CellDisplayTemplateController = OutlookInspired.Blazor.Server.Features.Employees.Evaluations.CellDisplayTemplateController;

namespace OutlookInspired.Blazor.Server;
[ToolboxItemFilter("Xaf.Platform.Blazor")]
public sealed class OutlookInspiredBlazorModule : ModuleBase {
    private void Application_CreateCustomUserModelDifferenceStore(object sender, CreateCustomModelDifferenceStoreEventArgs e) {
        e.Store = new ModelDifferenceDbStore((XafApplication)sender, typeof(ModelDifference), false, "Blazor");
        e.Handled = true;
    }

    protected override IEnumerable<Type> GetDeclaredControllerTypes()
        => new[]{
            typeof(CellDisplayTemplateController), typeof(SchedulerGroupTypeController), typeof(EnableDashboardMasterItemNewActionController),
            typeof(DxGridListEditorController),typeof(DetailRowController),typeof(RichTextPropertyEditorController),
            typeof(Features.Employees.Tasks.CellDisplayTemplateController),typeof(Features.Orders.DetailRowController),
            typeof(TravelModeViewController),typeof(TravelModeViewController),typeof(MapsViewController),typeof(Features.Orders.RouteMapsViewController),
            typeof(BlazorMapsViewController),typeof(PaletteController),typeof(PopupWindowSizeController),typeof(ViewFilterController),
            typeof(FunnelFilterController),typeof(WelcomeController), typeof(DisableInlineRowActionController),
            typeof(SalesMapItemDxChartListEditorController),typeof(SalesMapsViewController)
        };

    public override void Setup(XafApplication application) {
        base.Setup(application);
        application.CreateCustomUserModelDifferenceStore += Application_CreateCustomUserModelDifferenceStore;
    }

}
