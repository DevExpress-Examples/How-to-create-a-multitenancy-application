using DevExpress.ExpressApp;
using DevExpress.ExpressApp.PivotGrid.Win;
using OutlookInspired.Module.BusinessObjects;

namespace OutlookInspired.Win.Features.Quotes{
    public class WinQuoteAnalysisPivotGridListEditorController:ObjectViewController<ListView,QuoteAnalysis>{
        protected override void OnActivated(){
            base.OnActivated();
            View.CollectionSource.CriteriaApplied+=CollectionSourceOnCriteriaApplied;
        }

        protected override void OnDeactivated(){
            base.OnDeactivated();
            View.CollectionSource.CriteriaApplied-=CollectionSourceOnCriteriaApplied;
        }

        private void CollectionSourceOnCriteriaApplied(object sender, EventArgs e){
            ObjectSpace.Refresh();
            ((PivotGridListEditor)View.Editor).ChartControl.RefreshData();
        }
    }
}