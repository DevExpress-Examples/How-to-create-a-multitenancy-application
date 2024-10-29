using DevExpress.ExpressApp;
using OutlookInspired.Blazor.Server.Editors.Maps;
using OutlookInspired.Module.BusinessObjects;
using MapItem = OutlookInspired.Module.BusinessObjects.MapItem;

namespace OutlookInspired.Blazor.Server.Features.Maps{
    public class MapItemListEditorController:ObjectViewController<ListView,MapItem>{
        
        private MapItemListEditor _mapItemListEditor;

        protected override void OnActivated(){
            base.OnActivated();
            _mapItemListEditor = View.Editor as MapItemListEditor;
            if (_mapItemListEditor == null){
                Active["editor"] = false;
                return;
            }
            _mapItemListEditor.CustomizeLayers+=MapItemListEditorOnCustomizeLayers;
        }

        private bool IsCustomer => ((PropertyCollectionSource)View.CollectionSource).MasterObject is Customer;
        protected override void OnDeactivated(){
            base.OnDeactivated();
            if (_mapItemListEditor != null) _mapItemListEditor.CustomizeLayers-=MapItemListEditorOnCustomizeLayers;
        }

        private void MapItemListEditorOnCustomizeLayers(object sender, CustomizeLayersArgs e){
            var mapItems = e.MapItems.Cast<MapItem>()
                .GroupBy(item => new { item.City, itemName=IsCustomer?item.ProductName:item.CustomerName })
                .Select(items => new MapItem{
                    City = items.Key.City,
                    Total = items.Sum(item => item.Total),
                    Latitude = items.First().Latitude,
                    Longitude = items.First().Longitude,
                    ProductName = IsCustomer?items.Key.itemName:null,
                    CustomerName = !IsCustomer?items.Key.itemName:null
                }).Cast<IMapItem>().ToArray();
            
            var dataSource = MapItemListEditor.CreateFeatureCollection(mapItems, items => items.Select(item => item.Total).ToList());
            var pieLayer = new PieLayer{ DataSource = dataSource,
                Palette =_mapItemListEditor.CreatePalette(dataSource.Features.Select(feature => feature.Properties.City)).Values.ToArray() 
            };
            var predefinedLayer = new PredefinedLayer(){DataSource ="usa" };
            e.Layers.AddRange([predefinedLayer,pieLayer]);
            _mapItemListEditor.Control.Bounds= MapItem.GetBounds(mapItems);
            _mapItemListEditor.Control.Height = "500px";
        }
        
    }
}