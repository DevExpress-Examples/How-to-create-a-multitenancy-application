using DevExpress.ExpressApp;
using DevExpress.Map.Dashboard;
using DevExpress.Persistent.Base;
using DevExpress.XtraMap;
using OutlookInspired.Module;
using OutlookInspired.Module.BusinessObjects;
using OutlookInspired.Win.Editors.Maps;
using GeoPoint = DevExpress.DashboardExport.Map.GeoPoint;

namespace OutlookInspired.Win.Features.Quotes{
    public class QuoteMapItemListEditorController:ObjectViewController<ListView,QuoteMapItem>{
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
        }

        protected override void OnDeactivated(){
            base.OnDeactivated();
            if (_mapItemListEditor == null) return;
            _mapItemListEditor.CreateDataAdapter -= OnCreateAdapter;
            _mapItemListEditor.SelectionChanged-=MapItemListEditorOnSelectionChanged;
            _mapItemListEditor.ControlsCreated-=MapItemListEditorOnControlsCreated;
        }

        private void MapItemListEditorOnSelectionChanged(object sender, EventArgs e){
            var mapItem = View.SelectedObjects.Cast<QuoteMapItem>().FirstOrDefault();
            if (mapItem==null) return;
            _callOut.Location = new GeoPoint(mapItem.Latitude, mapItem.Longitude);
            _callOut.Text = OpportunityCallout(mapItem);
        }

        string OpportunityCallout(QuoteMapItem item) 
            => $"TOTAL<br><color=206,113,0><b><size=+4>{Opportunity(item.Stage, item.City)}</color></size></b><br>{item.City}";
        
        decimal Opportunity(Stage stage,string city)    
            => Quotes(stage).Where(q => q.CustomerStore.City == city).Sum(q => q.Total);

        public static IQueryable<Quote> Quotes( Stage stage,string criteria=null) 
            // => ((IQueryable<Quote>)((EFCoreObjectSpace)objectSpace).Query(typeof(Quote), criteria)).Where(stage);
            => throw new NotImplementedException();
        private void MapItemListEditorOnControlsCreated(object sender, EventArgs e)
            => _mapItemListEditor.Control.Layers.Add(new VectorItemsLayer
                { Data = new MapItemStorage{ Items ={ _callOut } } });

        private void OnCreateAdapter(object sender, DataAdapterArgs e){
            _mapItemListEditor.ItemsLayer.ToolTipPattern = $"{nameof(QuoteMapItem.City)}:%A% {nameof(QuoteMapItem.Value)}:%V%";
            var quote = ((Quote)((PropertyCollectionSource)View.CollectionSource).MasterObject);
            throw new NotImplementedException();
            // _mapItemListEditor.ItemsLayer.ItemStyle.Fill = quote.PaletteEntries[Array.IndexOf(Enum.GetValues(typeof(Stage)), quote.Stage)].Color;
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

        // private Stage SalesPeriod => ((Quote)((PropertyCollectionSource)View.CollectionSource).MasterObject).Stage;
        private Stage SalesPeriod => throw new NotImplementedException();
        private void ItemsLayerOnDataLoaded(object sender, DataLoadedEventArgs e) 
            => ZoomTo(_mapItemListEditor.ZoomService,Stores(SalesPeriod));
        
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



        
        CustomerStore[] Stores( Stage stage) 
            => Quotes(stage).Select(quote => quote.CustomerStore).Distinct().ToArray();

    }
}