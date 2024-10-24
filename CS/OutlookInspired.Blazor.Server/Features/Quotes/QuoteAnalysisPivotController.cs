using DevExpress.Blazor;
using DevExpress.Data.Linq;
using DevExpress.Data.Linq.Helpers;
using DevExpress.ExpressApp;
using OutlookInspired.Blazor.Server.Editors.Pivot;
using OutlookInspired.Module.BusinessObjects;

namespace OutlookInspired.Blazor.Server.Features.Quotes{
    public class QuoteAnalysisPivotController:ObjectViewController<ListView,QuoteAnalysis>{
        private QuoteAnalysis[] _quoteAnalyses;

        protected override void OnActivated(){
            base.OnActivated();
            Active["editor"] = View.Editor is PivotGridListEditor;
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
        }

        private void NonPersistentObjectSpaceOnObjectsCountGetting(object sender, ObjectsCountGettingEventArgs e){
            if (e.Criteria == null){
                e.Count = _quoteAnalyses.Length;
            }
            else{
                var criteriaOperator = ObjectSpace.ParseCriteria(e.Criteria.ToString());
                var expressionEvaluator = ObjectSpace.GetExpressionEvaluator(typeof(QuoteAnalysis),criteriaOperator);
                e.Count = _quoteAnalyses.Count(analysis => (bool)(expressionEvaluator.Evaluate(analysis)??true));    
            }
        }

        private void OnObjectsGetting(object sender, ObjectsGettingEventArgs e){
            var criteriaToExpressionConverter = new CriteriaToExpressionConverter();
            var quotes = (IQueryable<Quote>)ObjectSpace.GetObjectsQuery<Quote>()
                .AppendWhere(criteriaToExpressionConverter, ObjectSpace.ParseCriteria(string.Join(" AND ",View.CollectionSource.Criteria.Values)));
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

        protected override void OnViewControlsCreated(){
            base.OnViewControlsCreated();
            var pivotGridModel = ((DxPivotGridModel)View.Editor.Control);
            pivotGridModel.Fields =[
                new PivotField{
                    Name = nameof(QuoteAnalysis.State),
                    SortOrder = PivotGridSortOrder.Ascending,
                    Area = PivotGridFieldArea.Row, SummaryType = PivotGridSummaryType.Count,
                    Caption = nameof(CustomerStore.State),
                },
                new PivotField{
                    Name = nameof(QuoteAnalysis.City),
                    SortOrder = PivotGridSortOrder.Ascending,
                    Area = PivotGridFieldArea.Row, SummaryType = PivotGridSummaryType.Count,
                    Caption = nameof(CustomerStore.City)
                },
                new PivotField{
                    Name = nameof(QuoteAnalysis.Total), SortOrder = PivotGridSortOrder.Descending, Caption = "Opportunities",
                    Area = PivotGridFieldArea.Data, SummaryType = PivotGridSummaryType.Sum, DisplayFormat = "{0:C0}"
                },
                new PivotField{
                    Name = nameof(QuoteAnalysis.Opportunity), SortOrder = PivotGridSortOrder.Descending, Caption = "PERCENTAGE",
                    Area = PivotGridFieldArea.Data, SummaryType = PivotGridSummaryType.Avg, DisplayFormat = "{0:P}",
                    IsProgressBar = true
                }
            ];
            pivotGridModel.ExpandAllRows = true;
            
        }
    }
}