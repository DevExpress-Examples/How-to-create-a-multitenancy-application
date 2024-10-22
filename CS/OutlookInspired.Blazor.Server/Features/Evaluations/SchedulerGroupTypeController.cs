using DevExpress.Blazor;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Scheduler.Blazor.Components.Models;
using DevExpress.ExpressApp.Scheduler.Blazor.Editors;
using OutlookInspired.Module.BusinessObjects;

namespace OutlookInspired.Blazor.Server.Features.Evaluations{
    public class SchedulerGroupTypeController:ObjectViewController<ListView,Evaluation>{
        protected override void OnViewControlsCreated(){
            base.OnViewControlsCreated();
            if (View.Editor is not SchedulerListEditor editor)return;
            ((DxSchedulerModel)editor.Control).GroupType = SchedulerGroupType.None;
        }
    }
}