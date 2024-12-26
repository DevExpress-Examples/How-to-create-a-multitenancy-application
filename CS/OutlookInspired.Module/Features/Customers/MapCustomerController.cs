using System.Drawing;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.Templates;
using DevExpress.Persistent.Base;
using OutlookInspired.Module.BusinessObjects;

namespace OutlookInspired.Module.Features.Customers{
     class MapCustomerController:ObjectViewController<ListView,Customer>{
        public const string MapItActionId = "MapCustomer";
        public MapCustomerController(){
            MapCustomerAction = new PopupWindowShowAction(this, MapItActionId, PredefinedCategory.View){
                ImageName = "MapIt", PaintStyle = ActionItemPaintStyle.Image,SelectionDependencyType = SelectionDependencyType.RequireSingleObject
            };
            MapCustomerAction.CustomizePopupWindowParams+=MapCustomerActionOnCustomizePopupWindowParams;
        }

        private void MapCustomerActionOnCustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e){
            var objectSpace = Application.CreateObjectSpace(typeof(Customer));
            var createdView = Application.CreateDetailView(objectSpace,
                (IModelDetailView)Application.Model.Views[Customer.MapsDetailView], false, objectSpace.GetObject(View.CurrentObject));
            e.View = createdView;
            e.Size=new Size(1024,768);
        }

        public PopupWindowShowAction MapCustomerAction{ get; }
        
    }
}