using DevExpress.Blazor;
using DevExpress.ExpressApp.Blazor.Components.Models;
using DevExpress.ExpressApp.Blazor.Editors;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Model;
using Microsoft.AspNetCore.Components;
using OutlookInspired.Module.Services.Internal;
using EditorAliases = OutlookInspired.Module.Services.EditorAliases;

namespace OutlookInspired.Blazor.Server.Editors{
    [PropertyEditor(typeof(byte[]),EditorAliases.DxHtmlPropertyEditor, false)]
    public class DxHtmlPropertyEditor(Type objectType, IModelMemberViewItem model)
        : BlazorPropertyEditorBase(objectType, model){
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

        protected override void WriteValueCore() => PropertyValue = $"{ControlValue}".Bytes().ToRtfBytes();

        protected override void ReadValueCore() {
            base.ReadValueCore();
            ComponentModel.Markup = ((byte[])PropertyValue).ToDocumentText();
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