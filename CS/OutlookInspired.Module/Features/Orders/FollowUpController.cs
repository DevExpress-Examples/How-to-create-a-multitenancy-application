using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Templates;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl.EF;
using OutlookInspired.Module.BusinessObjects;
using OutlookInspired.Module.Services.Internal;
using static OutlookInspired.Module.Services.Internal.MailMergeExtensions;

namespace OutlookInspired.Module.Features.Orders{
    public class FollowUpController:ObjectViewController<ObjectView,Order>{
        private readonly SimpleAction _refundAction;

        public FollowUpController(){
            TargetObjectType = typeof(Order);
            _refundAction = new SimpleAction(this, FollowUp, PredefinedCategory.Edit){
                ImageName = "ThankYouNote", SelectionDependencyType = SelectionDependencyType.RequireSingleObject,PaintStyle = ActionItemPaintStyle.Image,
            };
            _refundAction.Executed+=EditInvoiceActionOnExecuted;
        }

        private void EditInvoiceActionOnExecuted(object sender, ActionBaseEventArgs e) 
            => Frame.ShowInDocument(FollowUp);
        
        protected override void OnViewControllersActivated(){
            base.OnViewControllersActivated();
            _refundAction.Enabled[nameof(FollowUpController)] = ObjectSpace.Any<RichTextMailMergeData>(data => data.Name == FollowUp);
        }
    }
}