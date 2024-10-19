using DevExpress.ExpressApp;
using DevExpress.XtraMap;
using OutlookInspired.Module.BusinessObjects;
using OutlookInspired.Module.Services.Internal;
using OutlookInspired.Win.Editors.Maps;
using OutlookInspired.Win.Services.Internal;
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
            _callOut.Text = ObjectSpace.OpportunityCallout(mapItem);
        }


        private void MapItemListEditorOnControlsCreated(object sender, EventArgs e)
            => _mapItemListEditor.Control.Layers.Add(new VectorItemsLayer
                { Data = new MapItemStorage{ Items ={ _callOut } } });

        private void OnCreateAdapter(object sender, DataAdapterArgs e){
            _mapItemListEditor.ItemsLayer.ToolTipPattern = $"{nameof(QuoteMapItem.City)}:%A% {nameof(QuoteMapItem.Value)}:%V%";
            var quote = ((Quote)((PropertyCollectionSource)View.CollectionSource).MasterObject);
            _mapItemListEditor.ItemsLayer.ItemStyle.Fill = quote.PaletteEntries[Array.IndexOf(Enum.GetValues(typeof(Stage)), quote.Stage)].Color;
            e.Adapter = new BubbleChartDataAdapter(){
                Mappings ={
                    Latitude = nameof(QuoteMapItem.Latitude), Longitude = nameof(QuoteMapItem.Longitude),
                    Value = nameof(QuoteMapItem.Value)
                },
                BubbleItemDataMember = nameof(QuoteMapItem.City), BubbleGroupMember = nameof(QuoteMapItem.Index)
            };
        }

        protected override void OnViewControlsCreated(){
            base.OnViewControlsCreated();
            _mapItemListEditor.ItemsLayer.DataLoaded+=ItemsLayerOnDataLoaded;
        }

        private Stage SalesPeriod => ((Quote)((PropertyCollectionSource)View.CollectionSource).MasterObject).Stage;
        private void ItemsLayerOnDataLoaded(object sender, DataLoadedEventArgs e) 
            => _mapItemListEditor.ZoomService.To(ObjectSpace.Stores(SalesPeriod));
    }
}