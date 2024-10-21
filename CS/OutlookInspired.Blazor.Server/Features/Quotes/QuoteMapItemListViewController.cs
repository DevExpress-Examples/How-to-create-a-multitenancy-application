using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Templates;
using DevExpress.ExpressApp.Utils;
using DevExpress.Persistent.Base;
using OutlookInspired.Blazor.Server.Components.DevExtreme.Maps;
using OutlookInspired.Blazor.Server.Editors.Maps;
using OutlookInspired.Module.BusinessObjects;
using OutlookInspired.Module.Services;
using OutlookInspired.Module.Services.Internal;

namespace OutlookInspired.Blazor.Server.Features.Quotes{
    public class QuoteMapItemListViewController:ObjectViewController<ListView,QuoteMapItem>{
        private MapItemListEditor _mapItemListEditor;

        public QuoteMapItemListViewController(){
            StageAction = new SingleChoiceAction(this,"Stage",PredefinedCategory.PopupActions);
            StageAction.ItemType=SingleChoiceActionItemType.ItemIsOperation;
            StageAction.ImageMode=ImageMode.UseItemImage;
            StageAction.DefaultItemMode = DefaultItemMode.LastExecutedItem;
            StageAction.PaintStyle=ActionItemPaintStyle.Image;
            StageAction.Executed+=ActionOnExecuted;
            StageAction.Items.AddRange(Enum.GetValues<Stage>().Where(stage => stage!=Stage.Summary)
                .Select(stage => new ChoiceActionItem(stage.ToString(), stage){ImageName = ImageLoader.Instance.GetEnumValueImageName(stage)}).ToArray());
            StageAction.SelectedItem = StageAction.Items.First();
        }

        public SingleChoiceAction StageAction{ get; }
        
        private void ActionOnExecuted(object sender, ActionBaseEventArgs e) 
            => ((MapItemListEditor)View.Editor).Refresh();
        
        protected override void OnActivated(){
            base.OnActivated();
            _mapItemListEditor = View.Editor as MapItemListEditor;
            if (_mapItemListEditor == null){
                Active["editor"] = false;
                return;
            }
            ((NonPersistentObjectSpace)ObjectSpace).ObjectsGetting+=OnObjectsGetting;
            View.CollectionSource.ResetCollection(true);
            _mapItemListEditor.CustomizeLayers+=MapItemListEditorOnCustomizeLayers;
        }
        
        protected override void OnDeactivated(){
            base.OnDeactivated();
            if (_mapItemListEditor == null) return;
            _mapItemListEditor.CustomizeLayers-=MapItemListEditorOnCustomizeLayers;
            ((NonPersistentObjectSpace)ObjectSpace).ObjectsGetting-=OnObjectsGetting;
        }
        
        
        private Stage Stage => (Stage)StageAction.SelectedItem.Data;
        private void OnObjectsGetting(object sender, ObjectsGettingEventArgs e) 
            => e.Objects=Enum.GetValues<Stage>().Where(stage1 => stage1==Stage)
                .SelectMany(stage => NewQuoteMapItem(stage, (IObjectSpace)sender, Stage.Map()))
                .ToArray();

        private QuoteMapItem[] NewQuoteMapItem(Stage stage, IObjectSpace objectSpace, (double min, double max) value){
            var quotes = objectSpace.GetObjectsQuery<Quote>()
                .Where(quote => quote.Opportunity > value.min && quote.Opportunity < value.max);
            return quotes.Select(quote => new{
                    quote.Total, quote.Date, quote.CustomerStore.City,
                    quote.CustomerStore.Latitude, quote.CustomerStore.Longitude,
                })
                .ToArray().Select((t, i) => new QuoteMapItem{
                    Stage = stage, Latitude = t.Latitude, City = t.City, ID = i,
                    Date = t.Date, Longitude = t.Longitude, Total = t.Total, Value = t.Total,
                    Color = stage.Color()
                }).ToArray();
        }
        
        private void MapItemListEditorOnCustomizeLayers(object sender, CustomizeLayersArgs e){
            var dataSource = MapItemListEditor.CreateFeatureCollection(e.MapItems, items =>[Math.Round(items.Sum(item => item.Total))]);
            var bubbleLayer = new BubbleLayer{
                DataSource = dataSource,
                Color =Stage.Color() 
            };
            var predefinedLayer = new PredefinedLayer(){DataSource ="usa" };
            e.Layers.AddRange([predefinedLayer,bubbleLayer]);
            _mapItemListEditor.Control.Bounds= MapItemListEditor.GetBounds(e.MapItems,predefinedLayer.Bounds());
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