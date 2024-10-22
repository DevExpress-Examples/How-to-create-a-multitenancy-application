using DevExpress.Blazor;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Templates;
using DevExpress.Persistent.Base;
using OutlookInspired.Blazor.Server.Editors.Maps;
using OutlookInspired.Module.BusinessObjects;

namespace OutlookInspired.Blazor.Server.Features.Employees.Maps{
    public class MapsTravelModeViewController:ObjectViewController<DetailView,Employee>{
        private readonly SingleChoiceAction _action;

        public MapsTravelModeViewController(){
            TargetViewId = Employee.MapsDetailView;
            _action = new SingleChoiceAction(this,"TravelMode",PredefinedCategory.PopupActions);
            _action.Caption = "Travel Mode";
            _action.Items.AddRange([new ChoiceActionItem("Driving", MapRouteMode.Driving){ ImageName = "Driving" },
                new ChoiceActionItem("Walking", MapRouteMode.Walking){ ImageName = "Walking" }
            ]);
            _action.SelectedItem = _action.Items.First();
            _action.ItemType=SingleChoiceActionItemType.ItemIsOperation;
            _action.ImageMode=ImageMode.UseItemImage;
            _action.DefaultItemMode = DefaultItemMode.LastExecutedItem;
            _action.PaintStyle=ActionItemPaintStyle.Image;
            _action.Executed+=ActionOnExecuted;
        }

        private void ActionOnExecuted(object sender, ActionBaseEventArgs e) 
            => View.GetItems<DxMapHomeOfficePropertyEditor>().First().ComponentModel.MapRouteMode = (MapRouteMode)_action.SelectedItem.Data;

        
    }

}