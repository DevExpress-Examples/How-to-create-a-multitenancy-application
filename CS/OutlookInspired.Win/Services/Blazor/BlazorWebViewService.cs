using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Win;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using OutlookInspired.Module.Blazor;
using OutlookInspired.Module.Services.Internal;

namespace OutlookInspired.Win.Services.Blazor{
    public static class BlazorWebViewService{
        public static EventCallback<KeyboardEventArgs> ExecuteActions(this IBlazorWebViewPropertyEditorActions actions) 
            => EventCallback.Factory.Create<KeyboardEventArgs>(actions, e => {
                if (!e.CtrlKey || e.Key == "Control") return;
                var simpleAction = actions.Actions.OfType<SimpleAction>().Find(e);
                if (simpleAction != null){
                    simpleAction.DoExecute();
                }
                else{
                    var singleChoiceAction = actions.Actions.OfType<SingleChoiceAction>().Find(e);
                    if (singleChoiceAction != null){
                        singleChoiceAction.DoExecute();
                    }
                    else{
                        var parametrizedAction = actions.Actions.OfType<ParametrizedAction>().Find(e);
                        parametrizedAction?.DoExecute(parametrizedAction.Value);
                    }
                }
            });

        static TAction Find<TAction>(this IEnumerable<TAction> source,KeyboardEventArgs e) where TAction:ActionBase 
            => source.Where(@base => {
                var shortcut = ShortcutHelper.ParseBarShortcut(@base.Shortcut);
                return (shortcut.Key & Keys.Control) == Keys.Control && (shortcut.Key & Keys.KeyCode).ToString()
                    .Equals(e.Key, StringComparison.OrdinalIgnoreCase);
            }).Take(1).FirstOrDefault(simpleAction => simpleAction.Available());
    }
}