using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Templates;
using DevExpress.Persistent.Base;
using OutlookInspired.Module.BusinessObjects;

namespace OutlookInspired.Module.Features.Orders{
    public class MapOrderController:ObjectViewController<ObjectView,Order>{
        public const string MapItActionId = "MapOrder";
        public MapOrderController(){
            MapOrderAction = new SimpleAction(this, MapItActionId, PredefinedCategory.View){
                ImageName = "MapIt", PaintStyle = ActionItemPaintStyle.Image,SelectionDependencyType = SelectionDependencyType.RequireSingleObject
            };
            MapOrderAction.Executed+=MapOrderActionOnExecuted;
        }

        private void MapOrderActionOnExecuted(object sender, ActionBaseEventArgs e){
            var objectSpace = Application.CreateObjectSpace(typeof(Order));
            e.ShowViewParameters.CreatedView = Application.CreateDetailView(objectSpace,
                (IModelDetailView)Application.Model.Views[Order.MapsDetailView], false, objectSpace.GetObject(View.CurrentObject));
            e.ShowViewParameters.TargetWindow=TargetWindow.NewModalWindow;
            e.ShowViewParameters.Controllers.Add(Application.CreateController<DialogController>());
        }


        public SimpleAction MapOrderAction{ get; }
        
    }
}