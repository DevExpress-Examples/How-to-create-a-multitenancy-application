using DevExpress.Blazor;
using DevExpress.ExpressApp.Blazor.Components.Models;
using Microsoft.AspNetCore.Components;

namespace OutlookInspired.Module.Blazor{
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