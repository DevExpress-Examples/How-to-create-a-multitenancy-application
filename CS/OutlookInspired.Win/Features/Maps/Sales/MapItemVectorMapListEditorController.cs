using DevExpress.ExpressApp;
using DevExpress.XtraMap;
using OutlookInspired.Module.BusinessObjects;
using OutlookInspired.Module.Features.Maps;
using OutlookInspired.Module.Services.Internal;
using OutlookInspired.Win.Editors.Maps;
using OutlookInspired.Win.Services.Internal;
using MapItem = OutlookInspired.Module.BusinessObjects.MapItem;

namespace OutlookInspired.Win.Features.Maps.Sales{
    public class MapItemVectorMapListEditorController:ObjectViewController<ListView,MapItem>{
        private VectorMapListEditor _vectorMapListEditor;

        protected override void OnActivated(){
            base.OnActivated();
            _vectorMapListEditor = View.Editor as VectorMapListEditor;
            if (_vectorMapListEditor == null){
                Active["editor"] = false;
                return;
            }
            _vectorMapListEditor.CreateDataAdapter+=OnCreateAdapter;
        }

        protected override void OnDeactivated(){
            base.OnDeactivated();
            if (_vectorMapListEditor != null) _vectorMapListEditor.CreateDataAdapter -= OnCreateAdapter;
        }

        private string GetPieSegmentPropertyName(){
            var isCustomer = ((PropertyCollectionSource)View.CollectionSource).MasterObject is Customer;
            return isCustomer ? nameof(MapItem.ProductName) : nameof(MapItem.CustomerName);
        }

        private void OnCreateAdapter(object sender, DataAdapterArgs e){
            _vectorMapListEditor.ItemsLayer.ToolTipPattern = $"{nameof(MapItem.City)}:%A% {nameof(MapItem.Total)}:%V%";
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
            _vectorMapListEditor.ItemsLayer.DataLoaded+=ItemsLayerOnDataLoaded;
        }

        private void ItemsLayerOnDataLoaded(object sender, DataLoadedEventArgs e){
            var zoomToRegionService = _vectorMapListEditor.ZoomService;
            var salesPeriod = ((ISalesMapsMarker)((PropertyCollectionSource)View.CollectionSource).MasterObject).SalesPeriod;
            var customerStores = ObjectSpace.Stores(salesPeriod);
            zoomToRegionService.To(customerStores);
        }
    }
}