using DevExpress.ExpressApp.Actions;
using DevExpress.Persistent.Base;
using OutlookInspired.Blazor.Server.Components.DevExtreme.Maps;
using OutlookInspired.Blazor.Server.Services.Internal;
using OutlookInspired.Module.Features.Maps;
using OutlookInspired.Module.Services.Internal;

namespace OutlookInspired.Blazor.Server.Features.Maps{
    public abstract class RouteMapsViewController<T>:BlazorMapsViewController<T,DxrMapModel,DxrMap>,IMapsRouteController where T:IRouteMapsMarker{
        protected override void OnDeactivated(){
            base.OnDeactivated();
            if (!Active)return;
            MapsViewController.TravelModeAction.Executed-=TravelModeActionOnExecuted;
        }

        protected override void OnActivated(){
            base.OnActivated();
            if(!Active)return;
            MapsViewController.TravelModeAction.Executed+=TravelModeActionOnExecuted;
        }

        protected override DxrMapModel CustomizeModel(DxrMapModel model){
            var dxMapOptions = model.Options = ((IMapsMarker)View.CurrentObject).DxMapOptions(
                ((IModelOptionsHomeOffice)Application.Model.Options).HomeOffice,
                (string)Frame.GetController<MapsViewController>().TravelModeAction.SelectedItem.Data);
            CalculateRoute(dxMapOptions);
            return model;
        }

        private void CalculateRoute(DxMapOptions options) 
            => this.Await(async () => {
                var routeCalculatedArgs = await ObjectSpace.ManeuverInstructions(
                    options.Markers.First().Location, options.Markers.Last().Location, options.Routes.First().Mode,
                    options.ApiKey.Bing);
                OnRouteCalculated(routeCalculatedArgs);
            });
        
        private void TravelModeActionOnExecuted(object sender, ActionBaseEventArgs e) 
            => CustomizeModel().RouteMode = ((string)Frame.GetController<MapsViewController>().TravelModeAction.SelectedItem.Data).ToLower();

        public event EventHandler<RouteCalculatedArgs> RouteCalculated;

        protected virtual void OnRouteCalculated(RouteCalculatedArgs e) 
            => RouteCalculated?.Invoke(this, e);
    }
}