using System.Linq.Expressions;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Layout;
using OutlookInspired.Blazor.Server.Components.DevExtreme.Maps;
using OutlookInspired.Blazor.Server.Editors.MapItemChart;
using OutlookInspired.Blazor.Server.Services.Internal;
using OutlookInspired.Module.BusinessObjects;
using OutlookInspired.Module.Features.Maps;
using OutlookInspired.Module.Services.Internal;

namespace OutlookInspired.Blazor.Server.Features.Maps.Sales{
    public class SalesMapItemDxChartListEditorController:ObjectViewController<DetailView,ISalesMapsMarker>{
        private MapItemDxChartListEditor _mapItemChartListEditor;
        private SingleChoiceAction _salesPeriodAction;

        protected override void OnActivated(){
            base.OnActivated();
            View.CustomizeViewItemControl<ListPropertyEditor>(this,listPropertyEditor => {
                _mapItemChartListEditor = ((MapItemDxChartListEditor)listPropertyEditor.ListView.Editor);
                _mapItemChartListEditor.ControlsCreated+=MapItemChartListEditorOnControlsCreated;
            });
            _salesPeriodAction = Frame.GetController<Module.Features.Maps.MapsViewController>().SalesPeriodAction;
            _salesPeriodAction.Executed+=SalesPeriodActionOnExecuted;
        }

        protected override void OnDeactivated(){
            base.OnDeactivated();
            _salesPeriodAction.Executed-=SalesPeriodActionOnExecuted;
        }

        private void SalesPeriodActionOnExecuted(object sender, ActionBaseEventArgs e)
            => _mapItemChartListEditor.DataSource = DataSource();

        private void MapItemChartListEditorOnControlsCreated(object sender, EventArgs e){
            _mapItemChartListEditor.MapItemDxChartModel.ArgumentField = ChartModelField();
            _mapItemChartListEditor.MapItemDxChartModel.NameField = ChartModelField();
            _mapItemChartListEditor.MapItemDxChartModel.ValueField = item => item.Total;
            MapDxVectorMapModel.MapItemSelected+=MapModelOnMapItemSelected;
            _mapItemChartListEditor.DataSource = DataSource();
        }

        private DxVectorMapModel MapDxVectorMapModel 
            => ((DxVectorMapModel)View.GetItems<ControlViewItem>().First().Control);

        private MapItem[] DataSource(){
            // return ((ISalesMapsMarker)View.CurrentObject).Sales((Period)_salesPeriodAction.SelectedItem.Data).ToArray()
                // .Colorize(MapDxVectorMapModel.Options.Layers.OfType<PieLayer>().First().Palette,
                    // View.ObjectTypeInfo.Type);
                    throw new NotImplementedException();
        }

        private void MapModelOnMapItemSelected(object sender, MapItemSelectedArgs e){
            // var model = (DxVectorMapModel)sender;
            // _mapItemChartListEditor.DataSource = ((ISalesMapsMarker)View.CurrentObject)
            //     .Sales((Period)_salesPeriodAction.SelectedItem.Data, e.Item.GetProperty(nameof(MapItem.City).FirstCharacterToLower()).GetString())
            //     .Colorize(model.Options.Layers.OfType<PieLayer>().First().Palette, View.ObjectTypeInfo.Type).ToArray();
            
        }

        private Expression<Func<MapItem, string>> ChartModelField() 
            => item => View.ObjectTypeInfo.Type==typeof(Customer)?item.ProductName:item.CustomerName;
    }
}