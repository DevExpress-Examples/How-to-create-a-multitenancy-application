using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.Win.Editors;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebView.WindowsForms;
using OutlookInspired.Module.Blazor;
using OutlookInspired.Module.Services.Internal;
using EditorAliases = OutlookInspired.Module.Services.EditorAliases;

namespace OutlookInspired.Win.Editors{
    [PropertyEditor(typeof(byte[]), EditorAliases.DxHtmlPropertyEditor, false)]  
    public class DxHtmlPropertyEditor(Type objectType, IModelMemberViewItem info) : WinPropertyEditor(objectType, info){
        private readonly MarkupContentService _contentService = new();
        private DxHtmlEditorModel _htmlEditorModel;
        public new BlazorWebView Control => (BlazorWebView)base.Control;
        
        protected override object CreateControlCore(){
            var blazorWebView = new BlazorWebView(){
                Dock = DockStyle.Fill, HostPage = "wwwroot\\index.html", Services = ServiceProvider,
            };
            blazorWebView.RootComponents.Add<MarkupRenderer>("#app", new Dictionary<string, object>{
                { nameof(MarkupRenderer.ContentService), _contentService }
            });

            return blazorWebView;
        }

        protected override void WriteValueCore() => PropertyValue = $"{ControlValue}".Bytes().ToRtfBytes();

        protected override object GetControlValueCore() => _htmlEditorModel.Markup;
        
        protected override void ReadValueCore(){
            if (PropertyValue is not byte[] { Length: > 0 }) return;
            _htmlEditorModel = (DxHtmlEditorModel)(_contentService.Model = CreateModel());
        }

        private DxHtmlEditorModel CreateModel(){
            var dxHtmlEditorModel = new DxHtmlEditorModel{
                Markup = ((byte[])PropertyValue).ToDocumentText(), Height = "300px"
            };
            dxHtmlEditorModel.MarkupChanged = EventCallback.Factory.Create<string>(this, value => {
                dxHtmlEditorModel.Markup = value;
                OnControlValueChanged();
                WriteValue();
            });
            return dxHtmlEditorModel;
        }
    }
    
}