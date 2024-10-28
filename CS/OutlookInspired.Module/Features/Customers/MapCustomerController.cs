using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Templates;
using DevExpress.Persistent.Base;
using OutlookInspired.Module.BusinessObjects;

namespace OutlookInspired.Module.Features.Customers{
    public class MapCustomerController:ObjectViewController<ListView,Customer>{
        public const string MapItActionId = "MapCustomer";
        public MapCustomerController(){
            MapCustomerAction = new SimpleAction(this, MapItActionId, PredefinedCategory.View){
                ImageName = "MapIt", PaintStyle = ActionItemPaintStyle.Image,SelectionDependencyType = SelectionDependencyType.RequireSingleObject
            };
            MapCustomerAction.Executed+=MapCustomerActionOnExecuted;
        }

        private void MapCustomerActionOnExecuted(object sender, ActionBaseEventArgs e){
            var objectSpace = Application.CreateObjectSpace(typeof(Customer));
            e.ShowViewParameters.CreatedView = Application.CreateDetailView(objectSpace,
                (IModelDetailView)Application.Model.Views[Customer.MapsDetailView], false, objectSpace.GetObject(View.CurrentObject));
            e.ShowViewParameters.TargetWindow=TargetWindow.NewModalWindow;
            e.ShowViewParameters.Controllers.Add(Application.CreateController<DialogController>());
        }


        public SimpleAction MapCustomerAction{ get; }
        
    }
}