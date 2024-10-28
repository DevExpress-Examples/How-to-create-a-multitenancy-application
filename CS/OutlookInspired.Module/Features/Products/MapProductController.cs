using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Templates;
using DevExpress.Persistent.Base;
using OutlookInspired.Module.BusinessObjects;

namespace OutlookInspired.Module.Features.Products{
    public class MapProductController:ObjectViewController<ObjectView,Product>{
        public const string MapItActionId = "MapProduct";
        public MapProductController(){
            MapProductAction = new SimpleAction(this, MapItActionId, PredefinedCategory.View){
                ImageName = "MapIt", PaintStyle = ActionItemPaintStyle.Image,SelectionDependencyType = SelectionDependencyType.RequireSingleObject
            };
            MapProductAction.Executed+=MapProductActionOnExecuted;
        }

        private void MapProductActionOnExecuted(object sender, ActionBaseEventArgs e){
            var objectSpace = Application.CreateObjectSpace(typeof(Product));
            e.ShowViewParameters.CreatedView = Application.CreateDetailView(objectSpace,
                (IModelDetailView)Application.Model.Views[Product.MapsDetailView], false, objectSpace.GetObject(View.CurrentObject));
            e.ShowViewParameters.TargetWindow=TargetWindow.NewModalWindow;
            e.ShowViewParameters.Controllers.Add(Application.CreateController<DialogController>());
        }


        public SimpleAction MapProductAction{ get; }
        
    }
}