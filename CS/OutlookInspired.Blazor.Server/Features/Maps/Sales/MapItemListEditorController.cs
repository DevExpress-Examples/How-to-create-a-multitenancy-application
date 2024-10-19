using DevExpress.ExpressApp;
using OutlookInspired.Blazor.Server.Components.DevExtreme.Maps;
using OutlookInspired.Blazor.Server.Editors.Maps;
using OutlookInspired.Module.BusinessObjects;
using MapItem = OutlookInspired.Module.BusinessObjects.MapItem;

namespace OutlookInspired.Blazor.Server.Features.Maps.Sales{
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
            var groupedMapItems = e.MapItems.Cast<MapItem>()
                .GroupBy(item => new { item.City, itemName=IsCustomer?item.ProductName:item.CustomerName })
                .Select(items => new MapItem{
                    City = items.Key.City,
                    Total = items.Sum(item => item.Total),
                    Latitude = items.First().Latitude,
                    Longitude = items.First().Longitude,
                    ProductName = IsCustomer?items.Key.itemName:null,
                    CustomerName = !IsCustomer?items.Key.itemName:null
                }).Cast<IMapItem>().ToArray();
            var pieLayer = new PieLayer{
                DataSource = _mapItemListEditor.CreateFeatureCollection(groupedMapItems),
                Palette =_mapItemListEditor.CreatePalette(e.MapItems.Cast<MapItem>().Select(item =>IsCustomer?item.ProductName: item.CustomerName)).Values.ToArray() 
            };
            e.Layers.AddRange([new PredefinedLayer(){DataSource ="usa" },pieLayer]);
        }
        
    }
}