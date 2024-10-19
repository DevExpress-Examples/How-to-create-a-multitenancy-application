using DevExpress.ExpressApp;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.Persistent.Base;
using static DevExpress.ExpressApp.Blazor.SystemModule.BlazorModificationsController;

namespace OutlookInspired.Blazor.Server.Features.Maps{
    [Obsolete]
    public class MapsViewController:Module.Features.Maps.MapsViewController{
        protected override PredefinedCategory PopupActionsCategory() => PredefinedCategory.PopupActions;

        protected override void Configure(ShowViewParameters parameters){
            base.Configure(parameters);
            ShowSaveButtonsInPopup = false;
            var dialogController = Application.CreateController<DialogController>();
            // dialogController.CancelAction.Active[nameof(MapsViewController)] = false;
            dialogController.AcceptAction.Caption = "OK";
            parameters.Controllers.Add(dialogController);
        }

        protected override string FrameContext() => TemplateContext.PopupWindowContextName;

        protected override void OnActivated(){
            base.OnActivated();
            ShowSaveButtonsInPopup = true;
            PrintPreviewMapAction.Active[GetType().Name] = false;
            ExportMapAction.Active[GetType().Name] = false;
        }
    }
}