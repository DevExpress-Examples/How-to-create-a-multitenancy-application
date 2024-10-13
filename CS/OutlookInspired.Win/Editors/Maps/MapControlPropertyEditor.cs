using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.Win.Editors;
using DevExpress.XtraMap;
using Microsoft.Extensions.DependencyInjection;
using OutlookInspired.Module.BusinessObjects;
using OutlookInspired.Module.Features.Maps;
using EditorAliases = OutlookInspired.Module.Services.EditorAliases;

namespace OutlookInspired.Win.Editors.Maps{
    [PropertyEditor(typeof(Location),EditorAliases.MapPropertyEditor)]
    public class MapControlPropertyEditor(Type objectType, IModelMemberViewItem model)
        : WinPropertyEditor(objectType, model){
        private ImageLayer _imageLayer;

        protected override object CreateControlCore(){
            var bingKey = ServiceProvider.GetService<IMapApiKeyProvider>().Key;
            var mapControl = new MapControl();
            _imageLayer = new ImageLayer{ DataProvider =new BingMapDataProvider(){ BingKey = bingKey,Kind = BingMapKind.Road} };
            _imageLayer.Error+=ImageLayerOnError;
            mapControl.Layers.Add(_imageLayer);
            mapControl.Layers.AddRange(new LayerBase[]{
                new InformationLayer{ DataProvider = new BingGeocodeDataProvider(){BingKey =bingKey } },
                new InformationLayer{ DataProvider = new BingSearchDataProvider(){BingKey = bingKey} },
            });
            return mapControl;
        }

        public override void BreakLinksToControl(bool unwireEventsOnly){
            base.BreakLinksToControl(unwireEventsOnly);
            if (_imageLayer != null) _imageLayer.Error -= ImageLayerOnError;
        }

        public new MapControl Control => (MapControl)base.Control;

        private void ImageLayerOnError(object sender, MapErrorEventArgs e) => throw new AggregateException(e.Exception.Message, e.Exception);

        
    }
}