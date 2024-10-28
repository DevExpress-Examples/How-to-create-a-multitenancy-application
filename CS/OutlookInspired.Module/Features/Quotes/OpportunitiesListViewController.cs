using DevExpress.Data.Linq;
using DevExpress.Data.Linq.Helpers;
using DevExpress.ExpressApp;
using OutlookInspired.Module.BusinessObjects;


namespace OutlookInspired.Module.Features.Quotes{
    public class OpportunitiesListViewController:ObjectViewController<ListView,Opportunity>{
        

        protected override void OnActivated(){
            base.OnActivated();
            var nonPersistentObjectSpace = ((NonPersistentObjectSpace)ObjectSpace);
            nonPersistentObjectSpace.ObjectsGetting+=OnObjectsGetting;
            View.CollectionSource.ResetCollection(true);
        }

        protected override void OnDeactivated(){
            base.OnDeactivated();
            if (ObjectSpace is not NonPersistentObjectSpace nonPersistentObjectSpace) return;
            nonPersistentObjectSpace.ObjectsGetting += OnObjectsGetting;
        }

        

        private void OnObjectsGetting(object sender, ObjectsGettingEventArgs e) 
            => e.Objects =Enum.GetValues<Stage>().Where(stage => stage != Stage.Summary)
                .Select(stage => NewOpportunity(stage, (IObjectSpace)sender, stage.Range()))
                .Select((item, i) => {
                    item.ID = i;
                    return item;
                }).ToList();

        private Opportunity NewOpportunity(Stage stage, IObjectSpace objectSpace, (double min, double max) value){
            var criteriaToExpressionConverter = new CriteriaToExpressionConverter();
            var quotes = ((IQueryable<Quote>)objectSpace.GetObjectsQuery<Quote>()
                .AppendWhere(criteriaToExpressionConverter, ObjectSpace.ParseCriteria(string.Join(" AND ",View.CollectionSource.Criteria.Values))))
                ;
            return new(){ Stage = stage, Value = (decimal)quotes
                .Where(quote => quote.Opportunity > value.min && quote.Opportunity < value.max)
                .Select(quote => (double)quote.Total)
                .Sum() };
        }


    }
}