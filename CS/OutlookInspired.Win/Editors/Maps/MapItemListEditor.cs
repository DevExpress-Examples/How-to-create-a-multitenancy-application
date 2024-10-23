using System.Collections;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Model;
using DevExpress.Map.Dashboard;
using DevExpress.XtraMap;
using Microsoft.Extensions.DependencyInjection;
using OutlookInspired.Module.BusinessObjects;
using OutlookInspired.Module.Features.Maps;
using OutlookInspired.Win.Features.Maps;

namespace OutlookInspired.Win.Editors.Maps{
    [ListEditor(typeof(IMapItem),true)]
    public class MapItemListEditor(IModelListView model) : ListEditor(model),IComplexListEditor{
        public event EventHandler<DataAdapterArgs> CreateDataAdapter; 
        private ImageLayer _imageLayer;
        private VectorItemsLayer _itemsLayer;
        private DataSourceAdapterBase _dataAdapter;
        private CollectionSourceBase _collectionSource;
        private IZoomToRegionService _zoomToRegionService;

        public VectorItemsLayer ItemsLayer => _itemsLayer;

        public IZoomToRegionService ZoomService => _zoomToRegionService;

        public new MapControl Control => (MapControl)base.Control;
        
        protected override object CreateControlsCore(){
            var bingKey = ServiceProvider.GetService<IMapApiKeyProvider>().Key;
            var mapControl = new MapControl();
            _zoomToRegionService = (IZoomToRegionService)((IServiceProvider)mapControl).GetService(typeof(IZoomToRegionService));
            _imageLayer = new ImageLayer{ DataProvider =new BingMapDataProvider(){ BingKey = bingKey,Kind = BingMapKind.Road} };
            _imageLayer.Error+=ImageLayerOnError;
            mapControl.Layers.Add(_imageLayer);
            mapControl.Layers.AddRange(new LayerBase[]{
                new InformationLayer{ DataProvider = new BingSearchDataProvider(){BingKey = bingKey} },
            });
            mapControl.ZoomLevel = 8;
            mapControl.Dock=DockStyle.Fill;
            mapControl.Layers.AddRange(new LayerBase[]{ NewVectorItemsLayer() });
            _itemsLayer.DataLoaded+=ItemsLayerOnDataLoaded;
            mapControl.SelectionChanged+=MapControlOnSelectionChanged;
            return mapControl;
        }

        private void MapControlOnSelectionChanged(object sender, MapSelectionChangedEventArgs e) 
            => OnSelectionChanged();

        protected override void OnSelectionChanged(){
            base.OnSelectionChanged();
            OnProcessSelectedItem();
        }

        private VectorItemsLayer NewVectorItemsLayer(){
            _itemsLayer = new VectorItemsLayer{
                Colorizer = new Colorizer{ ItemKeyProvider = new ArgumentItemKeyProvider() }
            };
            var args = new DataAdapterArgs();
            OnCreateAdapter(args);
            _itemsLayer.Data=_dataAdapter=args.Adapter;
            return _itemsLayer;
        }
        
        private void ItemsLayerOnDataLoaded(object sender, DataLoadedEventArgs e){
            var item = _itemsLayer.Data.Items.FirstOrDefault();
            _itemsLayer.SelectedItem = item != null ? _itemsLayer.Data.GetItemSourceObject(item) : null;
        }

        private void ImageLayerOnError(object sender, MapErrorEventArgs e) 
            => throw new AggregateException(e.Exception.Message, e.Exception);

        public override void BreakLinksToControls(){
            base.BreakLinksToControls();
            if (_imageLayer != null) _imageLayer.Error -= ImageLayerOnError;
            if (Control != null) Control.SelectionChanged -= MapControlOnSelectionChanged;
        }

        protected override void AssignDataSourceToControl(object dataSource){
            if (_dataAdapter != null) _dataAdapter.DataSource = dataSource;
        }

        public override void Refresh() => _collectionSource.ResetCollection();

        public override IList GetSelectedObjects() => ItemsLayer?.SelectedItems.Cast<IMapItem>().ToList()??new ();

        public override SelectionType SelectionType => SelectionType.None;
        public void Setup(CollectionSourceBase collectionSource, XafApplication application) 
            => _collectionSource = collectionSource;

        protected virtual void OnCreateAdapter(DataAdapterArgs e) => CreateDataAdapter?.Invoke(this, e);
    }

    public class DataAdapterArgs :EventArgs{
        public DataSourceAdapterBase Adapter{ get; set; } 
    }
}