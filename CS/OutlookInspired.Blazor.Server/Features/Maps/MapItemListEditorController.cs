using DevExpress.ExpressApp;
using OutlookInspired.Blazor.Server.Editors.Maps;
using OutlookInspired.Module.BusinessObjects;
using MapItem = OutlookInspired.Module.BusinessObjects.MapItem;

namespace OutlookInspired.Blazor.Server.Features.Maps {
    public class MapItemListEditorController : ObjectViewController<ListView, MapItem> {
        protected override void OnActivated() {
            base.OnActivated();
            if(View.Editor is MapItemListEditor mapItemListEditor) {
                mapItemListEditor.CustomizeLayers += MapItemListEditorOnCustomizeLayers;
            }
        }

        private bool IsCustomer => ((PropertyCollectionSource)View.CollectionSource).MasterObject is Customer;
        protected override void OnDeactivated() {
            base.OnDeactivated();
            if(View.Editor is MapItemListEditor mapItemListEditor) {
                mapItemListEditor.CustomizeLayers -= MapItemListEditorOnCustomizeLayers;
            }
        }

        private void MapItemListEditorOnCustomizeLayers(object sender, CustomizeLayersArgs e) {
            var mapItemListEditor = (MapItemListEditor)sender;
            var mapItems = e.MapItems.Cast<MapItem>()
                .GroupBy(item => new { item.City, itemName = IsCustomer ? item.ProductName : item.CustomerName })
                .Select(items => new MapItem {
                    City = items.Key.City,
                    Total = items.Sum(item => item.Total),
                    Latitude = items.First().Latitude,
                    Longitude = items.First().Longitude,
                    ProductName = IsCustomer ? items.Key.itemName : null,
                    CustomerName = !IsCustomer ? items.Key.itemName : null
                }).Cast<IMapItem>().ToArray();

            var dataSource = MapItemListEditor.CreateFeatureCollection(mapItems, items => items.Select(item => item.Total).ToList());
            var pieLayer = new PieLayer {
                DataSource = dataSource,
                Palette = mapItemListEditor.CreatePalette(dataSource.Features.Select(feature => feature.Properties.City)).Values.ToArray()
            };
            var predefinedLayer = new PredefinedLayer() { DataSource = "usa" };
            e.Layers.AddRange([predefinedLayer, pieLayer]);
            mapItemListEditor.Control.Bounds = MapItem.GetBounds(mapItems);
            mapItemListEditor.Control.Height = "500px";
        }

    }
}