using DevExpress.ExpressApp.Blazor.Components;
using DevExpress.ExpressApp.Blazor.Editors;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Model;
using Microsoft.AspNetCore.Components;
using OutlookInspired.Module.Services.Internal;
using EditorAliases = OutlookInspired.Module.Services.EditorAliases;

namespace OutlookInspired.Blazor.Server.Editors {
    [PropertyEditor(typeof(Enum), EditorAliases.EnumImageOnlyEditor, false)]
    public class EnumPropertyEditor(Type objectType, IModelMemberViewItem model)
        : DevExpress.ExpressApp.Blazor.Editors.EnumPropertyEditor(objectType, model){
        protected override RenderFragment CreateViewComponentCore(object dataContext)
            => ComboBoxIconItem.Create(null, ((Enum)this.GetPropertyValue(dataContext))?.ImageName());
    }
}