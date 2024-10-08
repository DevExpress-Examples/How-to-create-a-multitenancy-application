using DevExpress.ExpressApp.Blazor.Components.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace OutlookInspired.Win.Services.Blazor{
    public class BlazorWebviewComponent : ComponentBase, IDisposable{
        [Parameter]
        public ComponentModelBase Model { get; set; }

        protected override void OnInitialized() => ((IComponentModel)Model).Changed+=ModelOnChanged;

        private void ModelOnChanged(object sender, EventArgs e) => StateHasChanged();

        protected override void BuildRenderTree(RenderTreeBuilder builder) => builder.AddContent(0, Model.GetComponentContent());

        public void Dispose() => ((IComponentModel)Model).Changed -= ModelOnChanged;
    }
}