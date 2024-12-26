using System.Drawing;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.Templates;
using DevExpress.Persistent.Base;
using OutlookInspired.Module.BusinessObjects;

namespace OutlookInspired.Module.Features.Employees{
     class MapEmployeeController:ObjectViewController<ListView,Employee>{
        public const string MapItActionId = "MapEmployee";
        public MapEmployeeController(){
            MapEmployeeAction = new PopupWindowShowAction(this, MapItActionId, PredefinedCategory.View){
                ImageName = "MapIt", PaintStyle = ActionItemPaintStyle.Image,SelectionDependencyType = SelectionDependencyType.RequireSingleObject
            };
            MapEmployeeAction.CustomizePopupWindowParams+=MapEmployeeActionOnCustomizePopupWindowParams;
        }

        private void MapEmployeeActionOnCustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e){
            var objectSpace = Application.CreateObjectSpace(typeof(Employee));
            var createdView = Application.CreateDetailView(objectSpace,
                (IModelDetailView)Application.Model.Views[Employee.MapsDetailView], false, objectSpace.GetObject(View.CurrentObject));
            e.View=createdView;
            e.Size=new Size(1024,768);
        }

        public PopupWindowShowAction MapEmployeeAction{ get; }
        
    }

}