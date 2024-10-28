using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.XtraCharts;
using OutlookInspired.Module.BusinessObjects;

namespace OutlookInspired.Win.Features.Quotes{
    public class PaletteEntriesController:ViewController<DashboardView>{
        private PaletteEntry[] _paletteEntries;
        public PaletteEntriesController() => TargetViewId = "Opportunities";
        protected override void OnViewControlsCreated(){
            base.OnViewControlsCreated();
            // ((DevExpress.ExpressApp.Chart.Win.ChartListEditor)View.ChildItem().Frame.View.ToListView().Editor)
                // .ControlsCreated+=ChartListEditorOnControlsCreated;
            // View.MasterItem().Frame.GetController<MapQuoteController>().MapQuoteAction.Executed+=MapQuoteActionOnExecuted;
        }

        private void ChartListEditorOnControlsCreated(object sender, EventArgs e) 
            => _paletteEntries = ((DevExpress.ExpressApp.Chart.Win.ChartListEditor)sender).ChartControl
                .GetPaletteEntries(Enum.GetValues(typeof(Stage)).Length);

        protected override void OnDeactivated(){
            base.OnDeactivated();
            // View.MasterItem().Frame.GetController<MapQuoteController>().MapQuoteAction.Executed-=MapQuoteActionOnExecuted;
        }

        private void MapQuoteActionOnExecuted(object sender, ActionBaseEventArgs e) 
            => ((Quote)e.ShowViewParameters.CreatedView.CurrentObject).PaletteEntries=_paletteEntries;
    }
}