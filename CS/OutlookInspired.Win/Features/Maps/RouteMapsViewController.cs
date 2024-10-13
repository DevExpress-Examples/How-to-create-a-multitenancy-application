using System.Text.RegularExpressions;
using DevExpress.ExpressApp;
using DevExpress.Map.Native;
using DevExpress.Persistent.Base;
using DevExpress.XtraMap;
using Microsoft.Extensions.DependencyInjection;
using OutlookInspired.Module.BusinessObjects;
using OutlookInspired.Module.Features.Maps;
using OutlookInspired.Module.Services;
using OutlookInspired.Win.Editors;
using OutlookInspired.Win.Editors.Maps;
using OutlookInspired.Win.Services;
using OutlookInspired.Win.Services.Internal;

namespace OutlookInspired.Win.Features.Maps;
public class RouteMapsViewController:ObjectViewController<DetailView,IRouteMapsMarker>{
    private  readonly Regex _removeTagRegex = new(@"<[^>]*>", RegexOptions.Compiled);
    private BingRouteDataProvider _routeDataProvider;
    private MapControl _mapControl;
    public event EventHandler<RouteCalculatedArgs> RouteCalculated;
    protected override void OnActivated(){
        base.OnActivated();
        
        View.CustomizeViewItemControl<MapControlPropertyEditor>(this, editor => {
            var bingKey = Application.ServiceProvider.GetService<IMapApiKeyProvider>().Key;
            _routeDataProvider = new BingRouteDataProvider(){ BingKey = bingKey,RouteOptions = { DistanceUnit = DistanceMeasureUnit.Mile} };
            _routeDataProvider.RouteCalculated+=OnRouteCalculated;
            _mapControl = editor.Control;
            var routeLayer = RouteLayer();
            AddRoutePoints(routeLayer);
            _mapControl.Layers.Add(routeLayer);
            CalculateRoute(BingTravelMode.Driving);
        });
    }
    
    public void CalculateRoute(BingTravelMode bingTravelMode){
        var modelHomeOffice = (((IModelOptionsHomeOffice)View.Model.Application.Options).HomeOffice);
        var centerPoint = new GeoPoint(modelHomeOffice.Latitude,modelHomeOffice.Longitude);
        _mapControl.CenterPoint = centerPoint;
        _routeDataProvider.RouteOptions.Mode=bingTravelMode;
        var mapsMarker = (IMapsMarker)View.CurrentObject;

        
        _routeDataProvider.CalculateRoute(new[]
        { new RouteWaypoint("Home Office", centerPoint), new RouteWaypoint(mapsMarker.Title,
            new GeoPoint(mapsMarker.Latitude, mapsMarker.Longitude)) }.ToList());
    }
    
    private InformationLayer RouteLayer() 
        => new(){
            DataProvider = _routeDataProvider, 
            HighlightedItemStyle ={ Stroke = Color.Cyan, StrokeWidth = 3 },
            ItemStyle = { Stroke = Color.Cyan,StrokeWidth = 3}
        };

    public void AddRoutePoints(InformationLayer routeLayer){
        routeLayer.Data.Items.Clear();
        var mapsMarker = ((IMapsMarker)View.CurrentObject);
        routeLayer.Data.Items.AddRange(new[]{
            new MapPushpin{ Text = "A", Location = _mapControl.CenterPoint },
            new MapPushpin{ Text = "B", Location = new GeoPoint(mapsMarker.Latitude,mapsMarker.Longitude) }
        });
    }

    private void OnRouteCalculated(object sender, BingRouteCalculatedEventArgs e){
        if(e.Error != null || e.Cancelled || e.CalculationResult is not{ ResultCode: RequestResultCode.Success })
            return;
        var bingRouteResult = e.CalculationResult.RouteResults.First();
        var args = new RouteCalculatedArgs(bingRouteResult.Legs.SelectMany(leg => leg.Itinerary)
            .Select(item => {
                var point = ObjectSpace.CreateObject<RoutePoint>();
                point.ManeuverInstruction = _removeTagRegex.Replace(item.ManeuverInstruction, string.Empty);
                point.Distance = (item.Distance > 0.9) ? $"{Math.Ceiling(item.Distance):0} mi"
                    : $"{Math.Ceiling(item.Distance * 52.8) * 100:0} ft";
                point.Maneuver = (Module.BusinessObjects.BingManeuverType)item.Maneuver;
                return point;
            }).ToArray(),bingRouteResult.Distance,bingRouteResult.Time,(TravelMode)_routeDataProvider.RouteOptions.Mode);
        OnRouteCalculated(args);
        var mapsMarker = ((IMapsMarker)View.CurrentObject);
        _mapControl.Zoom().To((GeoPoint)_mapControl.CenterPoint, new GeoPoint(mapsMarker.Latitude,mapsMarker.Longitude));
    }

    protected virtual void OnRouteCalculated(RouteCalculatedArgs e) => RouteCalculated?.Invoke(this, e);
}
