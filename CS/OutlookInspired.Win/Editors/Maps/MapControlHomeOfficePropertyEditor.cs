using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.Win.Editors;
using DevExpress.Map.Dashboard;
using DevExpress.Persistent.Base;
using DevExpress.XtraMap;
using Microsoft.Extensions.DependencyInjection;
using OutlookInspired.Module;
using OutlookInspired.Module.BusinessObjects;
using OutlookInspired.Module.Features.Maps;
using EditorAliases = OutlookInspired.Module.EditorAliases;

namespace OutlookInspired.Win.Editors.Maps{
    [PropertyEditor(typeof(Location),EditorAliases.MapHomeOfficePropertyEditor)]
    public class MapControlHomeOfficePropertyEditor(Type objectType, IModelMemberViewItem model)
        : WinPropertyEditor(objectType, model){
        private ImageLayer _imageLayer;
        private BingRouteDataProvider _routeDataProvider;
        private GeoPoint _homeOfficePoint;
        private MapControl _mapControl;

        protected override object CreateControlCore(){
            var bingKey = ServiceProvider.GetService<IMapApiKeyProvider>().Key;
            _mapControl = new MapControl();
            _imageLayer = new ImageLayer{ DataProvider =new BingMapDataProvider(){ BingKey = bingKey,Kind = BingMapKind.Road} };
            _imageLayer.Error+=ImageLayerOnError;
            _mapControl.Layers.Add(_imageLayer);
            _mapControl.Layers.AddRange(new LayerBase[]{
                new InformationLayer{ DataProvider = new BingGeocodeDataProvider(){BingKey =bingKey } },
                new InformationLayer{ DataProvider = new BingSearchDataProvider(){BingKey = bingKey} },
                
            });
            var modelHomeOffice = (((IModelOptionsHomeOffice)View.Model.Application.Options).HomeOffice);
            _homeOfficePoint = new GeoPoint(modelHomeOffice.Latitude,modelHomeOffice.Longitude);
            _routeDataProvider = new BingRouteDataProvider(){ BingKey = bingKey,RouteOptions = { DistanceUnit = DistanceMeasureUnit.Mile} };
            var routeLayer = RouteLayer();
            AddRoutePoints(routeLayer);
            _mapControl.Layers.Add(routeLayer);
            _routeDataProvider.RouteCalculated+=RouteDataProviderOnRouteCalculated;
            CalculateRoute(BingTravelMode.Driving);
            return _mapControl;
        }

        private void RouteDataProviderOnRouteCalculated(object sender, BingRouteCalculatedEventArgs e){
            var mapsMarker = ((IMapsMarker)View.CurrentObject);
            var zoomToRegionService = (IZoomToRegionService)((IServiceProvider)_mapControl).GetService(typeof(IZoomToRegionService));
            ZoomTo(zoomToRegionService,_homeOfficePoint, new GeoPoint(mapsMarker.Latitude,mapsMarker.Longitude));
        }

        void ZoomTo(IZoomToRegionService zoomService, GeoPoint pointA, GeoPoint pointB, double margin = 0.2){
            if (pointA == null || pointB == null || zoomService == null) return;
            var (latDiff, longDiff) = (pointB.Latitude - pointA.Latitude, pointB.Longitude - pointA.Longitude);
            var (latPad, longPad) = (CalculatePadding(margin,latDiff), CalculatePadding(margin,longDiff));
            zoomService.ZoomToRegion(new GeoPoint(pointA.Latitude - latPad, pointA.Longitude - longPad),
                new GeoPoint(pointB.Latitude + latPad, pointB.Longitude + longPad),
                new GeoPoint((pointA.Latitude + pointB.Latitude) / 2, (pointA.Longitude + pointB.Longitude) / 2));
        }
        
        static double CalculatePadding(double margin,double delta) 
            => delta > 0 ? Math.Max(0.1, delta * margin) : delta < 0 ? Math.Min(-0.1, delta * margin) : 0;

        public void CalculateRoute(BingTravelMode bingTravelMode){
            _routeDataProvider.RouteOptions.Mode=bingTravelMode;
            var mapsMarker = (IMapsMarker)View.CurrentObject;
            _routeDataProvider.CalculateRoute(new[]
            { new RouteWaypoint("Home Office", _homeOfficePoint), new RouteWaypoint(mapsMarker.Title,
                new GeoPoint(mapsMarker.Latitude, mapsMarker.Longitude)) }.ToList());
        }

        public void AddRoutePoints(InformationLayer routeLayer){
            routeLayer.Data.Items.Clear();
            var mapsMarker = ((IMapsMarker)View.CurrentObject);
            routeLayer.Data.Items.AddRange(new[]{
                new MapPushpin{ Text = "A", Location = _homeOfficePoint },
                new MapPushpin{ Text = "B", Location = new GeoPoint(mapsMarker.Latitude,mapsMarker.Longitude) }
            });
        }


        private InformationLayer RouteLayer() 
            => new(){
                DataProvider = _routeDataProvider, 
                HighlightedItemStyle ={ Stroke = Color.Cyan, StrokeWidth = 3 },
                ItemStyle = { Stroke = Color.Cyan,StrokeWidth = 3}
            };

        public override void BreakLinksToControl(bool unwireEventsOnly){
            base.BreakLinksToControl(unwireEventsOnly);
            if (_imageLayer != null) _imageLayer.Error -= ImageLayerOnError;
        }

        public new MapControl Control => (MapControl)base.Control;

        private void ImageLayerOnError(object sender, MapErrorEventArgs e) => throw new AggregateException(e.Exception.Message, e.Exception);

        
    }
}