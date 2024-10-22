using DevExpress.Blazor;
using DevExpress.ExpressApp.Blazor.Components.Models;
using DevExpress.Persistent.Base;

namespace OutlookInspired.Blazor.Server.Editors.Maps{
    public class DxMapModel : ComponentModelBase {
        public override Type ComponentType => typeof(DxMapComponent);

        public Dictionary<string,IMapsMarker> Markers{
            get => GetPropertyValue<Dictionary<string, IMapsMarker>>();
            set => SetPropertyValue(value);
        }
        public IMapsMarker Center{
            get => GetPropertyValue<IMapsMarker>();
            set => SetPropertyValue(value);
        }
        public MapRouteMode MapRouteMode{
            get => GetPropertyValue<MapRouteMode>();
            set => SetPropertyValue(value);
        }
    }
}