using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.Map.Dashboard;
using DevExpress.Persistent.Base;
using DevExpress.XtraMap;
using OutlookInspired.Module.BusinessObjects;
using OutlookInspired.Module.Features.Quotes;
using OutlookInspired.Win.Editors.Maps;
using GeoPoint = DevExpress.DashboardExport.Map.GeoPoint;

namespace OutlookInspired.Win.Features.Quotes{
    public class WinQuoteMapItemListEditorController:ObjectViewController<ListView,QuoteMapItem>{
        private MapItemListEditor _mapItemListEditor;
        private readonly MapCallout _callOut = new(){ AllowHtmlText = true };

        protected override void OnActivated(){
            base.OnActivated();
            _mapItemListEditor = View.Editor as MapItemListEditor;
            if (_mapItemListEditor == null){
                Active["editor"] = false;
                return;
            }
            _mapItemListEditor.CreateDataAdapter+=OnCreateAdapter;
            _mapItemListEditor.SelectionChanged+=MapItemListEditorOnSelectionChanged;
            _mapItemListEditor.ControlsCreated+=MapItemListEditorOnControlsCreated;
            Frame.GetController<QuoteMapItemListViewController>().StageAction.Executed+=StageActionOnExecuted;
        }

        protected override void OnDeactivated(){
            base.OnDeactivated();
            if (_mapItemListEditor == null) return;
            _mapItemListEditor.CreateDataAdapter -= OnCreateAdapter;
            _mapItemListEditor.SelectionChanged-=MapItemListEditorOnSelectionChanged;
            _mapItemListEditor.ControlsCreated-=MapItemListEditorOnControlsCreated;
            Frame.GetController<QuoteMapItemListViewController>().StageAction.Executed-=StageActionOnExecuted;
        }

        private void StageActionOnExecuted(object sender, ActionBaseEventArgs e) 
            => _mapItemListEditor.ItemsLayer.ItemStyle.Fill = ColorTranslator.FromHtml(SalesPeriod.Color());

        private void MapItemListEditorOnSelectionChanged(object sender, EventArgs e){
            var mapItem = View.SelectedObjects.Cast<QuoteMapItem>().FirstOrDefault();
            if (mapItem==null) return;
            _callOut.Location = new GeoPoint(mapItem.Latitude, mapItem.Longitude);
            var opportunity = ObjectSpace.GetObjectsQuery<QuoteMapItem>().Where(q => q.City == mapItem.City&&q.Stage==mapItem.Stage)
                .Sum(item => item.Total);
            _callOut.Text = $"TOTAL<br><color=206,113,0><b><size=+4>{opportunity}</color></size></b><br>{mapItem.City}";
        }
        
        private void MapItemListEditorOnControlsCreated(object sender, EventArgs e)
            => _mapItemListEditor.Control.Layers.Add(new VectorItemsLayer
                { Data = new MapItemStorage{ Items ={ _callOut } } });

        private void OnCreateAdapter(object sender, DataAdapterArgs e){
            _mapItemListEditor.ItemsLayer.ToolTipPattern = $"{nameof(QuoteMapItem.City)}:%A% {nameof(QuoteMapItem.Value)}:%V%";
            _mapItemListEditor.ItemsLayer.ItemStyle.Fill = ColorTranslator.FromHtml(SalesPeriod.Color());
            e.Adapter = new BubbleChartDataAdapter(){
                Mappings ={
                    Latitude = nameof(QuoteMapItem.Latitude), Longitude = nameof(QuoteMapItem.Longitude),
                    Value = nameof(QuoteMapItem.Value)
                },
                BubbleItemDataMember = nameof(QuoteMapItem.City), BubbleGroupMember = nameof(QuoteMapItem.Index)
            };
        }
        GeoPoint ToGeoPoint(IMapsMarker mapsMarker) => new(mapsMarker.Latitude, mapsMarker.Longitude);
        protected override void OnViewControlsCreated(){
            base.OnViewControlsCreated();
            _mapItemListEditor.ItemsLayer.DataLoaded+=ItemsLayerOnDataLoaded;
        }

        
        private Stage SalesPeriod => (Stage)Frame.GetController<QuoteMapItemListViewController>().StageAction.SelectedItem.Data;
        private void ItemsLayerOnDataLoaded(object sender, DataLoadedEventArgs e) 
            => ZoomTo(_mapItemListEditor.ZoomService,(IEnumerable<IMapsMarker>)((ProxyCollection)_mapItemListEditor.DataSource).OriginalCollection);
        
        void ZoomTo( IZoomToRegionService zoomService, IEnumerable<IMapsMarker> mapsMarkers, double margin = 0.25){
            var points = mapsMarkers.Select(ToGeoPoint).Where(p => p != null && !Equals(p, new GeoPoint(0, 0))).ToList();
            if (!points.Any()) return;
            ZoomTo(zoomService,new GeoPoint(points.Min(p => p.Latitude), points.Min(p => p.Longitude)),
                new GeoPoint(points.Max(p => p.Latitude), points.Max(p => p.Longitude)), margin);
        }
        
        void ZoomTo( IZoomToRegionService zoomService, GeoPoint pointA, GeoPoint pointB, double margin = 0.2){
            if (pointA == null || pointB == null || zoomService == null) return;
            var (latDiff, longDiff) = (pointB.Latitude - pointA.Latitude, pointB.Longitude - pointA.Longitude);
            var (latPad, longPad) = (CalculatePadding(margin,latDiff), CalculatePadding(margin,longDiff));
            zoomService.ZoomToRegion(new GeoPoint(pointA.Latitude - latPad, pointA.Longitude - longPad),
                new GeoPoint(pointB.Latitude + latPad, pointB.Longitude + longPad),
                new GeoPoint((pointA.Latitude + pointB.Latitude) / 2, (pointA.Longitude + pointB.Longitude) / 2));
        }
        
        static double CalculatePadding(double margin,double delta) 
            => delta > 0 ? Math.Max(0.1, delta * margin) : delta < 0 ? Math.Min(-0.1, delta * margin) : 0;
        
    }
}