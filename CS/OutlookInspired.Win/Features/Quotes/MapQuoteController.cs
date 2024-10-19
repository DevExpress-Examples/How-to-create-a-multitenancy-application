using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Templates;
using DevExpress.Persistent.Base;
using OutlookInspired.Module.BusinessObjects;

namespace OutlookInspired.Win.Features.Quotes{
    public class MapQuoteController:ObjectViewController<ListView,Quote>{
        public const string MapItActionId = "MapQuote";
        public MapQuoteController(){
            MapQuoteAction = new SimpleAction(this, MapItActionId, PredefinedCategory.View){
                ImageName = "MapIt", PaintStyle = ActionItemPaintStyle.Image
            };
            MapQuoteAction.Executed+=MapQuoteActionOnExecuted;
        }

        private void MapQuoteActionOnExecuted(object sender, ActionBaseEventArgs e){
            var objectSpace = Application.CreateObjectSpace(typeof(Opportunity));
            
            e.ShowViewParameters.CreatedView = Application.CreateDetailView(objectSpace,objectSpace.CreateObject<Opportunity>());
            e.ShowViewParameters.TargetWindow=TargetWindow.NewModalWindow;
        }


        public SimpleAction MapQuoteAction{ get; }
        
    }
}