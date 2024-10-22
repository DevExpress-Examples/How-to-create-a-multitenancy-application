using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Blazor.Editors;

namespace OutlookInspired.Blazor.Server.Controllers {
    public class DxGridListEditorController : ViewController<ListView> {
        protected override void OnViewControlsCreated() {
            base.OnViewControlsCreated();
            if (View.Editor is not DxGridListEditor editor) return;
            editor.RowClickMode = RowClickMode.SelectOnSingleProcessOnDouble;
        }
    }
}