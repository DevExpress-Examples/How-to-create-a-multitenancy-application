using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Templates;
using DevExpress.Persistent.Base;
using DevExpress.XtraMap;
using OutlookInspired.Module.BusinessObjects;
using OutlookInspired.Win.Editors.Maps;

namespace OutlookInspired.Win.Features.Employees{
    public class MapsTravelModeViewController:ObjectViewController<DetailView,Employee>{
        private readonly SingleChoiceAction _action;

        public MapsTravelModeViewController(){
            TargetViewId = Employee.MapsDetailView;
            _action = new SingleChoiceAction(this,"TravelMode",PredefinedCategory.View);
            _action.Items.AddRange([new ChoiceActionItem("Driving", BingTravelMode.Driving){ ImageName = "Driving" },
                new ChoiceActionItem("Walking", BingTravelMode.Walking){ ImageName = "Walking" }
            ]);
            _action.SelectedItem = _action.Items.First();
            _action.ItemType=SingleChoiceActionItemType.ItemIsOperation;
            _action.ImageMode=ImageMode.UseItemImage;
            _action.DefaultItemMode = DefaultItemMode.LastExecutedItem;
            _action.PaintStyle=ActionItemPaintStyle.Image;
            _action.Executed+=ActionOnExecuted;
        }


        private void ActionOnExecuted(object sender, ActionBaseEventArgs e){
            var editor = View.GetItems<MapControlHomeOfficePropertyEditor>().First();
            editor.CalculateRoute((BingTravelMode)_action.SelectedItem.Data);
        }
    }

}