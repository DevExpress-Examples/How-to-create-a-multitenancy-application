using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Office;
using DevExpress.ExpressApp.Templates;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl.EF;
using OutlookInspired.Module.BusinessObjects;
using static OutlookInspired.Module.DatabaseUpdate.Updater;


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
            => ShowInDocument();
        
        void ShowInDocument(){
            var showInDocumentAction = Frame.GetController<RichTextShowInDocumentControllerBase>().ShowInDocumentAction;
            showInDocumentAction.Active.RemoveItem("ByAppearance");
            void OnDetailViewCreated(object sender, DetailViewCreatedEventArgs e){
                e.View.Caption = FollowUp;
                Frame.Application.DetailViewCreated-=OnDetailViewCreated;
            }
            Frame.Application.DetailViewCreated += OnDetailViewCreated;
            showInDocumentAction.SelectedItem = showInDocumentAction.Items.First(item => ((MailMergeDataInfo)item.Data).DisplayName == FollowUp);
            showInDocumentAction.DoExecute(showInDocumentAction.SelectedItem);
            showInDocumentAction.Active["ByAppearance"] = false;
        }

        
        protected override void OnViewControllersActivated(){
            base.OnViewControllersActivated();
            _refundAction.Enabled[nameof(FollowUpController)] = ObjectSpace.GetObjectsQuery<RichTextMailMergeData>().Any(data => data.Name == FollowUp);
        }
        
        
    }
}