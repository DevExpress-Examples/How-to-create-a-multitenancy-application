using DevExpress.ExpressApp.Blazor.Components.Models;
using DevExpress.ExpressApp.Blazor.Editors;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Model;
using Microsoft.AspNetCore.Components;
using OutlookInspired.Blazor.Server.Components;
using EditorAliases = OutlookInspired.Module.Services.EditorAliases;

namespace OutlookInspired.Blazor.Server.Editors {
    [PropertyEditor(typeof(int), EditorAliases.ProgressEditor, false)]
    [PropertyEditor(typeof(double), EditorAliases.ProgressEditor, false)]
    public class ProgressPropertyEditor : BlazorPropertyEditorBase {
        public override ProgressBarModel ComponentModel => (ProgressBarModel)base.ComponentModel;
        protected override IComponentModel CreateComponentModel() => new ProgressBarModel();
        private int ConvertPropertyValue(object propertyValue) => Convert.ToInt32(propertyValue is double doubleValue ? doubleValue * 100 : propertyValue ?? 0);
        protected override void ReadValueCore() {
            base.ReadValueCore();
            ComponentModel.Width = ConvertPropertyValue(PropertyValue);
        }
        protected override object GetControlValueCore() => ComponentModel.Width;
        protected override RenderFragment CreateViewComponentCore(object dataContext) {
            var propertyValue = this.GetPropertyValue(dataContext);
            var componentModel = new ProgressBarModel() { Width = ConvertPropertyValue(propertyValue) };
            return componentModel.GetComponentContent();
        }
        public ProgressPropertyEditor(Type objectType, IModelMemberViewItem model) : base(objectType, model) { }
    }
}