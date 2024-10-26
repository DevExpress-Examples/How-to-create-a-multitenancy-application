using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.Templates;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl.EF;
using OutlookInspired.Module.BusinessObjects;
using OutlookInspired.Module.Features.Security;
using static OutlookInspired.Module.DatabaseUpdate.Updater;
using static OutlookInspired.Module.OutlookInspiredModule;


namespace OutlookInspired.Module.Features.Orders{
    public class OrdersReportController:ObjectViewController<ObjectView,Order>,IReportController{
        public const string ReportActionId = "OrderReport";
        public OrdersReportController(){
            TargetObjectType = typeof(Order);
            ReportAction = new SingleChoiceAction(this, ReportActionId, PredefinedCategory.Reports){
                ImageName = "BO_Report", SelectionDependencyType = SelectionDependencyType.RequireSingleObject,PaintStyle = ActionItemPaintStyle.Image,
                Items ={
                    new ChoiceActionItem("Revenue",null){ImageName ="CostAnalysis", Items ={
                        new ChoiceActionItem("Report", RevenueReport){ImageName = "CustomerProfileReport"},
                        new ChoiceActionItem("Analysis", RevenueAnalysis){ImageName = "SalesAnalysis"}
                    }},
                    new ChoiceActionItem("Report",MailMergeOrder){ImageName = "CustomerProfileReport"}
                },
                ItemType = SingleChoiceActionItemType.ItemIsOperation
            };
            ReportAction.Executed+=ReportActionOnExecuted;
        }

        public SingleChoiceAction ReportAction{ get; }

        private void ReportActionOnExecuted(object sender, ActionBaseEventArgs e){
            var selectedItemData = (string)ReportAction.SelectedItem.Data;
            if (selectedItemData==null)return;
            var reportController = Frame.GetController<ShowReportController>();
            if (selectedItemData.Contains("Revenue")){
                var xafDataViewRecord = ((SingleChoiceActionExecuteEventArgs)e).SelectedObjects.Cast<ObjectRecord>().First();
                var order = ObjectSpace.GetObjectByKey<Order>(xafDataViewRecord.ObjectKeyValue);
                reportController.ShowReportPreview(ReportAction,selectedItemData == RevenueAnalysis
                    ? CriteriaOperator.FromLambda<OrderItem>(item => item.Order.Store.Customer.ID == order.Store.Customer.ID)
                    : CriteriaOperator.Parse($"IsThisMonth([{nameof(OrderItem.Order)}.{nameof(Order.OrderDate)}])"));
            }
            else{
                e.ShowViewParameters.TargetWindow=TargetWindow.NewModalWindow;
                var objectRecord = (ObjectRecord)((SingleChoiceActionExecuteEventArgs)e).SelectedObjects.Cast<object>().First();
                var modelDetailView = (IModelDetailView)e.Action.Application.FindModelView(Order.InvoiceDetailView);
                var objectSpace = Application.CreateObjectSpace(objectRecord.GetType());
                var detailView = Application.CreateDetailView(objectSpace,modelDetailView,false,objectSpace.GetObjectByKey<Order>(objectRecord.ObjectKeyValue));
                e.ShowViewParameters.TargetWindow=TargetWindow.NewModalWindow;
                e.ShowViewParameters.CreatedView = detailView;
            }
        }

        protected override void OnViewControllersActivated(){
            base.OnViewControllersActivated();
            var item = ReportAction.Items.First(item => (string)item.Data==MailMergeOrder);
            item.Enabled["MailMergeProtection"] = ObjectSpace.GetObjectsQuery<RichTextMailMergeData>()
                .Any(data => data.Name == (string)item.Data);

        }
        
    }
}