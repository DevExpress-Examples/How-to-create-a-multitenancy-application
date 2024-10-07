using DevExpress.ExpressApp.Actions;

namespace OutlookInspired.Win.Services.Blazor{
    public interface IBlazorWebViewPropertyEditorActions{
        List<ActionBase> Actions { get; set; }
    }
}