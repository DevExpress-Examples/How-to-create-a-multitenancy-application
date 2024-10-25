using DevExpress.ExpressApp;
using OutlookInspired.Blazor.Server.Editors.PdfViewer;
using OutlookInspired.Module.BusinessObjects;

namespace OutlookInspired.Blazor.Server.Features {
    public class WelcomeController : ObjectViewController<DetailView, Welcome> {
        protected override void OnActivated() {
            base.OnActivated();
            View.CustomizeViewItemControl<PdfViewerPropertyEditor>(this, item => {
                item.ComponentModel.CssClass = "welcome-pdf-viewer";
                item.ComponentModel.IsSinglePagePreview = true;
            });
        }
    }
}