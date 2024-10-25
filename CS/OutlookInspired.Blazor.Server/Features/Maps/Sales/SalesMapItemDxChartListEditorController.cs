using System.Drawing;
using DevExpress.Blazor;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Editors;
using OutlookInspired.Blazor.Server.Editors.Charts;
using OutlookInspired.Blazor.Server.Editors.Maps;
using OutlookInspired.Module.BusinessObjects;
using OutlookInspired.Module.Features.Maps;

namespace OutlookInspired.Blazor.Server.Features.Maps.Sales{
    public class SalesMapItemDxChartListEditorController:ObjectViewController<DetailView,ISalesMapsMarker>{
        private DxChartListEditor _chartListEditor;

        protected override void OnActivated(){
            base.OnActivated();
            View.CustomizeViewItemControl<ListPropertyEditor>(this,listPropertyEditor => {
                if (listPropertyEditor.ListView.Editor is not DxChartListEditor mapItemDxChartListEditor) return;
                _chartListEditor = mapItemDxChartListEditor;
                _chartListEditor.ControlsCreated+=ChartListEditorOnControlsCreated;
            });
        }

        protected override void OnDeactivated(){
            base.OnDeactivated();
            if (_chartListEditor == null) return;
            _chartListEditor.ControlsCreated -= ChartListEditorOnControlsCreated;
        }

        private void ChartListEditorOnControlsCreated(object sender, EventArgs args){
            var model = _chartListEditor.Control;
            model.SeriesType=ChartSeriesType.Bar;
            model.ArgumentField = model.NameField=item 
                => IsCustomer? ((MapItem)item).ProductName:((MapItem)item).CustomerName;
            model.SummaryMethod = items => items.Sum();
            model.ValueField = item => ((MapItem)item).Total;
            model.CustomizeSeriesPoint=e => e.PointAppearance.Color = ColorTranslator.FromHtml(e.Point.DataItems.Cast<MapItem>().First().Color);
            var mapItemListEditor = View.GetItems<ListPropertyEditor>()
                .Select(editor => editor.ListView?.Editor).OfType<MapItemListEditor>().First();
            mapItemListEditor.SelectionChanged+=MapItemListEditorOnSelectionChanged;
            var mapItems = ((ProxyCollection)mapItemListEditor.DataSource).Cast<MapItem>().ToArray();
            mapItemListEditor.ApplyColors( mapItems,item => IsCustomer?item.ProductName:item.CustomerName);
            _chartListEditor.DataSource = mapItems;
        }

        private bool IsCustomer => View.ObjectTypeInfo.Type == typeof(Customer);
        private void MapItemListEditorOnSelectionChanged(object sender, EventArgs e){
            var city = ((MapItemListEditor)sender).GetSelectedObjects().Cast<IMapItem>().FirstOrDefault()?.City;
            if (city==null)return;
            var mapItemListEditor = View.GetItems<ListPropertyEditor>()
                .Select(editor => editor.ListView?.Editor).OfType<MapItemListEditor>().First();
            var dataSource = MapItems(mapItemListEditor)
                .Where(item => item.City == city)
                .ToArray();
            mapItemListEditor.ApplyColors( dataSource,item => IsCustomer?item.ProductName:item.CustomerName );
            _chartListEditor.DataSource = dataSource;
        }

        private static MapItem[] MapItems(MapItemListEditor mapItemListEditor) => ((ProxyCollection)mapItemListEditor.DataSource).Cast<MapItem>().ToArray();

    }
}