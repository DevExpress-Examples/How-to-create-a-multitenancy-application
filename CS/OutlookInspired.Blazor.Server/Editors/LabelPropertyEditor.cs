using DevExpress.ExpressApp.Blazor.Editors;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Model;
using OutlookInspired.Blazor.Server.Components;
using OutlookInspired.Blazor.Server.Services.Internal;
using OutlookInspired.Module.Services.Internal;
using EditorAliases = OutlookInspired.Module.Services.EditorAliases;

namespace OutlookInspired.Blazor.Server.Editors
{
    [PropertyEditor(typeof(object), EditorAliases.LabelPropertyEditor, false)]
    public class LabelPropertyEditor : BlazorPropertyEditorBase {
        public LabelPropertyEditor(Type objectType, IModelMemberViewItem model) : base(objectType, model) { }
        public override LabelModel ComponentModel => (LabelModel)base.ComponentModel;
        protected override LabelModel CreateComponentModel() {
            var labelModel = new LabelModel();
            labelModel.Style = MemberInfo.FontSize();
            return labelModel;
        }
        protected override void ReadValueCore() {
            base.ReadValueCore();
            ComponentModel.Text = PropertyValue is byte[] bytes ? bytes.ToDocumentText() : $"{PropertyValue}";
        }
        protected override object GetControlValueCore() => ComponentModel.Text;
    }
}