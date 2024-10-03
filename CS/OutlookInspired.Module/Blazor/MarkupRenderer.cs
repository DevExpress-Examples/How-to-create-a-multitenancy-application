using DevExpress.ExpressApp.Blazor.Components.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace OutlookInspired.Module.Blazor{
    public class MarkupRenderer : ComponentBase, IDisposable{
        [Parameter]
        public MarkupContentService ContentService { get; set; }

        protected override void OnInitialized() => ContentService.OnChange += StateHasChanged;

        protected override void BuildRenderTree(RenderTreeBuilder builder) => builder.AddContent(0, ContentService.Model.GetComponentContent());

        public void Dispose() => ContentService.OnChange -= StateHasChanged;
    }
}