using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Templates;
using DevExpress.ExpressApp.Utils;
using DevExpress.Persistent.Base;
using OutlookInspired.Module.BusinessObjects;

namespace OutlookInspired.Module.Features.Quotes{
    public class QuoteMapItemListViewController:ObjectViewController<ListView,QuoteMapItem>{
        public QuoteMapItemListViewController(){
            StageAction = new SingleChoiceAction(this,"Stage",PredefinedCategory.PopupActions);
            StageAction.ItemType=SingleChoiceActionItemType.ItemIsOperation;
            StageAction.ImageMode=ImageMode.UseItemImage;
            StageAction.DefaultItemMode = DefaultItemMode.LastExecutedItem;
            StageAction.PaintStyle=ActionItemPaintStyle.Image;
            StageAction.Items.AddRange(Enum.GetValues<Stage>().Where(stage => stage!=Stage.Summary)
                .Select(stage => new ChoiceActionItem(stage.ToString(), stage){ImageName = ImageLoader.Instance.GetEnumValueImageName(stage)}).ToArray());
            StageAction.SelectedItem = StageAction.Items.First();
            StageAction.Executed+=StageActionOnExecuted;
        }

        private void StageActionOnExecuted(object sender, ActionBaseEventArgs e) => View.Editor.Refresh();

        public SingleChoiceAction StageAction{ get; }
        
        protected override void OnActivated(){
            base.OnActivated();
            ((NonPersistentObjectSpace)ObjectSpace).ObjectsGetting+=OnObjectsGetting;
            View.CollectionSource.ResetCollection(true);
            
        }

        protected override void OnDeactivated(){
            base.OnDeactivated();
            ((NonPersistentObjectSpace)ObjectSpace).ObjectsGetting-=OnObjectsGetting;
        }

        private Stage Stage => (Stage)StageAction.SelectedItem.Data;
        private void OnObjectsGetting(object sender, ObjectsGettingEventArgs e) 
            => e.Objects=Enum.GetValues<Stage>().Where(stage1 => stage1==Stage)
                .SelectMany(stage => NewQuoteMapItem(stage, (IObjectSpace)sender, Stage.Range()))
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
        
        
    }
}