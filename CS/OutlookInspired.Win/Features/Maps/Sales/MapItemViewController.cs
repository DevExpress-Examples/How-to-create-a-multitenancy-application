using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Chart.Win;
using DevExpress.ExpressApp.Editors;
using DevExpress.XtraMap;
using OutlookInspired.Module.Features.Maps;
using OutlookInspired.Win.Editors.Maps;
using OutlookInspired.Win.Services.Internal;
using MapItem = OutlookInspired.Module.BusinessObjects.MapItem;

namespace OutlookInspired.Win.Features.Maps.Sales{
    public class MapItemViewController:ObjectViewController<DetailView,ISalesMapsMarker>{
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
            var vectorMapItemListEditor = (VectorMapListEditor)View.GetItems<ListPropertyEditor>()
                .First(editor => editor.ListView?.Editor is VectorMapListEditor).ListView.Editor;
            var city = ((MapItem)vectorMapItemListEditor.ItemsLayer.SelectedItem)?.City;
            var proxyCollection = (ProxyCollection)vectorMapItemListEditor.DataSource;
            chartListEditor.DataSource= ((IEnumerable<MapItem>)proxyCollection.OriginalCollection).Where(item => item.City==city).ToArray();
            chartListEditor.ChartControl.ApplyColors((KeyColorColorizer)vectorMapItemListEditor.ItemsLayer.Colorizer);
        }
    }
}