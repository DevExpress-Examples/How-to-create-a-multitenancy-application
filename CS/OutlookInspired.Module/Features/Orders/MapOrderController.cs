using System.Drawing;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.Templates;
using DevExpress.Persistent.Base;
using OutlookInspired.Module.BusinessObjects;

namespace OutlookInspired.Module.Features.Orders{
     class MapOrderController:ObjectViewController<ObjectView,Order>{
        public const string MapItActionId = "MapOrder";
        public MapOrderController(){
            MapOrderAction = new PopupWindowShowAction(this, MapItActionId, PredefinedCategory.View){
                ImageName = "MapIt", PaintStyle = ActionItemPaintStyle.Image,SelectionDependencyType = SelectionDependencyType.RequireSingleObject
            };
            MapOrderAction.CustomizePopupWindowParams+=MapOrderActionOnCustomizePopupWindowParams;
        }

        private void MapOrderActionOnCustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e){
            var objectSpace = Application.CreateObjectSpace(typeof(Order));
            var createdView = Application.CreateDetailView(objectSpace,
                (IModelDetailView)Application.Model.Views[Order.MapsDetailView], false, objectSpace.GetObject(View.CurrentObject));
            e.View=createdView;
            e.Size=new Size(1024,768);
        }

        public PopupWindowShowAction MapOrderAction{ get; }
        
    }
}