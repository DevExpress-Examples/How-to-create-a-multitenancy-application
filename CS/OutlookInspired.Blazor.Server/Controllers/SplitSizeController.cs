using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Blazor;
using DevExpress.ExpressApp.Blazor.Services;
using OutlookInspired.Module;

namespace OutlookInspired.Blazor.Server.Controllers{
    public class SplitSizeController:ViewController<ListView>{
        ISplitLayoutSizeService SplitLayoutSizeService =>
            ((BlazorApplication)Application).ServiceProvider.GetRequiredService<ISplitLayoutSizeService>();
        protected override void OnActivated() {
            base.OnActivated();
            SplitLayoutSizeService[View.Id] = (((IModelListViewSplitterRelativePosition)View.Model.SplitLayout).RelativePosition);
        }
    }
}