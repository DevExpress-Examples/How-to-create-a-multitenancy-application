using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Win;
using DevExpress.ExpressApp.Win.Editors;
using Microsoft.AspNetCore.Components.Web;
using OutlookInspired.Module.Services.Internal;

namespace OutlookInspired.Win.Editors.DxHtmlEditorEditor{
    public class BlazorWebViewShortcutsController:ViewController<DetailView>{
        TAction Find<TAction>( KeyboardEventArgs e) where TAction:ActionBase 
            => Frame.Controllers.Cast<Controller>().SelectMany(controller => controller.Actions).OfType<TAction>()
                    .FirstOrDefault(action => action.Active && action.Enabled&&Find(e, action));


        private static bool Find<TAction>(KeyboardEventArgs e, TAction action) where TAction : ActionBase{
            if (action.Shortcut == null) return false;
            var shortcut = ShortcutHelper.ParseBarShortcut(action.Shortcut);
            return (shortcut.Key & Keys.Control) == Keys.Control && (shortcut.Key & Keys.KeyCode).ToString()
                .Equals(e.Key, StringComparison.OrdinalIgnoreCase);
        }

        protected override void OnActivated(){
            base.OnActivated();
            View.CustomizeViewItemControl<WinPropertyEditor>(this, winPropertyEditor => {
                if (winPropertyEditor is not IBlazorWebViewKeydown keydown) return;
                SynchronizeActionShortcuts(keydown);
            });
        }

        private void SynchronizeActionShortcuts(IBlazorWebViewKeydown editor){
            editor.KeyDown += (_, e) => {
                if (!e.CtrlKey || e.Key == "Control") return;
                var simpleAction = Find<SimpleAction>( e);
                if (simpleAction != null){
                    simpleAction.DoExecute();
                }
                else{
                    var singleChoiceAction = Find<SingleChoiceAction>(e);
                    if (singleChoiceAction != null){
                        singleChoiceAction.DoExecute();
                    }
                    else{
                        var parametrizedAction = Find<ParametrizedAction>( e);
                        parametrizedAction?.DoExecute(parametrizedAction.Value);
                    }
                }
            };
        }
    }
}