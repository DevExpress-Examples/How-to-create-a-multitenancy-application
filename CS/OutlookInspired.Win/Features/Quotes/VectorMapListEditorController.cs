using DevExpress.ExpressApp;
using DevExpress.XtraMap;
using OutlookInspired.Module.BusinessObjects;
using OutlookInspired.Module.Services.Internal;
using OutlookInspired.Win.Editors.Maps;
using OutlookInspired.Win.Services.Internal;
using GeoPoint = DevExpress.DashboardExport.Map.GeoPoint;

namespace OutlookInspired.Win.Features.Quotes{
    public class VectorMapListEditorController:ObjectViewController<ListView,QuoteMapItem>{
        private VectorMapListEditor _vectorMapListEditor;
        private readonly MapCallout _callOut = new(){ AllowHtmlText = true };

        protected override void OnActivated(){
            base.OnActivated();
            _vectorMapListEditor = View.Editor as VectorMapListEditor;
            if (_vectorMapListEditor == null){
                Active["editor"] = false;
                return;
            }
            _vectorMapListEditor.CreateDataAdapter+=OnCreateAdapter;
            _vectorMapListEditor.SelectionChanged+=VectorMapListEditorOnSelectionChanged;
            _vectorMapListEditor.ControlsCreated+=VectorMapListEditorOnControlsCreated;
        }

        protected override void OnDeactivated(){
            base.OnDeactivated();
            if (_vectorMapListEditor == null) return;
            _vectorMapListEditor.CreateDataAdapter -= OnCreateAdapter;
            _vectorMapListEditor.SelectionChanged-=VectorMapListEditorOnSelectionChanged;
            _vectorMapListEditor.ControlsCreated-=VectorMapListEditorOnControlsCreated;
        }

        private void VectorMapListEditorOnSelectionChanged(object sender, EventArgs e){
            var mapItem = View.SelectedObjects.Cast<QuoteMapItem>().FirstOrDefault();
            if (mapItem==null) return;
            _callOut.Location = new GeoPoint(mapItem.Latitude, mapItem.Longitude);
            _callOut.Text = ObjectSpace.OpportunityCallout(mapItem);
        }


        private void VectorMapListEditorOnControlsCreated(object sender, EventArgs e){
            
            _vectorMapListEditor.Control.Layers.Add(new VectorItemsLayer
                { Data = new MapItemStorage{ Items ={ _callOut } } });
        }

        private void OnCreateAdapter(object sender, DataAdapterArgs e){
            _vectorMapListEditor.ItemsLayer.ToolTipPattern = $"{nameof(QuoteMapItem.City)}:%A% {nameof(QuoteMapItem.Value)}:%V%";
            var quote = ((Quote)((PropertyCollectionSource)View.CollectionSource).MasterObject);
            _vectorMapListEditor.ItemsLayer.ItemStyle.Fill = quote.PaletteEntries[Array.IndexOf(Enum.GetValues(typeof(Stage)), quote.Stage)].Color;
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
            _vectorMapListEditor.ItemsLayer.DataLoaded+=ItemsLayerOnDataLoaded;
        }

        private void ItemsLayerOnDataLoaded(object sender, DataLoadedEventArgs e){
            var salesPeriod = ((Quote)((PropertyCollectionSource)View.CollectionSource).MasterObject).Stage;
            var customerStores = ObjectSpace.Stores(salesPeriod);
            _vectorMapListEditor.ZoomService.To(customerStores);
        }
        
        


    }
}