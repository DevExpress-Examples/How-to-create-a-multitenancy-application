using DevExpress.DashboardExport.Map;
using DevExpress.ExpressApp;
using OutlookInspired.Blazor.Server.Components.DevExtreme.Maps;
using OutlookInspired.Blazor.Server.Editors.Maps;
using OutlookInspired.Module.BusinessObjects;
using OutlookInspired.Module.Services.Internal;
using GeoPoint = DevExpress.DashboardExport.Map.GeoPoint;

namespace OutlookInspired.Blazor.Server.Features.Quotes{
    public class QuoteMapItemListEditorController:ObjectViewController<ListView,QuoteMapItem>{
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
            // var groupedMapItems = e.MapItems
            //     .GroupBy(item => new { item.City, itemName=IsCustomer?item.ProductName:item.CustomerName })
            //     .Select(items => new QuoteMapItem{
            //         City = items.Key.City,
            //         Total = items.Sum(item => item.Total),
            //         Latitude = items.First().Latitude,
            //         Longitude = items.First().Longitude,
            //         ProductName = IsCustomer?items.Key.itemName:null,
            //         CustomerName = !IsCustomer?items.Key.itemName:null
            //     }).Cast<IMapItem>().ToArray();
            // var productNames = e.MapItems.Select(item =>IsCustomer?item.ProductName: item.CustomerName);
            var pieLayer = new BubbleLayer{
                DataSource = _mapItemListEditor.CreateFeatureCollection(e.MapItems),
                // Palette =_mapItemListEditor.CreatePalette(productNames).Values.ToArray() 
            };
            e.Layers.AddRange([new PredefinedLayer(){DataSource ="usa" },pieLayer]);
        }
        
    }
}