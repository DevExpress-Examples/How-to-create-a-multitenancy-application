using System.Text;
using DevExpress.Blazor;
using DevExpress.ExpressApp.Blazor.Components.Models;
using DevExpress.ExpressApp.Blazor.Editors;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Model;
using DevExpress.XtraRichEdit;
using Microsoft.AspNetCore.Components;
using EditorAliases = OutlookInspired.Module.EditorAliases;

namespace OutlookInspired.Blazor.Server.Editors{
    [PropertyEditor(typeof(byte[]),EditorAliases.DxHtmlPropertyEditor, false)]
    public class DxHtmlPropertyEditor(Type objectType, IModelMemberViewItem model)
        : BlazorPropertyEditorBase(objectType, model){
        private static readonly RichEditDocumentServer RichEditDocumentServer = new();
        public override DxHtmlEditorModel ComponentModel => (DxHtmlEditorModel)base.ComponentModel;

        protected override IComponentModel CreateComponentModel() {
            var model = new DxHtmlEditorModel() {
                Height = "300px",
            };

            model.MarkupChanged = EventCallback.Factory.Create<string>(this, value => {
                model.Markup = value;
                OnControlValueChanged();
                WriteValue();
            });
            
            return model;
        }
        
        protected override void WriteValueCore(){
            var bytes = Bytes($"{ControlValue}");
            using var memoryStream = new MemoryStream(bytes);
            RichEditDocumentServer.LoadDocument(memoryStream,DocumentFormat.OpenXml);
            PropertyValue = RichEditDocumentServer.OpenXmlBytes;
        }

        byte[] Bytes( string s, Encoding encoding = null) 
            => s == null ? [] : (encoding ?? Encoding.UTF8).GetBytes(s);

        protected override void ReadValueCore() {
            base.ReadValueCore();
            if (PropertyValue==null)return;
            using var memoryStream = new MemoryStream(((byte[])PropertyValue));
            RichEditDocumentServer.LoadDocument(memoryStream,DocumentFormat.OpenXml);
            ComponentModel.Markup = RichEditDocumentServer.Text;
        }

        protected override object GetControlValueCore() => ComponentModel.Markup;
    }

 
    public class DxHtmlEditorModel : ComponentModelBase {
        public override Type ComponentType => typeof(DxHtmlEditor);

        public string Markup {
            get => GetPropertyValue<string>();
            set => SetPropertyValue(value);
        }
        
        public string Height {
            get => GetPropertyValue<string>();
            set => SetPropertyValue(value);
        }

        public EventCallback<string> MarkupChanged {
            get => GetPropertyValue<EventCallback<string>>();
            set => SetPropertyValue(value);
        }
        
    }

}