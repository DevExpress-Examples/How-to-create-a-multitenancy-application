using System.Drawing;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Templates;
using DevExpress.Persistent.Base;
using OutlookInspired.Module.BusinessObjects;

namespace OutlookInspired.Module.Features.Quotes{
    public class MapOpportunitiesController:ObjectViewController<ListView,QuoteAnalysis>{
        public const string MapItActionId = "MapOpportunity";
        public MapOpportunitiesController(){
            MapOpportunitiesAction = new PopupWindowShowAction(this, MapItActionId, PredefinedCategory.View){
                ImageName = "MapIt", PaintStyle = ActionItemPaintStyle.Image
            };
            MapOpportunitiesAction.CustomizePopupWindowParams+=MapOpportunitiesActionOnCustomizePopupWindowParams;
        }

        private void MapOpportunitiesActionOnCustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e){
            var objectSpace = Application.CreateObjectSpace(typeof(QuoteMapItem));
            var createdView = Application.CreateListView(objectSpace, typeof(QuoteMapItem),false);
            e.View=createdView;
            e.Size=new Size(1024,768);
        }

        public PopupWindowShowAction MapOpportunitiesAction{ get; }
        
    }
}