using DevExpress.Blazor;
using DevExpress.ExpressApp;
using OutlookInspired.Blazor.Server.Editors.Pivot;
using OutlookInspired.Module.BusinessObjects;

namespace OutlookInspired.Blazor.Server.Features.Quotes{
    public class QuoteAnalysisPivotController:ObjectViewController<ListView,QuoteAnalysis>{
        protected override void OnActivated(){
            base.OnActivated();
            Active["editor"] = View.Editor is PivotGridListEditor;
            ((NonPersistentObjectSpace)View.ObjectSpace).ObjectsGetting+=OnObjectsGetting;
            View.CollectionSource.ResetCollection(true);
        }

        protected override void OnDeactivated(){
            base.OnDeactivated();
            ((NonPersistentObjectSpace)View.ObjectSpace).ObjectsGetting-=OnObjectsGetting;
        }

        private void OnObjectsGetting(object sender, ObjectsGettingEventArgs e){
            e.Objects = ObjectSpace.GetObjectsQuery<Quote>()
                .Select(quote => new{
                    quote.CustomerStore.State, quote.CustomerStore.City,
                    quote.Opportunity, quote.Total
                }).ToArray()
                .Select((t, i) => new QuoteAnalysis(){
                    Total = t.Total, Opportunity = t.Opportunity,
                    State = t.State, City = t.City, ID = i
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