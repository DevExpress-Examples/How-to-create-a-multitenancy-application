using DevExpress.ExpressApp;
using DevExpress.XtraMap;
using OutlookInspired.Module.BusinessObjects;
using OutlookInspired.Win.Editors.Maps;

using MapItem = OutlookInspired.Module.BusinessObjects.MapItem;

namespace OutlookInspired.Win.Features.Maps.Sales{
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
            var zoomToRegionService = _mapItemListEditor.ZoomService;
            throw new NotImplementedException();
            // var salesPeriod = ((ISalesMapsMarker)((PropertyCollectionSource)View.CollectionSource).MasterObject).SalesPeriod;
            // var customerStores = ObjectSpace.Stores(salesPeriod);
            // zoomToRegionService.To(customerStores);
        }
    }
}