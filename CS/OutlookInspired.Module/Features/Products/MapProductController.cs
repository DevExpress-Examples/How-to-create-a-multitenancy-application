using System.Drawing;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.Templates;
using DevExpress.Persistent.Base;
using OutlookInspired.Module.BusinessObjects;

namespace OutlookInspired.Module.Features.Products{
    public class MapProductController:ObjectViewController<ObjectView,Product>{
        public const string MapItActionId = "MapProduct";
        public MapProductController(){
            MapProductAction = new PopupWindowShowAction(this, MapItActionId, PredefinedCategory.View){
                ImageName = "MapIt", PaintStyle = ActionItemPaintStyle.Image,SelectionDependencyType = SelectionDependencyType.RequireSingleObject
            };
            MapProductAction.CustomizePopupWindowParams+=MapProductActionOnCustomizePopupWindowParams;
        }

        private void MapProductActionOnCustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e){
            var objectSpace = Application.CreateObjectSpace(typeof(Product));
            var createdView = Application.CreateDetailView(objectSpace,
                (IModelDetailView)Application.Model.Views[Product.MapsDetailView], false, objectSpace.GetObject(View.CurrentObject));
            e.View=createdView;
            e.Size=new Size(1024,768);
        }

        public PopupWindowShowAction MapProductAction{ get; }
        
    }
}