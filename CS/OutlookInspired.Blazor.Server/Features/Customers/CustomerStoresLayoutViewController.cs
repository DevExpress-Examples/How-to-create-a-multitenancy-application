using DevExpress.ExpressApp;
using OutlookInspired.Blazor.Server.Editors.LayoutView;
using OutlookInspired.Blazor.Server.Editors.LayoutViewStacked;
using OutlookInspired.Module.BusinessObjects;

namespace OutlookInspired.Blazor.Server.Features.Customers{
    public class CustomerStoresLayoutViewController:ObjectViewController<ListView, CustomerStore>{
        protected override void OnActivated(){
            base.OnActivated();
            Active["editor"] = View.Editor is StackedLayoutViewEditor;
        }

        protected override void OnViewControlsCreated(){
            base.OnViewControlsCreated();
            var model = ((StackedLayoutViewEditor)View.Editor).Control;
            model.ImageSelector = o => ((CustomerStore)o).Crest.LargeImage;
            model.BodySelector = o => ((CustomerStore)o).City;
            
        }
        
    }
}