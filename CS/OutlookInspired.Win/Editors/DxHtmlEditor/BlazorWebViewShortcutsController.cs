using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Win;
using DevExpress.ExpressApp.Win.Editors;
using Microsoft.AspNetCore.Components.Web;

namespace OutlookInspired.Win.Editors.DxHtmlEditor {
    public class BlazorWebViewKeyDownController : ViewController<DetailView> {
        private static bool Find<TAction>(KeyboardEventArgs e, TAction action) where TAction : ActionBase {
            if(action.Shortcut == null) return false;
            var shortcut = ShortcutHelper.ParseBarShortcut(action.Shortcut);
            return (shortcut.Key & Keys.Control) == Keys.Control && (shortcut.Key & Keys.KeyCode).ToString().Equals(e.Key, StringComparison.OrdinalIgnoreCase);
        }

        protected override void OnActivated() {
            base.OnActivated();
            View.CustomizeViewItemControl<WinPropertyEditor>(this, winPropertyEditor => {
                if(winPropertyEditor is not IBlazorWebViewKeyDown keydown) return;
                SynchronizeActionShortcuts(keydown);
            });
        }
        private void SynchronizeActionShortcuts(IBlazorWebViewKeyDown editor) 
            => editor.KeyDown += (_, e) => {
                if(!e.CtrlKey || e.Key == "Control") return;
                var action = Frame.Controllers.Values.SelectMany(controller => controller.Actions)
                    .Where(action => Find(e, action)).FirstOrDefault(action => action.Active && action.Enabled);
                switch(action) {
                    case SimpleAction simpleAction:
                        simpleAction.DoExecute();
                        break;
                    case SingleChoiceAction singleChoiceAction:
                        singleChoiceAction.DoExecute(singleChoiceAction.SelectedItem);
                        break;
                    case ParametrizedAction parametrizedAction:
                        parametrizedAction.DoExecute(parametrizedAction.Value);
                        break;
                }
            };
    }
}