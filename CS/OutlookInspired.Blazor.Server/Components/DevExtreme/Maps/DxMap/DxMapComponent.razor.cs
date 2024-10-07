namespace OutlookInspired.Blazor.Server.Components.DevExtreme.Maps.DxMap{
    public class DxMapModel : ComponentModelBase {
        
        public override Type ComponentType => typeof(DevExpress.Blazor.DxMap);

        public Dictionary<string,DxMarker> Markers{
            get => GetPropertyValue<Dictionary<string, DxMarker>>();
            set => SetPropertyValue(value);
        }
        // public Dictionary<string, Marker> Markers{ get; set; }
    }

    public class DxMarker{
    }
}