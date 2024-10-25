using DevExpress.ExpressApp.Blazor.Components;
using DevExpress.ExpressApp.Blazor.Editors;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.Utils;
using Microsoft.AspNetCore.Components;
using EditorAliases = OutlookInspired.Module.EditorAliases;

namespace OutlookInspired.Blazor.Server.Editors {
    [PropertyEditor(typeof(Enum), EditorAliases.EnumImageOnlyEditor, false)]
    public class EnumPropertyEditor(Type objectType, IModelMemberViewItem model)
        : DevExpress.ExpressApp.Blazor.Editors.EnumPropertyEditor(objectType, model){
        protected override RenderFragment CreateViewComponentCore(object dataContext){
            var propertyValue = ((Enum)this.GetPropertyValue(dataContext));
            return ComboBoxIconItem.Create(null, ImageLoader.Instance.GetEnumValueImageName(propertyValue));
        }
    }
}