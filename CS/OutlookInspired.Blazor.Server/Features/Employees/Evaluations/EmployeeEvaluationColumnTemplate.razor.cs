using DevExpress.ExpressApp.Blazor.Components.Models;

namespace OutlookInspired.Blazor.Server.Features.Employees.Evaluations{
    public class EmployeeEvaluationColumnTemplateModel:ComponentModelBase{
        public override Type ComponentType => typeof(EmployeeEvaluationColumnTemplate);

        public string Subject{
            get => GetPropertyValue<string>();
            set => SetPropertyValue(value);
        }
        
        public string RaiseImage{
            get => GetPropertyValue<string>();
            set => SetPropertyValue(value);
        }
        public string BonusImage{
            get => GetPropertyValue<string>();
            set => SetPropertyValue(value);
        }
        public string Description{
            get => GetPropertyValue<string>();
            set => SetPropertyValue(value);
        }
        public string Style{
            get => GetPropertyValue<string>();
            set => SetPropertyValue(value);
        }
        public string Manager{
            get => GetPropertyValue<string>();
            set => SetPropertyValue(value);
        }
        
        
        
    }
}