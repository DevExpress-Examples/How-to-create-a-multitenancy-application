using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Layout;
using OutlookInspired.Blazor.Server.Components.DevExtreme.Maps;
using OutlookInspired.Blazor.Server.Services.Internal;
using OutlookInspired.Module.BusinessObjects;
using OutlookInspired.Module.Features.Maps;
using OutlookInspired.Module.Services.Internal;

namespace OutlookInspired.Blazor.Server.Features.Maps.Sales {
    public class SalesMapsViewController:ObjectViewController<DetailView,ISalesMapsMarker>{
        private MapItem[] _mapItems;
        private DxVectorMapModel _model;
        private SingleChoiceAction _salesPeriodAction;

        protected override void OnActivated(){
            base.OnActivated();
            View.CustomizeViewItemControl<ControlViewItem>(this, item => {
                if (item.Control is not DxVectorMapModel) return;
                CustomizeModel(item);
            });
            _salesPeriodAction = Frame.GetController<Module.Features.Maps.MapsViewController>().SalesPeriodAction;
            _salesPeriodAction.Executed+=SalesPeriodActionOnExecuted;
        }
        
        protected override void OnDeactivated(){
            base.OnDeactivated();
            _salesPeriodAction.Executed-=SalesPeriodActionOnExecuted;
        }

        private void SalesPeriodActionOnExecuted(object sender, ActionBaseEventArgs e){
            var model = CustomizeModel(View.GetItems<ControlViewItem>().First());
            model.LayerDatasource = model.Options.Layers.OfType<PieLayer>().First();
        }


        protected DxVectorMapModel CustomizeModel(ControlViewItem item) {
            _model = ((DxVectorMapModel)item.Control);
            // _mapItems = ((ISalesMapsMarker)View.CurrentObject).Sales((Period)Frame.GetController<Module.Features.Maps.MapsViewController>().SalesPeriodAction.SelectedItem.Data).ToArray();
            _model.Options = _mapItems.VectorMapOptions<MapItem, PieLayer>(_mapItems.Palette(View.ObjectTypeInfo.Type),
                items => items.Select(mapItem => mapItem.Total).ToList());
            return _model;
        }
    }
}