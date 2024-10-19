using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Editors;
using OutlookInspired.Blazor.Server.Editors.MapItemChart;
using OutlookInspired.Blazor.Server.Editors.Maps;
using OutlookInspired.Module.BusinessObjects;
using OutlookInspired.Module.Features.Maps;

namespace OutlookInspired.Blazor.Server.Features.Maps.Sales{
    public class MapItemDxChartListEditorController:ObjectViewController<DetailView,ISalesMapsMarker>{
        private MapItemDxChartListEditor _mapItemChartListEditor;

        protected override void OnActivated(){
            base.OnActivated();
            View.CustomizeViewItemControl<ListPropertyEditor>(this,listPropertyEditor => {
                if (listPropertyEditor.ListView.Editor is not MapItemDxChartListEditor mapItemDxChartListEditor) return;
                _mapItemChartListEditor = mapItemDxChartListEditor;
                _mapItemChartListEditor.ControlsCreated+=MapItemChartListEditorOnControlsCreated;
            });
        }

        protected override void OnDeactivated(){
            base.OnDeactivated();
            if (_mapItemChartListEditor == null) return;
            _mapItemChartListEditor.ControlsCreated -= MapItemChartListEditorOnControlsCreated;
        }

        private void MapItemChartListEditorOnControlsCreated(object sender, EventArgs e){
            var model = _mapItemChartListEditor.Control;
            model.ArgumentField = model.NameField=item 
                => IsCustomer? ((MapItem)item).ProductName:((MapItem)item).CustomerName;
            model.SummaryMethod = items => items.Sum();
            model.ValueField = item => item.Total;
            var mapItemListEditor = View.GetItems<ListPropertyEditor>()
                .Select(editor => editor.ListView?.Editor).OfType<MapItemListEditor>().First();
            mapItemListEditor.SelectionChanged+=MapItemListEditorOnSelectionChanged;
            var mapItems = ((ProxyCollection)mapItemListEditor.DataSource).Cast<MapItem>().ToArray();
            mapItemListEditor.ApplyColors( mapItems,item => IsCustomer?item.ProductName:item.CustomerName);
            _mapItemChartListEditor.DataSource = mapItems;
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
            _mapItemChartListEditor.DataSource = dataSource;
        }

        private static MapItem[] MapItems(MapItemListEditor mapItemListEditor) => ((ProxyCollection)mapItemListEditor.DataSource).Cast<MapItem>().ToArray();

    }
}