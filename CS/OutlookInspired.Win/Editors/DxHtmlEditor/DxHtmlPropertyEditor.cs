using System.Text;
using DevExpress.Blazor;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.Win.Editors;
using DevExpress.XtraRichEdit;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebView.WindowsForms;
using EditorAliases = OutlookInspired.Module.EditorAliases;

namespace OutlookInspired.Win.Editors.DxHtmlEditor{
    public interface IBlazorWebViewKeyDown{
        event EventHandler<KeyboardEventArgs> KeyDown;
    }

    [PropertyEditor(typeof(byte[]), EditorAliases.DxHtmlPropertyEditor, false)]  
    public class DxHtmlPropertyEditor(Type objectType, IModelMemberViewItem info) : WinPropertyEditor(objectType, info), IBlazorWebViewKeyDown{
        private static readonly RichEditDocumentServer RichEditDocumentServer = new();

        private string _markup;
        public event EventHandler<KeyboardEventArgs> KeyDown;

        protected override object CreateControlCore(){
            var blazorWebView = new BlazorWebView{
                Dock = DockStyle.Fill, HostPage = "wwwroot\\index.html", Services = ServiceProvider,
            };
            
            blazorWebView.RootComponents.Add<DevExpress.Blazor.DxHtmlEditor>("#app", new Dictionary<string, object>{
                { nameof(DevExpress.Blazor.DxHtmlEditor.Markup), _markup},
                { nameof(DevExpress.Blazor.DxHtmlEditor.MarkupChanged), EventCallback.Factory.Create<string>(this, MarkupChanged)},
                { nameof(DevExpress.Blazor.DxHtmlEditor.Height), "300px" },
                { nameof(DevExpress.Blazor.DxHtmlEditor.BindMarkupMode), HtmlEditorBindMarkupMode.OnDelayedInput },
                { "onkeydown", EventCallback.Factory.Create<KeyboardEventArgs>(this, OnKeyDown) },
            });
            return blazorWebView;
        }

        public new BlazorWebView Control => (BlazorWebView)base.Control;

        private void MarkupChanged(string newMarkup){
            _markup = newMarkup;
            OnControlValueChanged();
            WriteValueCore();
        }

        protected override void WriteValueCore(){
            var bytes = Encoding.UTF8.GetBytes($"{ControlValue}");
            RichEditDocumentServer.LoadDocument(bytes,DocumentFormat.OpenXml);
            PropertyValue = RichEditDocumentServer.OpenXmlBytes;
        }

        protected override object GetControlValueCore() => _markup;

        protected override void ReadValueCore(){
            if (PropertyValue == null){
                _markup = string.Empty;
            }
            else{
                RichEditDocumentServer.LoadDocument((byte[])PropertyValue);
                _markup = RichEditDocumentServer.Text;    
            }
            
            Control.RootComponents.First().Parameters![nameof(DevExpress.Blazor.DxHtmlEditor.Markup)] = _markup;
        }

        protected virtual void OnKeyDown(KeyboardEventArgs e) => KeyDown?.Invoke(this, e);
    }
}