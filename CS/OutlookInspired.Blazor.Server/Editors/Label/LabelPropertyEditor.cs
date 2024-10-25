using DevExpress.ExpressApp.Blazor.Editors;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Model;
using DevExpress.XtraRichEdit;
using OutlookInspired.Module.Attributes;
using EditorAliases = OutlookInspired.Module.EditorAliases;

namespace OutlookInspired.Blazor.Server.Editors.Label{
    [PropertyEditor(typeof(object), EditorAliases.LabelPropertyEditor, false)]
    public class LabelPropertyEditor(Type objectType, IModelMemberViewItem model)
        : BlazorPropertyEditorBase(objectType, model){
        private static readonly RichEditDocumentServer RichEditDocumentServer = new();
        public override LabelModel ComponentModel => (LabelModel)base.ComponentModel;
        protected override LabelModel CreateComponentModel() 
            => new(){ Style = MemberInfo.FindAttribute<FontSizeDeltaAttribute>()?.Style() };

        protected override void ReadValueCore() {
            base.ReadValueCore();
            switch (PropertyValue){
                case null:
                    return;
                case byte[] bytes:{
                    using var memoryStream = new MemoryStream((bytes));
                    RichEditDocumentServer.LoadDocument(memoryStream,DocumentFormat.OpenXml);
                    ComponentModel.Text = RichEditDocumentServer.Text;
                    break;
                }
                default:
                    ComponentModel.Text = $"{PropertyValue}";
                    break;
            }
        }
        protected override object GetControlValueCore() => ComponentModel.Text;
    }
}