using System.Drawing;
using DevExpress.ExpressApp;
using OutlookInspired.Blazor.Server.Editors.Charts;
using OutlookInspired.Module.BusinessObjects;
using OutlookInspired.Module.Services;

namespace OutlookInspired.Blazor.Server.Features.Quotes{
    public class OpportunitiesListViewController:ObjectViewController<ListView,Opportunity>{
        protected override void OnActivated(){
            base.OnActivated();
            Active[$"not {nameof(DxChartPieListEditor)}"] = View.Editor is DxChartPieListEditor;
            if (!Active)return;
            ((NonPersistentObjectSpace)ObjectSpace).ObjectsGetting+=OnObjectsGetting;
            View.CollectionSource.ResetCollection(true);
        }

        protected override void OnDeactivated(){
            base.OnDeactivated();
            if (ObjectSpace is not NonPersistentObjectSpace nonPersistentObjectSpace) return;
            nonPersistentObjectSpace.ObjectsGetting += OnObjectsGetting;
        }

        private void OnObjectsGetting(object sender, ObjectsGettingEventArgs e) 
            => e.Objects = Enum.GetValues<Stage>().Where(stage1 => stage1 != Stage.Summary)
                .Select(stage => NewOpportunity(stage, (IObjectSpace)sender, stage.Map()))
                .Select((item, i) => {
                    item.ID = i;
                    return item;
                }).ToList();

        private Opportunity NewOpportunity(Stage stage, IObjectSpace objectSpace, (double min, double max) value){
            var quotes = objectSpace.GetObjectsQuery<Quote>()
                .Where(quote => quote.Opportunity > value.min && quote.Opportunity < value.max).ToArray();
            return new(){ Stage = stage, Value = quotes.Sum(q => q.Total) };
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
        }
    }
}