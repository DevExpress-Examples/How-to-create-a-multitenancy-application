using DevExpress.ExpressApp;
using OutlookInspired.Module.Features;
using OutlookInspired.Module.Services.Internal;

namespace OutlookInspired.Win.Features.Quotes{
    public class FunnelFilterController:ViewController<DashboardView>{
        public FunnelFilterController() => TargetViewId = "Opportunities";
        protected override void OnDeactivated(){
            base.OnDeactivated();
            View.MasterItem().Frame.View.ToListView().CollectionSource.CriteriaApplied-=CollectionSourceOnCriteriaApplied;
        }

        protected override void OnViewControlsCreated(){
            base.OnViewControlsCreated();
            View.MasterItem().Frame.View.ToListView().CollectionSource.CriteriaApplied+=CollectionSourceOnCriteriaApplied;
        }
        
        private void CollectionSourceOnCriteriaApplied(object sender, EventArgs e) 
            => ((ProxyCollection)((CollectionSource)View.ChildItem().Frame.View.ToListView().CollectionSource).Collection)
                .SetCollection(ObjectSpace.Opportunities(View.MasterItem().Frame.View.ToListView().CollectionSource.Criteria[nameof(ViewFilterController)]?.ToString())
                .ToBindingList());
    }
}