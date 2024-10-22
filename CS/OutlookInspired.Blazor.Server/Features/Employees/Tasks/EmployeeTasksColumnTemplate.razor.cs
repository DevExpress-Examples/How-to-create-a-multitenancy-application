using DevExpress.ExpressApp.Blazor.Components.Models;

namespace OutlookInspired.Blazor.Server.Features.Employees.Tasks{

    public class TasksColumnTemplateModel:ComponentModelBase{
        public override Type ComponentType => typeof(EmployeeTasksColumnTemplate);

        public string Subject{
            get => GetPropertyValue<string>();
            set => SetPropertyValue(value);
        }
        public string Description{
            get => GetPropertyValue<string>();
            set => SetPropertyValue(value);
        }
        public string Date{
            get => GetPropertyValue<string>();
            set => SetPropertyValue(value);
        }
        public int Progress{
            get => GetPropertyValue<int>();
            set => SetPropertyValue(value);
        }
        
        
    }
}