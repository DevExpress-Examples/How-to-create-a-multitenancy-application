namespace OutlookInspired.Blazor.Server.Components {
    public interface IHyperlinkModel{
        string Text{ get; set; }
        string Style{ get; set; }
        string Href{ get; set; }
    }

    public class HyperlinkModel : DevExpress.ExpressApp.Blazor.Components.Models.ComponentModelBase, IHyperlinkModel{
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
        public override Type ComponentType => typeof(Components.Hyperlink);
    }
}