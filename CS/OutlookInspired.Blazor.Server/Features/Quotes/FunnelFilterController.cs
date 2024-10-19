using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Layout;
using OutlookInspired.Blazor.Server.Components.Models;
using OutlookInspired.Module.Services.Internal;

namespace OutlookInspired.Blazor.Server.Features.Quotes{
    public class FunnelFilterController:ViewController<DashboardView>{
        public FunnelFilterController() => TargetViewId = "Opportunities";

        protected override void OnViewControlsCreated(){
            base.OnViewControlsCreated();
            // View.MasterItem().ControlCreated+=MasterDashboardViewItemOnControlCreated;
            // View.ChildItem().ControlCreated += ChildDashboardViewItemOnControlCreated;
        }

        private void ChildDashboardViewItemOnControlCreated(object sender, EventArgs e){
            var dashboardViewItem = ((DashboardViewItem)sender);
            dashboardViewItem.ControlCreated-=ChildDashboardViewItemOnControlCreated;
            UserControl(dashboardViewItem).ControlCreated+=OnChildControlCreated;
        }

        private static ControlViewItem UserControl(DashboardViewItem dashboardViewItem) 
            => dashboardViewItem.Frame.View.ToDetailView().GetItems<ControlViewItem>().First();

        private void OnChildControlCreated(object sender, EventArgs e){
            ((ControlViewItem)sender).ControlCreated-=OnMasterControlCreated;
            SetCriteria((UserControlComponentModel)UserControl(View.MasterItem()).Control);
        }

        private void MasterDashboardViewItemOnControlCreated(object sender, EventArgs e){
            var dashboardViewItem = ((DashboardViewItem)sender);
            dashboardViewItem.ControlCreated-=MasterDashboardViewItemOnControlCreated;
            // dashboardViewItem.Frame.View.ToDetailView().GetItems<ControlViewItem>().First().ControlCreated+=OnMasterControlCreated;
        }

        private void OnMasterControlCreated(object sender, EventArgs e){
            var controlViewItem = ((ControlViewItem)sender);
            controlViewItem.ControlCreated-=OnMasterControlCreated;
            ((UserControlComponentModel)controlViewItem.Control).CriteriaChanged+=OnCriteriaChanged;
        }

        private void OnCriteriaChanged(object sender, EventArgs e) 
            => SetCriteria((UserControlComponentModel)sender);

        private void SetCriteria(UserControlComponentModel model)
            => View.ChildItem().Frame.View.ToDetailView().GetItems<ControlViewItem>()
                .Select(item => item.Control).Cast<UserControlComponentModel>().First()
                .SetCriteria(model.Criteria?.ToString());
    }
}