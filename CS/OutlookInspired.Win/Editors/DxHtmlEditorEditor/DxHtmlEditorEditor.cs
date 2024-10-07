using DevExpress.Blazor;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.Win.Editors;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebView.WindowsForms;
using OutlookInspired.Module.Blazor;
using OutlookInspired.Module.Services.Internal;
using OutlookInspired.Win.Services.Blazor;
using EditorAliases = OutlookInspired.Module.Services.EditorAliases;

namespace OutlookInspired.Win.Editors.DxHtmlEditorEditor{
    public interface IBlazorWebViewKeydown{
        event EventHandler<KeyboardEventArgs> KeyDown;
    }

    [PropertyEditor(typeof(byte[]), EditorAliases.DxHtmlPropertyEditor, false)]  
    public class DxHtmlPropertyEditor(Type objectType, IModelMemberViewItem info) : WinPropertyEditor(objectType, info), IBlazorWebViewKeydown{
        public event EventHandler<KeyboardEventArgs> KeyDown;
        private readonly ContentService _contentService = new();
        private DxHtmlEditorModel _htmlEditorModel;

        protected override object CreateControlCore(){
            var blazorWebView = new BlazorWebView(){
                Dock = DockStyle.Fill, HostPage = "wwwroot\\index.html", Services = ServiceProvider,
            };
            blazorWebView.RootComponents.Add<ContentRenderer>("#app", new Dictionary<string, object>{
                { nameof(ContentRenderer.ContentService), _contentService }
            });
            return blazorWebView;
        }
        
        protected override void WriteValueCore() => PropertyValue = $"{ControlValue}".Bytes().ToRtfBytes();

        protected override object GetControlValueCore() => _htmlEditorModel.Markup;
        
        protected override void ReadValueCore() => _htmlEditorModel = (DxHtmlEditorModel)(_contentService.Model = CreateModel());

        private DxHtmlEditorModel CreateModel(){
            var dxHtmlEditorModel = new DxHtmlEditorModel{
                Markup = ((byte[])PropertyValue).ToDocumentText(), Height = "300px"
            };
            dxHtmlEditorModel.SetAttribute("onkeydown", EventCallback.Factory.Create<KeyboardEventArgs>(this, OnKeyDown));
            dxHtmlEditorModel.SetAttribute(nameof(DxHtmlEditor.BindMarkupMode), HtmlEditorBindMarkupMode.OnDelayedInput);
            
            dxHtmlEditorModel.MarkupChanged = EventCallback.Factory.Create<string>(this, value => {
                dxHtmlEditorModel.Markup = value;
                OnControlValueChanged();
                WriteValue();
            });
            return dxHtmlEditorModel;
        }

        protected virtual void OnKeyDown(KeyboardEventArgs e) => KeyDown?.Invoke(this, e);
    }
}