using System.Text.RegularExpressions;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.Win.Editors;
using DevExpress.Map.Dashboard;
using DevExpress.Map.Native;
using DevExpress.Persistent.Base;
using DevExpress.XtraMap;
using OutlookInspired.Module.BusinessObjects;
using OutlookInspired.Module.Features.Maps;
using OutlookInspired.Win.Services.Internal;
using BingManeuverType = OutlookInspired.Module.BusinessObjects.BingManeuverType;
using EditorAliases = OutlookInspired.Module.Services.EditorAliases;

namespace OutlookInspired.Win.Editors{
    [PropertyEditor(typeof(Location),EditorAliases.MapRoutePropertyEditor)]
    public class MapControlRoutePropertyEditor(Type objectType, IModelMemberViewItem model)
        : WinPropertyEditor(objectType, model),IComplexViewItem{
        public event EventHandler<RouteCalculatedArgs> RouteCalculated;
        private  readonly Regex _removeTagRegex = new(@"<[^>]*>", RegexOptions.Compiled);
        readonly BingMapDataProvider _mapDataProvider=new(){ BingKey = MapsViewController.BindKey,Kind = BingMapKind.Road};
        private readonly BingGeocodeDataProvider _geocodeDataProvider=new(){BingKey = MapsViewController.BindKey};
        private readonly BingRouteDataProvider _routeDataProvider=new(){BingKey = MapsViewController.BindKey,RouteOptions = { DistanceUnit = DistanceMeasureUnit.Mile}};
        private readonly BingSearchDataProvider _searchDataProvider=new(){BingKey = MapsViewController.BindKey};
        private ImageLayer _imageLayer;
        private IObjectSpace _objectSpace;
        private IZoomToRegionService _zoom;

        protected override object CreateControlCore(){
            _routeDataProvider.RouteCalculated+=OnRouteCalculated;
            var mapControl = new MapControl();
            
            _imageLayer = new ImageLayer{ DataProvider = _mapDataProvider };
            _imageLayer.Error+=ImageLayerOnError;
            mapControl.Layers.Add(_imageLayer);
            mapControl.CenterPoint = CenterPoint();
            mapControl.Layers.AddRange(new LayerBase[]{
                new InformationLayer{ DataProvider = _geocodeDataProvider },
                new InformationLayer{ DataProvider = _searchDataProvider },
                RouteLayer(mapControl)
            });
            _zoom= (IZoomToRegionService)((IServiceProvider)mapControl).GetService(typeof(IZoomToRegionService));
            CalculateRoute(BingTravelMode.Driving);
            return mapControl;
        }

        private GeoPoint CenterPoint() => ((IModelOptionsHomeOffice)Model.Application.Options).HomeOffice.ToGeoPoint();


        private void OnRouteCalculated(object sender, BingRouteCalculatedEventArgs e){
            if(e.Error != null || e.Cancelled || e.CalculationResult is not{ ResultCode: RequestResultCode.Success })
                return;
            var bingRouteResult = e.CalculationResult.RouteResults.First();
            var args = new RouteCalculatedArgs(bingRouteResult.Legs.SelectMany(leg => leg.Itinerary)
                .Select(item => {
                    var point = _objectSpace.CreateObject<RoutePoint>();
                    point.ManeuverInstruction = _removeTagRegex.Replace(item.ManeuverInstruction, string.Empty);
                    point.Distance = (item.Distance > 0.9) ? $"{Math.Ceiling(item.Distance):0} mi"
                        : $"{Math.Ceiling(item.Distance * 52.8) * 100:0} ft";
                    point.Maneuver = (BingManeuverType)item.Maneuver;
                    return point;
                }).ToArray(),bingRouteResult.Distance,bingRouteResult.Time,(TravelMode)_routeDataProvider.RouteOptions.Mode);
            OnRouteCalculated(args);
            _zoom.To((GeoPoint)Control.CenterPoint, ((IMapsMarker)CurrentObject).ToGeoPoint());
        }

        public override void BreakLinksToControl(bool unwireEventsOnly){
            base.BreakLinksToControl(unwireEventsOnly);
            if (_imageLayer != null) _imageLayer.Error -= ImageLayerOnError;
            _routeDataProvider.RouteCalculated-=OnRouteCalculated;
        }

        public void CalculateRoute(BingTravelMode bingTravelMode){
            _routeDataProvider.RouteOptions.Mode=bingTravelMode;
            var mapsMarker = (IMapsMarker)CurrentObject;
            _routeDataProvider.CalculateRoute(new[]
                { new RouteWaypoint("Home Office", CenterPoint()), new RouteWaypoint(mapsMarker.Title,
                    mapsMarker.ToGeoPoint()) }.ToList());
        }

        
        
        private InformationLayer RouteLayer(MapControl mapControl){
            var routeLayer = new InformationLayer{
                DataProvider = _routeDataProvider, 
                HighlightedItemStyle ={ Stroke = Color.Cyan, StrokeWidth = 3 },
                ItemStyle = { Stroke = Color.Cyan,StrokeWidth = 3}
            };
            AddRoutePoints(routeLayer,mapControl);
            return routeLayer;
        }

        public new MapControl Control => (MapControl)base.Control;
        public void AddRoutePoints(InformationLayer routeLayer, MapControl mapControl){
            routeLayer.Data.Items.Clear();
            routeLayer.Data.Items.AddRange(new[]{
                new MapPushpin{ Text = "A", Location = mapControl.CenterPoint },
                new MapPushpin{ Text = "B", Location = ((IMapsMarker)CurrentObject).ToGeoPoint() }
            });
        }

        private void ImageLayerOnError(object sender, MapErrorEventArgs e) => throw new AggregateException(e.Exception.Message, e.Exception);

        public void Setup(IObjectSpace objectSpace, XafApplication application) => _objectSpace = objectSpace;

        protected virtual void OnRouteCalculated(RouteCalculatedArgs e) => RouteCalculated?.Invoke(this, e);
    }
}