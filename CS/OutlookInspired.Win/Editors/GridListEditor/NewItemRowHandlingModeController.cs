using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Win.Editors;
using ViewFilter = OutlookInspired.Module.BusinessObjects.ViewFilter;

namespace OutlookInspired.Win.Editors.GridListEditor{
    public class NewItemRowHandlingModeController:ObjectViewController<ListView,ViewFilter>{
        protected override void OnViewControlsCreated(){
            base.OnViewControlsCreated();
            if (View.Editor is not DevExpress.ExpressApp.Win.Editors.GridListEditor listEditor) return;
            listEditor.NewItemRowHandlingMode=GridListEditorNewItemRowHandlingMode.NativeControl;
        }
    }
}