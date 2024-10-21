using DevExpress.Blazor;
using DevExpress.ExpressApp;
using OutlookInspired.Blazor.Server.Editors.Pivot;
using OutlookInspired.Module.BusinessObjects;

namespace OutlookInspired.Blazor.Server.Features.Quotes{
    public class QuoteListViewController:ObjectViewController<ListView,Quote>{
        protected override void OnActivated(){
            base.OnActivated();
            Active["editor"] = View.Editor is PivotGridListEditor;
        }

        protected override void OnViewControlsCreated(){
            base.OnViewControlsCreated();
            var pivotGridModel = ((Editors.Pivot.DxPivotGridModel)View.Editor.Control);
            pivotGridModel.Fields =[
                new PivotField{
                    Name = $"{nameof(Quote.CustomerStore)}.{nameof(CustomerStore.State)}",
                    SortOrder = PivotGridSortOrder.Ascending,
                    Area = PivotGridFieldArea.Row, SummaryType = PivotGridSummaryType.Count,
                    Caption = nameof(CustomerStore.State),
                },
                new PivotField{
                    Name = $"{nameof(Quote.CustomerStore)}.{nameof(CustomerStore.City)}",
                    SortOrder = PivotGridSortOrder.Ascending,
                    Area = PivotGridFieldArea.Row, SummaryType = PivotGridSummaryType.Count,
                    Caption = nameof(CustomerStore.City)
                },
                new PivotField{
                    Name = nameof(Quote.Total), SortOrder = PivotGridSortOrder.Descending, Caption = "Opportunities",
                    Area = PivotGridFieldArea.Data, SummaryType = PivotGridSummaryType.Sum, DisplayFormat = "{0:C0}"
                },
                new PivotField{
                    Name = nameof(Quote.Opportunity), SortOrder = PivotGridSortOrder.Descending, Caption = "PERCENTAGE",
                    Area = PivotGridFieldArea.Data, SummaryType = PivotGridSummaryType.Avg, DisplayFormat = "{0:P}",
                    IsProgressBar = true
                }
            ];
            pivotGridModel.ExpandAllRows = true;
            
        }
    }
}