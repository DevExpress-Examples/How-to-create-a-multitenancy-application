using Aqua.EnumerableExtensions;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Chart.Win;
using DevExpress.ExpressApp.Editors;
using DevExpress.XtraCharts;
using OutlookInspired.Module.Features.Maps;
using OutlookInspired.Win.Editors.Maps;
using KeyColorColorizer = DevExpress.XtraMap.KeyColorColorizer;
using MapItem = OutlookInspired.Module.BusinessObjects.MapItem;

namespace OutlookInspired.Win.Features.Maps{
    public class MapItemChartListEditorController:ObjectViewController<DetailView,ISalesMapsMarker>{
        protected override void OnActivated(){
            base.OnActivated();
            View.CustomizeViewItemControl<ListPropertyEditor>(this,
                editor => {
                    var listViewEditor = editor.ListView.Editor;
                    listViewEditor.SelectionChanged += ListViewEditorOnSelectionChanged;
                },nameof(ISalesMapsMarker.Sales));
        }

        private void ListViewEditorOnSelectionChanged(object sender, EventArgs e){
            var chartListEditor = (ChartListEditor)View.GetItems<ListPropertyEditor>()
                .First(editor => editor.ListView?.Editor is ChartListEditor).ListView.Editor;
            var vectorMapItemListEditor = (MapItemListEditor)View.GetItems<ListPropertyEditor>()
                .First(editor => editor.ListView?.Editor is MapItemListEditor).ListView.Editor;
            var city = ((MapItem)vectorMapItemListEditor.ItemsLayer.SelectedItem)?.City;
            var proxyCollection = (ProxyCollection)vectorMapItemListEditor.DataSource;
            chartListEditor.DataSource= ((IEnumerable<MapItem>)proxyCollection.OriginalCollection).Where(item => item.City==city).ToArray();
            ApplyColors(chartListEditor.ChartControl,(KeyColorColorizer)vectorMapItemListEditor.ItemsLayer.Colorizer);
        }
        
        void ApplyColors(ChartControl chartControl, KeyColorColorizer colorizer){
            colorizer.Colors.Clear();
            colorizer.Colors.BeginUpdate();
            chartControl.GetPaletteEntries(20).ForEach(entry => colorizer.Colors.Add(entry.Color));
            colorizer.Colors.EndUpdate();
            chartControl.Series[0].View.Colorizer = (DevExpress.XtraCharts.IColorizer)colorizer;
        }

    }
}