using DevExpress.ExpressApp;
using DevExpress.Persistent.Base;

namespace OutlookInspired.Win.Features.Maps{
    [Obsolete]
    public class MapsViewController:Module.Features.Maps.MapsViewController{
        protected override PredefinedCategory PopupActionsCategory() => PredefinedCategory.View;

        protected override string FrameContext() => TemplateContext.View;
    }
}