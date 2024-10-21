using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.Templates;
using DevExpress.Persistent.Base;
using OutlookInspired.Module.BusinessObjects;

namespace OutlookInspired.Module.Features.Quotes{
    public class MapOpportunitiesController:ObjectViewController<ListView,Quote>{
        public const string MapItActionId = "MapOpportunity";
        public MapOpportunitiesController(){
            MapQuoteAction = new SimpleAction(this, MapItActionId, PredefinedCategory.View){
                ImageName = "MapIt", PaintStyle = ActionItemPaintStyle.Image
            };
            MapQuoteAction.Executed+=MapQuoteActionOnExecuted;
        }

        private void MapQuoteActionOnExecuted(object sender, ActionBaseEventArgs e){
            var objectSpace = Application.CreateObjectSpace(typeof(QuoteMapItem));
            // var collectionSource = Application.CreateCollectionSource(objectSpace, typeof(QuoteMapItem),QuoteMapItem.MapsListView);
            // var listView = Application.CreateListView((IModelListView)Application.Model.Views[QuoteMapItem.MapsListView], collectionSource, false);
            e.ShowViewParameters.CreatedView = Application.CreateListView(objectSpace, typeof(QuoteMapItem),false);
            e.ShowViewParameters.TargetWindow=TargetWindow.NewModalWindow;
        }


        public SimpleAction MapQuoteAction{ get; }
        
    }
}