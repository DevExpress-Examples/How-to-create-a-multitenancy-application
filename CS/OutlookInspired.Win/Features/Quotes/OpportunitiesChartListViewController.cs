using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Chart.Win;
using DevExpress.ExpressApp.Editors;
using OutlookInspired.Module.BusinessObjects;

namespace OutlookInspired.Win.Features.Quotes{
    public class OpportunitiesChartListViewController:ObjectViewController<ListView,Opportunity>{
        private CollectionSourceBase _collectionSourceBase;

        protected override void OnActivated(){
            base.OnActivated();
            if (View.Editor is not ChartListEditor){
                Active[$"not {nameof(ChartListEditor)}"] = false;
                return;
            }

            var viewItem = ((NestedFrame)Frame).ViewItem;
            var item = ((DashboardView)viewItem.View).Items.Cast<DashboardViewItem>().First();
            _collectionSourceBase = ((ListView)item.InnerView).CollectionSource;
            _collectionSourceBase.CriteriaApplied+=CollectionSourceOnCriteriaApplied;
        }

        protected override void OnDeactivated(){
            base.OnDeactivated();
            if (_collectionSourceBase==null)return;
            _collectionSourceBase.CriteriaApplied-=CollectionSourceOnCriteriaApplied;
        }

        private void CollectionSourceOnCriteriaApplied(object sender, EventArgs e){
            ObjectSpace.Refresh();
        }
    }
}