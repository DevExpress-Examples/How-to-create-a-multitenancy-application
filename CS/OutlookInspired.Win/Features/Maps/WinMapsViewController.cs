using DevExpress.Drawing;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Layout;
using DevExpress.Map.Dashboard;
using DevExpress.Persistent.Base;
using DevExpress.XtraMap;
using OutlookInspired.Module.Services.Internal;

namespace OutlookInspired.Win.Features.Maps{
    
    public class WinMapsViewController:ObjectViewController<DetailView,IMapsMarker>{
        readonly BingMapDataProvider _mapDataProvider=new(){ BingKey = Module.Features.Maps.MapsViewController.BindKey,Kind = BingMapKind.Road};
        protected MapControl MapControl;
        protected IZoomToRegionService Zoom;
        private ImageLayer _imageLayer;
        protected MapsViewController MapsViewController;
        

        // public event EventHandler<MapControlEventArgs> MapControlLoaded; 
        static WinMapsViewController() => _ = typeof(MapControl);

        protected override void OnDeactivated(){
            base.OnActivated();
            if (!Active)return;
            MapsViewController.ExportMapAction.Executed-=ExportMapActionOnExecuted;
            MapsViewController.PrintAction.Executed-=PrintActionOnExecuted;
            MapsViewController.PrintPreviewMapAction.Executed-=PrintPreviewMapActionOnExecuted;
            _imageLayer.Error-=ImageLayerOnError;
        }

        protected override void OnActivated(){
            base.OnActivated();
            if (!(Active[nameof(NestedFrame)] = Frame is not NestedFrame&&View.CurrentObject!=null))return;
            MapsViewController = Frame.GetController<MapsViewController>();
            MapsViewController.ExportMapAction.Executed+=ExportMapActionOnExecuted;
            MapsViewController.PrintAction.Executed+=PrintActionOnExecuted;
            MapsViewController.PrintPreviewMapAction.Executed+=PrintPreviewMapActionOnExecuted;
            View.CustomizeViewItemControl<ControlViewItem>(this, item => {
                MapControl = (MapControl)item.Control;
                MapControl.ZoomLevel = 8;
                Zoom = (IZoomToRegionService)((IServiceProvider)MapControl).GetService(typeof(IZoomToRegionService));
                _imageLayer = new ImageLayer{ DataProvider = _mapDataProvider };
                _imageLayer.Error+=ImageLayerOnError;
                MapControl.Layers.Add(_imageLayer);
                
            });
        }

        private void ImageLayerOnError(object sender, MapErrorEventArgs e) => throw new AggregateException(e.Exception.Message, e.Exception);

        

        private void PrintActionOnExecuted(object sender, ActionBaseEventArgs e) 
            => MapControl.Print();

        private void PrintPreviewMapActionOnExecuted(object sender, ActionBaseEventArgs e) 
            => MapControl.ShowRibbonPrintPreview();

        private void ExportMapActionOnExecuted(object sender, ActionBaseEventArgs e){
            using var saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "PNG files (*.png)|*.png";
            saveFileDialog.RestoreDirectory = true;
            saveFileDialog.FileName = $"{View.DefaultMemberValue()}";
            if (saveFileDialog.ShowDialog() == DialogResult.OK){
                MapControl.ExportToImage(saveFileDialog.FileName,DXImageFormat.Png);
            }
        }

        
    }

    public class MapControlEventArgs(MapControl mapControl) :EventArgs{
        public MapControl MapControl{ get; } = mapControl;
    }
}