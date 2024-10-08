using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Templates;
using DevExpress.Persistent.Base;
using DevExpress.XtraMap;
using OutlookInspired.Module.BusinessObjects;
using OutlookInspired.Module.Features.Maps;
using OutlookInspired.Module.Services.Internal;
using OutlookInspired.Win.Editors;

namespace OutlookInspired.Win.Features.Employees.Maps{
    
    public class TravelModeViewController:ObjectViewController<DetailView,Employee>{
        private readonly SingleChoiceAction _action;

        public TravelModeViewController(){
            TargetViewId = Employee.MapsDetailView;
            _action = new SingleChoiceAction(this,"EmployeeTravelMode",PredefinedCategory.View);
            _action.Caption = "Travel Mode";
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

        private void ActionOnExecuted(object sender, ActionBaseEventArgs e) 
            => View.GetItems<MapControlRoutePropertyEditor>().First().CalculateRoute((BingTravelMode)_action.SelectedItem.Data);

        protected override void OnActivated(){
            base.OnActivated();
            View.CustomizeViewItemControl<MapControlRoutePropertyEditor>(this, editor => editor.RouteCalculated+=EditorOnRouteCalculated);
        }

        private void EditorOnRouteCalculated(object sender, RouteCalculatedArgs e){
            var routePoints = ((Employee)View.CurrentObject).RoutePoints;
            routePoints.Clear();
            e.RoutePoints.Do(routePoints.Add).Enumerate();
            View.SetNonTrackedMemberValue<Employee, string>(employee => employee.RouteResult,
                _ => $"{e.Distance:F1} mi, {e.Time:hh\\:mm} min {e.TravelMode}");
        }
    }

}