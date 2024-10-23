using System.Drawing;
using DevExpress.ExpressApp;
using OutlookInspired.Blazor.Server.Editors.Charts;
using OutlookInspired.Module.BusinessObjects;
using OutlookInspired.Module.Services;

namespace OutlookInspired.Blazor.Server.Features.Quotes{
    public class OpportunitiesChartListViewController:ObjectViewController<ListView,Opportunity>{
        protected override void OnActivated(){
            base.OnActivated();
            Active[$"not {nameof(DxChartPieListEditor)}"] = View.Editor is DxChartPieListEditor;
        }

        protected override void OnViewControlsCreated(){
            base.OnViewControlsCreated();
            var mapItemDxChartListEditor = ((DxChartPieListEditor)View.Editor);
            var chartModel = mapItemDxChartListEditor.Control;
            chartModel.ArgumentField = item => ((Opportunity)item).Stage.ToString();
            chartModel.ValueField = item => ((Opportunity)item).Value;
            chartModel.SummaryMethod = items => items.Sum();
            chartModel.CustomizeSeriesPoint = e
                => e.PointAppearance.Color = ColorTranslator.FromHtml(e.Point.DataItems.Cast<Opportunity>().First().Stage.Color());
            chartModel.Stick = true;
            chartModel.Style = "margin-top:100px";
        }
    }
}