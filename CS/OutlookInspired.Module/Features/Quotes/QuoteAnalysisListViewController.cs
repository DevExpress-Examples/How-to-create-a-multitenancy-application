using DevExpress.Data.Filtering;
using DevExpress.Data.Linq;
using DevExpress.Data.Linq.Helpers;
using DevExpress.ExpressApp;
using OutlookInspired.Module.BusinessObjects;

namespace OutlookInspired.Module.Features.Quotes{
    public class QuoteAnalysisListViewController:ObjectViewController<ListView,QuoteAnalysis>{
        private QuoteAnalysis[] _quoteAnalyses;

        protected override void OnActivated(){
            base.OnActivated();
            Frame.GetController<ViewFilterController>().CustomizeStartItem+=OnCustomizeStartItem;
            var nonPersistentObjectSpace = ((NonPersistentObjectSpace)View.ObjectSpace);
            nonPersistentObjectSpace.ObjectsGetting+=OnObjectsGetting;
            nonPersistentObjectSpace.ObjectsCountGetting+=NonPersistentObjectSpaceOnObjectsCountGetting;
            View.CollectionSource.ResetCollection(true);
        }

        protected override void OnDeactivated(){
            base.OnDeactivated();
            var nonPersistentObjectSpace = ((NonPersistentObjectSpace)View.ObjectSpace);
            nonPersistentObjectSpace.ObjectsGetting-=OnObjectsGetting;
            nonPersistentObjectSpace.ObjectsCountGetting-=NonPersistentObjectSpaceOnObjectsCountGetting;
            Frame.GetController<ViewFilterController>().CustomizeStartItem-=OnCustomizeStartItem;
        }

        private void OnCustomizeStartItem(object sender, CustomizeStartItemArgs e) 
            => e.ChoiceActionItem= ((ViewFilterController)sender).FilterAction.Items.First(item => $"{item.Data}" == "This Month");
        
        private void NonPersistentObjectSpaceOnObjectsCountGetting(object sender, ObjectsCountGettingEventArgs e){
            if (e.Criteria == null){
                e.Count = _quoteAnalyses.Length;
            }
            else{
                if (e.Criteria == null){
                    e.Count = _quoteAnalyses.Length;
                }
                else{
                    e.Count = _quoteAnalyses.Count(analysis
                        => {
                        var isObjectFitForCriteria = ObjectSpace.IsObjectFitForCriteria(analysis, (CriteriaOperator)e.Criteria);
                        return isObjectFitForCriteria.HasValue && isObjectFitForCriteria.Value;
                    });    
                }
            }
        }

        private void OnObjectsGetting(object sender, ObjectsGettingEventArgs e){
            var criteriaToExpressionConverter = new CriteriaToExpressionConverter();
            
            var quotes = ObjectSpace.GetObjectsQuery<Quote>();
            if (View.CollectionSource.Criteria.Keys.Any()){
                quotes = (IQueryable<Quote>)quotes.AppendWhere(criteriaToExpressionConverter,
                    new GroupOperator(GroupOperatorType.And, View.CollectionSource.Criteria.Values));
            }
            e.Objects = _quoteAnalyses = quotes
                .Select(quote => new{
                    quote.CustomerStore.State, quote.CustomerStore.City,
                    quote.Opportunity, quote.Total, quote.Date
                })
                .ToArray()
                .Select((t, i) => new QuoteAnalysis{
                    Total = t.Total, Opportunity = t.Opportunity,
                    State = t.State, City = t.City, ID = i, Date = t.Date
                }).ToArray();
        }

    }
}