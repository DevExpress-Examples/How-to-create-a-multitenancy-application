namespace OutlookInspired.Blazor.Server.Components {
    public class ProgressBarModel : DevExpress.ExpressApp.Blazor.Components.Models.ComponentModelBase {
        public int Width {
            get => GetPropertyValue<int>();
            set => SetPropertyValue(value);
        }
        public override Type ComponentType => typeof(ProgressBar);
    }
}