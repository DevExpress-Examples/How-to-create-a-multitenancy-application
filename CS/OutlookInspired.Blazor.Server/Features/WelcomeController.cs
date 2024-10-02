using DevExpress.ExpressApp;
using OutlookInspired.Blazor.Server.Editors.PdfViewer;

namespace OutlookInspired.Blazor.Server.Features {
    public class WelcomeController : Module.Features.WelcomeController {
        protected override void OnActivated() {
            base.OnActivated();
            View.CustomizeViewItemControl<PdfViewerPropertyEditor>(this, item => {
                item.ComponentModel.CssClass = "welcome-pdf-viewer";
                item.ComponentModel.IsSinglePagePreview = true;
            });
        }
    }
}