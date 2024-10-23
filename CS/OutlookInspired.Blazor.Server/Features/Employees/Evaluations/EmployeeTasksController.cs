using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Blazor.Components;
using DevExpress.ExpressApp.Blazor.Components.Models;
using DevExpress.ExpressApp.Blazor.Editors;
using OutlookInspired.Blazor.Server.Services.Internal;
using OutlookInspired.Module.BusinessObjects;
using OutlookInspired.Module.Services.Internal;

namespace OutlookInspired.Blazor.Server.Features.Employees.Evaluations{
    public class EmployeeEvaluationsController:ObjectViewController<ListView,Evaluation>{
        public EmployeeEvaluationsController() => TargetViewId=Employee.ChildDetailView;

        protected override void OnViewControlsCreated(){
            base.OnViewControlsCreated();
            if (View.Editor is not DxGridListEditor editor) return;
            var dataColumnModel = editor.GridDataColumnModels.First(model => model.FieldName == View.Model.VisibleMemberViewItems().First().PropertyName);
            dataColumnModel.HeaderCaptionTemplate = _ => _ => { };
            
            dataColumnModel.CellDisplayTemplate = value => {
                var evaluation = ((Evaluation)value.DataItem);
                var model = new EmployeeEvaluationColumnTemplateModel(){
                    Subject = evaluation.Subject,Description = evaluation.DescriptionBytes.ToDocumentText(),
                    Manager = evaluation.Manager.FullName,BonusImage = evaluation.Bonus.ImageName(),RaiseImage =evaluation.Raise.ImageName(),
                    Style = value.FontSize()
                };
                return ComponentModelObserver.Create(model, model.GetComponentContent());
            };

        }
    }
}