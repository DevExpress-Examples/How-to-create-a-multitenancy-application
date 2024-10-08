using System.Linq.Expressions;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Layout;
using OutlookInspired.Blazor.Server.Components.DevExtreme.Maps;
using OutlookInspired.Blazor.Server.Services.Internal;
using OutlookInspired.Module.BusinessObjects;
using OutlookInspired.Module.Features.Maps;
using OutlookInspired.Module.Services.Internal;

namespace OutlookInspired.Blazor.Server.Editors.MapItemChart{
    public class SalesMapItemDxChartListEditorController:ObjectViewController<DetailView,ISalesMapsMarker>{
        private MapItemDxChartListEditor _mapItemChartListEditor;

        protected override void OnActivated(){
            base.OnActivated();
            View.CustomizeViewItemControl<ListPropertyEditor>(this,listPropertyEditor => {
                _mapItemChartListEditor = ((MapItemDxChartListEditor)listPropertyEditor.ListView.Editor);
                _mapItemChartListEditor.ControlsCreated+=MapItemChartListEditorOnControlsCreated;
            });
        }

        private void MapItemChartListEditorOnControlsCreated(object sender, EventArgs e){
            var mapItemChartListEditor = ((MapItemDxChartListEditor)sender);
            mapItemChartListEditor.MapItemDxChartModel.ArgumentField = ChartModelField();
            mapItemChartListEditor.MapItemDxChartModel.NameField = ChartModelField();
            mapItemChartListEditor.MapItemDxChartModel.ValueField = item => item.Total;
            var period = SalesPeriod;
            var mapItems = ((ISalesMapsMarker)View.CurrentObject).Sales(period).ToArray();
            var mapModel = ((DxVectorMapModel)View.GetItems<ControlViewItem>().First().Control);
            mapModel.MapItemSelected+=MapModelOnMapItemSelected;
            mapItemChartListEditor.DataSource = mapItems
                .Colorize(mapModel.Options.Layers.OfType<PieLayer>().First().Palette,View.ObjectTypeInfo.Type);
        }

        private Period SalesPeriod 
            => (Period)Frame.GetController<MapsViewController>().SalesPeriodAction.SelectedItem.Data;

        private void MapModelOnMapItemSelected(object sender, MapItemSelectedArgs e){
            var model = (DxVectorMapModel)sender;
            _mapItemChartListEditor.DataSource = ((ISalesMapsMarker)View.CurrentObject)
                .Sales(SalesPeriod, e.Item.GetProperty(nameof(MapItem.City).FirstCharacterToLower()).GetString())
                .Colorize(model.Options.Layers.OfType<PieLayer>().First().Palette, View.ObjectTypeInfo.Type).ToArray();
        }

        private Expression<Func<MapItem, string>> ChartModelField() 
            => item => View.ObjectTypeInfo.Type==typeof(Customer)?item.ProductName:item.CustomerName;
    }
}