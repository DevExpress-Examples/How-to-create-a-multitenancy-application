using OutlookInspired.Blazor.Server.Editors.Maps;
using OutlookInspired.Module.BusinessObjects;
using OutlookInspired.Module.Features.Quotes;


namespace OutlookInspired.Blazor.Server.Features.Quotes {
    public class BlazorQuoteMapItemListViewController : QuoteMapItemListViewController {
        protected override void OnActivated() {
            base.OnActivated();
            if(View.Editor is MapItemListEditor mapItemListEditor) {
                View.CollectionSource.ResetCollection(true);
                mapItemListEditor.CustomizeLayers += MapItemListEditorOnCustomizeLayers;
            }
        }

        protected override void OnDeactivated() {
            base.OnDeactivated();
            if(View.Editor is MapItemListEditor mapItemListEditor) {
                mapItemListEditor.CustomizeLayers -= MapItemListEditorOnCustomizeLayers;
            }
        }


        private Stage Stage => (Stage)StageAction.SelectedItem.Data;


        private void MapItemListEditorOnCustomizeLayers(object sender, CustomizeLayersArgs e) {
            var mapItemListEditor = (MapItemListEditor)sender;
            var dataSource = MapItemListEditor.CreateFeatureCollection(e.MapItems, items => [Math.Round(items.Sum(item => item.Total))]);
            var bubbleLayer = new BubbleLayer {
                DataSource = dataSource,
                Color = Stage.Color()
            };
            var predefinedLayer = new PredefinedLayer() { DataSource = "usa" };
            e.Layers.AddRange([predefinedLayer, bubbleLayer]);
            mapItemListEditor.Control.Bounds = MapItem.GetBounds(e.MapItems, MapItem.PredefinedBound(predefinedLayer.DataSource.ToString()));
            var feature = dataSource.Features.MaxBy(feature => feature.Properties.Values.Sum());
            if(feature == null) return;
            mapItemListEditor.Control.Annotations = [
                new(){
                    Coordinates =[feature.Geometry.Coordinates.First(), feature.Geometry.Coordinates.Last()],
                    Data = feature.Properties.Tooltip
                }
            ];

            mapItemListEditor.Control.Height = "500px";
        }

    }
}