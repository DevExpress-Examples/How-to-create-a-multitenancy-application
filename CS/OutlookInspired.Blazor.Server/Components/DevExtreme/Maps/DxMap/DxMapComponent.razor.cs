using DevExpress.Blazor;
using DevExpress.Persistent.Base;
using Microsoft.AspNetCore.Components;
using OutlookInspired.Module.Features.Maps;

namespace OutlookInspired.Blazor.Server.Components.DevExtreme.Maps.DxMap{
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
        public bool CalculateRoute{
            get => GetPropertyValue<bool>();
            set => SetPropertyValue(value);
        }
        public EventCallback<RouteCalculatedArgs> RouteCalculated{
            get => GetPropertyValue<EventCallback<RouteCalculatedArgs>>();
            set => SetPropertyValue(value);
        }
        
        
    }
}