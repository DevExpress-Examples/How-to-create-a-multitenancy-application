namespace OutlookInspired.Blazor.Server.Components{
    public interface ILabelModel{
        string Text{ get; set; }
        string Style{ get; set; }
    }

    public class LabelModel : DevExpress.ExpressApp.Blazor.Components.Models.ComponentModelBase, ILabelModel{
        public string Text{
            get => GetPropertyValue<string>();
            set => SetPropertyValue(value);
        }

        public string Style{
            get => GetPropertyValue<string>();
            set => SetPropertyValue(value);
        }

        public override Type ComponentType => typeof(Label);
    }
}