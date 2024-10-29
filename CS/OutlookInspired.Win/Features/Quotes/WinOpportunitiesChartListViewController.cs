using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Chart.Win;
using DevExpress.XtraCharts;
using OutlookInspired.Module.BusinessObjects;

namespace OutlookInspired.Win.Features.Quotes{
    public class WinOpportunitiesChartListViewController:ObjectViewController<ListView,Opportunity>{
        protected override void OnActivated(){
            base.OnActivated();
            Active[$"not {nameof(ChartListEditor)}"] = View.Editor is ChartListEditor;
        }

        protected override void OnViewControlsCreated(){
            base.OnViewControlsCreated();
            ((ChartControl)View.Editor.Control).CustomDrawSeriesPoint+=OnCustomDrawSeriesPoint;
        }

        private void OnCustomDrawSeriesPoint(object sender, CustomDrawSeriesPointEventArgs e){
            if (e.SeriesPoint.Tag is not Opportunity opportunity) return;
            e.SeriesDrawOptions.Color = ColorTranslator.FromHtml(opportunity.Stage.Color());
        }
    }
}