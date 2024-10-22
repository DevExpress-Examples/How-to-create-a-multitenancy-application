using Aqua.EnumerableExtensions;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Office;
using OutlookInspired.Module.BusinessObjects;
using OutlookInspired.Module.Features.Employees;
using OutlookInspired.Module.Features.Maps;
using static OutlookInspired.Module.Services.Internal.MailMergeExtensions;

namespace OutlookInspired.Module.Features.Customers{
    public class MailMergeController:ObjectViewController<ObjectView,Employee>{
        private RichTextShowInDocumentControllerBase _textShowInDocumentController;

        protected override void OnDeactivated(){
            base.OnDeactivated();
            if (_textShowInDocumentController==null)return;
            _textShowInDocumentController.ShowInDocumentAction.ItemsChanged-=ShowInDocumentActionOnItemsChanged;
        }

        protected override void OnActivated(){
            base.OnActivated();
            _textShowInDocumentController = Frame.GetController<RichTextShowInDocumentControllerBase>();
            if (_textShowInDocumentController==null)return;
            _textShowInDocumentController.ShowInDocumentAction.ItemsChanged+=ShowInDocumentActionOnItemsChanged;
        }

        private void ShowInDocumentActionOnItemsChanged(object sender, ItemsChangedEventArgs e){
            if (e.ChangedItemsInfo.All(pair => pair.Value == ChoiceActionItemChangesType.ItemsAdd)){
                ((SingleChoiceAction)sender).Items.ForEach(item => item.ImageName = ((MailMergeDataInfo)item.Data).DisplayName 
                    switch{
                        MonthAward => "EmployeeQuickAward",
                        ProbationNotice => "EmployeeQuickProbationNotice",
                        ServiceExcellence => "EmployeeQuickExellece",
                        ThankYouNote => "ThankYouNote",
                        WelcomeToDevAV => "EmployeeQuickWelcome",
                        _ => item.ImageName
                    });
            }
        }
    }
}