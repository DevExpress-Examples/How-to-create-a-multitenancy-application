using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Templates;
using DevExpress.Persistent.Base;
using OutlookInspired.Module.BusinessObjects;

namespace OutlookInspired.Module.Features.Quotes{
    public class MapOpportunitiesController:ObjectViewController<ListView,QuoteAnalysis>{
        public const string MapItActionId = "MapOpportunity";
        public MapOpportunitiesController(){
            MapOpportunitiesAction = new SimpleAction(this, MapItActionId, PredefinedCategory.View){
                ImageName = "MapIt", PaintStyle = ActionItemPaintStyle.Image
            };
            MapOpportunitiesAction.Executed+=MapOpportunitiesActionOnExecuted;
        }

        private void MapOpportunitiesActionOnExecuted(object sender, ActionBaseEventArgs e){
            var objectSpace = Application.CreateObjectSpace(typeof(QuoteMapItem));
            e.ShowViewParameters.CreatedView = Application.CreateListView(objectSpace, typeof(QuoteMapItem),false);
            e.ShowViewParameters.TargetWindow=TargetWindow.NewModalWindow;
            e.ShowViewParameters.Controllers.Add(Application.CreateController<DialogController>());
        }


        public SimpleAction MapOpportunitiesAction{ get; }
        
    }
}