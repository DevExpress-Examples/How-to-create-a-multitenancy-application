using DevExpress.Blazor;
using DevExpress.ExpressApp;
using OutlookInspired.Blazor.Server.Editors.Pivot;
using OutlookInspired.Module.BusinessObjects;

namespace OutlookInspired.Blazor.Server.Features.Quotes {
    public class BlazorQuoteAnalysisPivotGridListEditorController : ObjectViewController<ListView, QuoteAnalysis> {
        protected override void OnActivated() {
            base.OnActivated();
            Active["editor"] = View.Editor is PivotGridListEditor;
        }

        protected override void OnViewControlsCreated() {
            base.OnViewControlsCreated();
            if(View.Editor is PivotGridListEditor editor) {
                editor.Control.Fields = [
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
                editor.Control.ExpandAllRows = true;
            }

        }
    }
}