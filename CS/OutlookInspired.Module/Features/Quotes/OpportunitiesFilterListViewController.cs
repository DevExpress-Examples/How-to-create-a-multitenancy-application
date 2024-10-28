using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Editors;
using OutlookInspired.Module.BusinessObjects;

namespace OutlookInspired.Module.Features.Quotes{
    public class OpportunitiesFilterListViewController:ObjectViewController<ListView,Opportunity>{
        private CollectionSourceBase _quoteAnalysisCollectionSource;

        protected override void OnActivated(){
            base.OnActivated();
            var viewItem = ((NestedFrame)Frame).ViewItem;
            var item = ((DashboardView)viewItem.View).Items.Cast<DashboardViewItem>().First(dashboardViewItem =>dashboardViewItem.InnerView!=View );
            _quoteAnalysisCollectionSource = ((ListView)item.InnerView).CollectionSource;
            _quoteAnalysisCollectionSource.CriteriaApplied+=QuoteAnalysisCollectionSourceOnCriteriaApplied;
        }

        protected override void OnDeactivated(){
            base.OnDeactivated();
            if (_quoteAnalysisCollectionSource==null)return;
            _quoteAnalysisCollectionSource.CriteriaApplied-=QuoteAnalysisCollectionSourceOnCriteriaApplied;
        }

        private void QuoteAnalysisCollectionSourceOnCriteriaApplied(object sender, EventArgs e){
            View.CollectionSource.Criteria[nameof(OpportunitiesFilterListViewController)] =
                _quoteAnalysisCollectionSource.Criteria[nameof(ViewFilterController)];
            ObjectSpace.Refresh();
        }
    }
}