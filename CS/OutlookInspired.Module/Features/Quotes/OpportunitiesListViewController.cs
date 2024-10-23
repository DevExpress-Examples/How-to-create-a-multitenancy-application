using DevExpress.ExpressApp;
using OutlookInspired.Module.BusinessObjects;
using OutlookInspired.Module.Services;

namespace OutlookInspired.Module.Features.Quotes{
    public class OpportunitiesListViewController:ObjectViewController<ListView,Opportunity>{
        protected override void OnActivated(){
            base.OnActivated();
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


    }
}