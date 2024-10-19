using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Layout;
using OutlookInspired.Module.BusinessObjects;
using OutlookInspired.Module.Features.Maps;
using OutlookInspired.Module.Features.ViewFilter;
using OutlookInspired.Module.Services.Internal;

namespace OutlookInspired.Blazor.Server.Features.Quotes {
    [Obsolete]
    public class PaletteController : ViewController<DashboardView> {
        private (string color, Stage stage)[] _palette;
        public PaletteController() => TargetViewId = "Opportunities";
        protected override void OnViewControlsCreated() {
            base.OnViewControlsCreated();
            View.MasterItem().ControlCreated += OnMasterItemControlCreated;
            View.ChildItem().ControlCreated += OncChildItemControlCreated;
        }

        private void OncChildItemControlCreated(object sender, EventArgs e) {
            var dashboardViewItem = ((DashboardViewItem)sender);
            dashboardViewItem.ControlCreated -= OnMasterItemControlCreated;
            // dashboardViewItem.Frame.View.ToDetailView().GetItems<ControlViewItem>().First().ControlCreated += OnChartControlCreated;
        }

        private void OnChartControlCreated(object sender, EventArgs e) {
            var controlViewItem = ((ControlViewItem)sender);
            controlViewItem.ControlCreated -= OnChartControlCreated;
            _palette = ((DxFunnelModel)controlViewItem.Control).ComponentModel.Options.PaletteData;
        }

        private void OnMasterItemControlCreated(object sender, EventArgs e) {
            var dashboardViewItem = ((DashboardViewItem)sender);
            dashboardViewItem.ControlCreated -= OnMasterItemControlCreated;
            if (dashboardViewItem.Frame?.GetController<MapsViewController>() is { } targetController) {
                targetController.MapItAction.Executed += MapItActionOnExecuted;
            }
        }

        protected override void OnDeactivated() {
            base.OnDeactivated();
            var targetController = View?.MasterItem()?.Frame?.GetController<MapsViewController>();
            if (targetController != null) {
                targetController.MapItAction.Executed -= MapItActionOnExecuted;
            }
        }
        private void MapItActionOnExecuted(object sender, ActionBaseEventArgs e) {
            var controller = Application.CreateController<BlazorMapsViewController>();
            controller.Palette = _palette;
            e.ShowViewParameters.Controllers.Add(controller);
            if (View.MasterItem()?.Frame.GetController<ViewFilterController>() is { } targetController &&
                targetController.FilterAction.SelectedItem.Data is Module.BusinessObjects.ViewFilter viewFilter) {
                controller.Criteria = viewFilter.Criteria;
            }
        }
    }
}