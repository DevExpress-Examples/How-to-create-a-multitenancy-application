using DevExpress.ExpressApp;
using OutlookInspired.Blazor.Server.Editors;
using OutlookInspired.Blazor.Server.Editors.Maps;
using OutlookInspired.Module.BusinessObjects;
using OutlookInspired.Module.Features.Maps;
using OutlookInspired.Module.Services.Internal;

namespace OutlookInspired.Blazor.Server.Features.Employees.Maps{
    public class ManeuverInstructionsController:ObjectViewController<DetailView,Employee>{
        public ManeuverInstructionsController() => TargetViewId = Employee.MapsDetailView;

        protected override void OnActivated(){
            base.OnActivated();
            View.CustomizeViewItemControl<DxMapPropertyEditor>(this, editor => editor.RouteCalculated+=EditorOnRouteCalculated);
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