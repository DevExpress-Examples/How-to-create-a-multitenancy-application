using DevExpress.ExpressApp;
using OutlookInspired.Module.BusinessObjects;
using OutlookInspired.Module.Features.Maps;
using OutlookInspired.Module.Services.Internal;

namespace OutlookInspired.Module.Features.Employees{
    public class RoutePointController:ObjectViewController<DetailView,Employee>{
        private IMapsRouteController _mapsRouteController;
        public RoutePointController() => TargetViewId = Employee.MapsDetailView;

        protected override void OnDeactivated(){
            base.OnDeactivated();
            if (_mapsRouteController != null) _mapsRouteController.RouteCalculated -= OnRouteCalculated;
        }

        protected override void OnActivated(){
            base.OnActivated();
            var employee = ((Employee)View.CurrentObject);
            var homeOffice = ((IModelOptionsHomeOffice)Application.Model.Options).HomeOffice;
            employee.AAddress = $"{homeOffice.Line}, {homeOffice.City}, {homeOffice.State} {homeOffice.City} {homeOffice.ZipCode}";
            employee.BAddress = $"{employee.Address}, {employee.City}, {employee.State} {employee.City} {employee.ZipCode}";
            _mapsRouteController = Frame.GetControllers<IMapsRouteController>().FirstOrDefault();
            if (_mapsRouteController != null) _mapsRouteController.RouteCalculated += OnRouteCalculated;
        }

        private void OnRouteCalculated(object sender, RouteCalculatedArgs e){
            var employee = ((Employee)View.CurrentObject);
            employee.RoutePoints.Clear();
            employee.RoutePoints.Do(employee.RoutePoints.Add).Enumerate();
            View.SetNonTrackedMemberValue<Employee, string>(employee1 => employee1.RouteResult,
                _ => $"{e.Distance:F1} mi, {e.Time:hh\\:mm} min {e.TravelMode}");
        }
    }
}