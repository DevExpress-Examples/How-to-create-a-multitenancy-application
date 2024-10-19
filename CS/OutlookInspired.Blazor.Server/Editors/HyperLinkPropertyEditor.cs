using DevExpress.ExpressApp.Blazor.Components.Models;
using DevExpress.ExpressApp.Blazor.Editors;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Model;
using Microsoft.AspNetCore.Components;
using OutlookInspired.Blazor.Server.Components;
using EditorAliases = OutlookInspired.Module.Services.EditorAliases;

namespace OutlookInspired.Blazor.Server.Editors {
    [PropertyEditor(typeof(string), EditorAliases.HyperLinkPropertyEditor, false)]
    public class HyperLinkPropertyEditor : StringPropertyEditor {
        public HyperLinkPropertyEditor(Type objectType, IModelMemberViewItem model) : base(objectType, model) { }
        protected override RenderFragment CreateViewComponentCore(object dataContext) {
            var displayValue = this.GetPropertyDisplayValue(dataContext);
            var hyperLinkModel = new HyperlinkModel {
                Text = displayValue,
                Href = $"mailto:{displayValue}"
            };
            return hyperLinkModel.GetComponentContent();
        }
    }
}