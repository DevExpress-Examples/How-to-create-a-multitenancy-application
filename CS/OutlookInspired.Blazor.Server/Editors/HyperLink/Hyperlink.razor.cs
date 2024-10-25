namespace OutlookInspired.Blazor.Server.Editors.HyperLink {

    public class HyperlinkModel : DevExpress.ExpressApp.Blazor.Components.Models.ComponentModelBase{
        public string Text {
            get => GetPropertyValue<string>();
            set => SetPropertyValue(value);
        }
        public string Style {
            get => GetPropertyValue<string>();
            set => SetPropertyValue(value);
        }
        public string Href {
            get => GetPropertyValue<string>();
            set => SetPropertyValue(value);
        }
        public override Type ComponentType => typeof(Hyperlink);
    }
}