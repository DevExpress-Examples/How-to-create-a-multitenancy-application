using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Model;
using OutlookInspired.Module.BusinessObjects;
using OutlookInspired.Module.Features;

namespace OutlookInspired.Blazor.Server.Features.Quotes{
    
    public class OpportunitiesFilterController:ViewController<DashboardView>{
        public OpportunitiesFilterController() => TargetViewId = "Opportunities";

        protected override void OnViewControlsCreated(){
            base.OnViewControlsCreated();
            var quoteAnalysisItem = View.Items.OfType<DashboardViewItem>().First(item => ((IModelObjectView)item.Model.View).ModelClass.TypeInfo.Type==typeof(QuoteAnalysis));
            quoteAnalysisItem.ControlCreated+=MasterItemOnControlCreated;
        }

        private void MasterItemOnControlCreated(object sender, EventArgs e) 
            => ((DashboardViewItem)sender).Frame.GetController<ViewFilterController>().FilterAction.Executed+=FilterActionOnExecuted;
        
        private void FilterActionOnExecuted(object sender, ActionBaseEventArgs e){
            var opportunityItem = View.Items.OfType<DashboardViewItem>().First(item
                => ((IModelObjectView)item.Model.View).ModelClass.TypeInfo.Type == typeof(Opportunity));
            ((ViewFilterController)((ActionBase)sender).Controller).FilterView((ListView)opportunityItem.Frame.View);
        }
    }
}