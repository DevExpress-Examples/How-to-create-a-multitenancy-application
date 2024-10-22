using DevExpress.ExpressApp;
using OutlookInspired.Blazor.Server.Editors.LayoutView;
using OutlookInspired.Module.BusinessObjects;

namespace OutlookInspired.Blazor.Server.Features.Customers{
    public class CustomerLayoutViewController:ObjectViewController<ListView, Customer>{
        public CustomerLayoutViewController() => TargetViewId = Customer.LayoutViewListView;
        
        protected override void OnViewControlsCreated(){
            base.OnViewControlsCreated();
            var model = ((LayoutViewListEditor)View.Editor).Control;
            model.ImageSelector = o => ((Customer)o).Logo;
            model.HeaderSelector = o => ((Customer)o).Name;
            model.InfoItemsSelector = o => {
                var customer = ((Customer)o);
                return new Dictionary<string, string>{
                    { "HOME OFFICE", customer.HomeOfficeLine },
                    { "BILLING ADDRESS", customer.BillingAddressLine }
                };
            };
        }
        
    }
}