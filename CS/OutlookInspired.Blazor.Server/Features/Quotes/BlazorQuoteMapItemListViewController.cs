using OutlookInspired.Blazor.Server.Editors.Maps;
using OutlookInspired.Module.BusinessObjects;
using OutlookInspired.Module.Features.Quotes;


namespace OutlookInspired.Blazor.Server.Features.Quotes{
    public class BlazorQuoteMapItemListViewController:QuoteMapItemListViewController{
        private MapItemListEditor _mapItemListEditor;
        
        protected override void OnActivated(){
            base.OnActivated();
            _mapItemListEditor = View.Editor as MapItemListEditor;
            if (_mapItemListEditor == null){
                Active["editor"] = false;
                return;
            }
            View.CollectionSource.ResetCollection(true);
            _mapItemListEditor.CustomizeLayers+=MapItemListEditorOnCustomizeLayers;
        }
        
        protected override void OnDeactivated(){
            base.OnDeactivated();
            if (_mapItemListEditor == null) return;
            _mapItemListEditor.CustomizeLayers-=MapItemListEditorOnCustomizeLayers;
        }
        
        
        private Stage Stage => (Stage)StageAction.SelectedItem.Data;

        
        private void MapItemListEditorOnCustomizeLayers(object sender, CustomizeLayersArgs e){
            var dataSource = MapItemListEditor.CreateFeatureCollection(e.MapItems, items =>[Math.Round(items.Sum(item => item.Total))]);
            var bubbleLayer = new BubbleLayer{
                DataSource = dataSource,
                Color =Stage.Color() 
            };
            var predefinedLayer = new PredefinedLayer(){DataSource ="usa" };
            e.Layers.AddRange([predefinedLayer,bubbleLayer]);
            _mapItemListEditor.Control.Bounds= MapItem.GetBounds(e.MapItems,MapItem.PredefinedBound(predefinedLayer.DataSource.ToString()));
            var feature = dataSource.Features.MaxBy(feature => feature.Properties.Values.Sum());
            if (feature==null)return;
            _mapItemListEditor.Control.Annotations =[
                new(){
                    Coordinates =[feature.Geometry.Coordinates.First(), feature.Geometry.Coordinates.Last()],
                    Data = feature.Properties.Tooltip
                }
            ];
        }
        
    }
}