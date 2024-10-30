using DevExpress.ExpressApp;
using OutlookInspired.Blazor.Server.Editors.LayoutView;
using OutlookInspired.Module.BusinessObjects;

namespace OutlookInspired.Blazor.Server.Features.Employees {
    public class EmployeeLayoutViewController : ObjectViewController<ListView, Employee> {
        protected override void OnViewControlsCreated() {
            base.OnViewControlsCreated();
            if(View.Editor is LayoutViewListEditor editor) {
                var model = editor.Control;
                model.ImageSelector = o => ((Employee)o).Picture?.Data;
                model.HeaderSelector = o => ((Employee)o).FullName;
                model.InfoItemsSelector = o => {
                    var employee = (Employee)o;
                    return new Dictionary<string, string>{
                    { "ADDRESS", employee.Address },
                    { "EMAIL", $"<a href=\"mailto:{employee.Email}\">{employee.Email}</a>" },
                    { "PHONE", employee.HomePhone }
                };
                };
            }
        }
    }
}