using DevExpress.ExpressApp;
using OutlookInspired.Blazor.Server.Editors.LayoutViewStacked;
using OutlookInspired.Module.BusinessObjects;

namespace OutlookInspired.Blazor.Server.Features.Customers {
    public class CustomerStoresLayoutViewController : ObjectViewController<ListView, CustomerStore> {
        protected override void OnViewControlsCreated() {
            base.OnViewControlsCreated();
            if(View.Editor is StackedLayoutViewEditor editor) {
                var model = editor.Control;
                model.ImageSelector = o => ((CustomerStore)o).Crest.LargeImage;
                model.BodySelector = o => ((CustomerStore)o).City;
            }
        }
    }
}