namespace OutlookInspired.Blazor.Server.Editors.Label{

    public class LabelModel : DevExpress.ExpressApp.Blazor.Components.Models.ComponentModelBase{
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