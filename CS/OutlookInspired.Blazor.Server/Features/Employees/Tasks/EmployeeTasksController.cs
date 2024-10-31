using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Blazor.Editors;
using OutlookInspired.Module.BusinessObjects;

namespace OutlookInspired.Blazor.Server.Features.Employees.Tasks{
    public class EmployeeTasksController:ObjectViewController<ListView,EmployeeTask>{
        public EmployeeTasksController() => TargetViewId=EmployeeTask.AssignedTasksChildListView;

        protected override void OnViewControlsCreated(){
            base.OnViewControlsCreated();
            if (View.Editor is not DxGridListEditor editor) return;
            var subjectDataColumnModel = editor.GridDataColumnModels.First(model => model.FieldName==nameof(EmployeeTask.Subject));
            subjectDataColumnModel.HeaderCaptionTemplate = _ => _ => { };
            
            subjectDataColumnModel.CellDisplayTemplate = value => {
                var employeeTask = (EmployeeTask)value.DataItem;
                return EmployeeTasksColumnTemplate.Create(employeeTask.Subject,employeeTask.DescriptionText,employeeTask.Completion,
                    employeeTask.DueDate.GetValueOrDefault().ToString("MMMM dd, yyyy"));
            };

        }
    }
}