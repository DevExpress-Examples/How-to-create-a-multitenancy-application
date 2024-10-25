using DevExpress.ExpressApp.Blazor.Components.Models;
using DevExpress.ExpressApp.Blazor.Editors;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Model;
using Microsoft.AspNetCore.Components;
using EditorAliases = OutlookInspired.Module.EditorAliases;

namespace OutlookInspired.Blazor.Server.Editors.HyperLink {
    [PropertyEditor(typeof(string), EditorAliases.HyperLinkPropertyEditor, false)]
    public class HyperLinkPropertyEditor(Type objectType, IModelMemberViewItem model)
        : StringPropertyEditor(objectType, model){
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