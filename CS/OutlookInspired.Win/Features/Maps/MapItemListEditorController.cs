using DevExpress.ExpressApp;
using DevExpress.XtraMap;
using OutlookInspired.Module.BusinessObjects;
using OutlookInspired.Win.Editors.Maps;
using MapItem = OutlookInspired.Module.BusinessObjects.MapItem;

namespace OutlookInspired.Win.Features.Maps{
    public class MapItemListEditorController:ObjectViewController<ListView,MapItem>{
        private MapItemListEditor _mapItemListEditor;

        protected override void OnActivated(){
            base.OnActivated();
            _mapItemListEditor = View.Editor as MapItemListEditor;
            if (_mapItemListEditor == null){
                Active["editor"] = false;
                return;
            }
            _mapItemListEditor.CreateDataAdapter+=OnCreateAdapter;
        }

        protected override void OnDeactivated(){
            base.OnDeactivated();
            if (_mapItemListEditor != null) _mapItemListEditor.CreateDataAdapter -= OnCreateAdapter;
        }

        private string GetPieSegmentPropertyName(){
            var isCustomer = ((PropertyCollectionSource)View.CollectionSource).MasterObject is Customer;
            return isCustomer ? nameof(MapItem.ProductName) : nameof(MapItem.CustomerName);
        }

        private void OnCreateAdapter(object sender, DataAdapterArgs e){
            _mapItemListEditor.ItemsLayer.ToolTipPattern = $"{nameof(MapItem.City)}:%A% {nameof(MapItem.Total)}:%V%";
            e.Adapter = new PieChartDataAdapter(){
                Mappings ={
                    Latitude = nameof(MapItem.Latitude), Longitude = nameof(MapItem.Longitude),
                    PieSegment = GetPieSegmentPropertyName(), Value = nameof(MapItem.Total)
                },
                PieItemDataMember = nameof(MapItem.City), SummaryFunction = SummaryFunction.Sum,
            };
        }

        protected override void OnViewControlsCreated(){
            base.OnViewControlsCreated();
            _mapItemListEditor.ItemsLayer.DataLoaded+=ItemsLayerOnDataLoaded;
        }

        private void ItemsLayerOnDataLoaded(object sender, DataLoadedEventArgs e){
            var items = ((ProxyCollection)_mapItemListEditor.DataSource).Cast<IMapItem>().ToArray();
            double[] bounds = MapItem.GetBounds(items);
            ZoomTo(new GeoPoint(bounds[1], bounds[0]), new GeoPoint(bounds[3], bounds[2]));
        }
        
        void ZoomTo(GeoPoint pointA, GeoPoint pointB, double margin = 0.2){
            if (pointA == null || pointB == null || _mapItemListEditor.ZoomService == null) return;
            var (latDiff, longDiff) = (pointB.Latitude - pointA.Latitude, pointB.Longitude - pointA.Longitude);
            var (latPad, longPad) = (CalculatePadding(margin,latDiff), CalculatePadding(margin,longDiff));
            _mapItemListEditor.ZoomService.ZoomToRegion(new GeoPoint(pointA.Latitude - latPad, pointA.Longitude - longPad),
                new GeoPoint(pointB.Latitude + latPad, pointB.Longitude + longPad),
                new GeoPoint((pointA.Latitude + pointB.Latitude) / 2, (pointA.Longitude + pointB.Longitude) / 2));
        }
        
        static double CalculatePadding(double margin,double delta) 
            => delta > 0 ? Math.Max(0.1, delta * margin) : delta < 0 ? Math.Min(-0.1, delta * margin) : 0;

    }
}