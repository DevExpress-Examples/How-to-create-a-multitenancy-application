using DevExpress.ExpressApp.Blazor.Editors;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Model;
using OutlookInspired.Blazor.Server.Services.Internal;
using OutlookInspired.Module.Services.Internal;
using EditorAliases = OutlookInspired.Module.Services.EditorAliases;

namespace OutlookInspired.Blazor.Server.Editors.Label{
    [PropertyEditor(typeof(object), EditorAliases.LabelPropertyEditor, false)]
    public class LabelPropertyEditor(Type objectType, IModelMemberViewItem model)
        : BlazorPropertyEditorBase(objectType, model){
        public override LabelModel ComponentModel => (LabelModel)base.ComponentModel;
        protected override LabelModel CreateComponentModel() 
            => new(){ Style = MemberInfo.FontSize() };

        protected override void ReadValueCore() {
            base.ReadValueCore();
            ComponentModel.Text = PropertyValue is byte[] bytes ? bytes.ToDocumentText() : $"{PropertyValue}";
        }
        protected override object GetControlValueCore() => ComponentModel.Text;
    }
}