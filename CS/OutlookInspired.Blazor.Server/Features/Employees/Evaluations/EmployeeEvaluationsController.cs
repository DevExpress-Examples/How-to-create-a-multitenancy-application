using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Blazor.Components;
using DevExpress.ExpressApp.Blazor.Components.Models;
using DevExpress.ExpressApp.Blazor.Editors;
using DevExpress.ExpressApp.Utils;
using OutlookInspired.Module.Attributes;
using OutlookInspired.Module.BusinessObjects;

namespace OutlookInspired.Blazor.Server.Features.Employees.Evaluations{
    
    public class EmployeeEvaluationsController:ObjectViewController<ListView,Evaluation>{
        public EmployeeEvaluationsController() => TargetViewId = Evaluation.EmployeeEvaluationsChildListView;

        protected override void OnViewControlsCreated(){
            base.OnViewControlsCreated();
            if (View.Editor is not DxGridListEditor editor) return;
            var subjectDataColumnModel = editor.GridDataColumnModels.First(model => model.FieldName==nameof(Evaluation.Subject));
            subjectDataColumnModel.HeaderCaptionTemplate = _ => _ => { };
            subjectDataColumnModel.CellDisplayTemplate = context => {
                var evaluation = ((Evaluation)context.DataItem);
                var memberInfo = ((IObjectSpaceLink)context.DataItem).ObjectSpace.TypesInfo.FindTypeInfo(typeof(Evaluation)).FindMember(context.DataColumn.FieldName);
                var fontSizeDeltaAttribute = memberInfo.FindAttribute<FontSizeDeltaAttribute>();
                var model = new EmployeeEvaluationColumnTemplateModel(){
                    Subject = evaluation.Subject,Description = evaluation.Description,
                    Manager = evaluation.Manager.FullName,
                    BonusImage = ImageLoader.Instance.GetEnumValueImageName(evaluation.Bonus),
                    RaiseImage =ImageLoader.Instance.GetEnumValueImageName(evaluation.Raise),
                    Style = fontSizeDeltaAttribute?.Style()
                };
                return ComponentModelObserver.Create(model, model.GetComponentContent());
            };

        }
    }
}