using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Security;
using DevExpress.ExpressApp.Templates;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl.EF;
using OutlookInspired.Module.BusinessObjects;
using OutlookInspired.Module.Controllers;
using OutlookInspired.Module.Services.Internal;
using static OutlookInspired.Module.Services.Internal.ReportsExtensions;

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
                    new ChoiceActionItem("Report",MailMergeExtensions.MailMergeOrder){ImageName = "CustomerProfileReport"}
                },
                ItemType = SingleChoiceActionItemType.ItemIsOperation
            };
            ReportAction.Executed+=ReportActionOnExecuted;
        }

        public SingleChoiceAction ReportAction{ get; }

        private void ReportActionOnExecuted(object sender, ActionBaseEventArgs e){
            var selectedItemData = (string)ReportAction.SelectedItem.Data;
            if (selectedItemData==null)return;
            if (selectedItemData.Contains("Revenue")){
                var id = ((Order)View.CurrentObject).Customer.ID;
                ReportAction.ShowReportPreview(View.ObjectTypeInfo.Type,selectedItemData == RevenueAnalysis
                    ? CriteriaOperator.FromLambda<OrderItem>(item => item.Order.Customer.ID == id)
                    : CriteriaOperator.Parse($"IsThisMonth([{nameof(OrderItem.Order)}.{nameof(Order.OrderDate)}])"));
            }
            else{
                e.NewDetailView(Order.InvoiceDetailView, TargetWindow.NewModalWindow);
            }
        }

        protected override void OnViewControllersActivated(){
            base.OnViewControllersActivated();
            
            var items = ReportAction.Items.SelectManyRecursive(item => item.Items);
            foreach (var item in items.Where(item => item.Data!=null)){
                var reportDataV2 = ObjectSpace.GetObjectsQuery<ReportDataV2>().FirstOrDefault(v2 => v2.DisplayName==(string)item.Data);
                item.Active["ReportProtection"] = reportDataV2==null||Application.Security.IsGranted(new PermissionRequest(ObjectSpace,
                    reportDataV2.GetType(), SecurityOperations.Read, reportDataV2));
            }
            ReportAction.ApplyMailMergeProtection(item => item.ParentItem==null);
        }
        
    }
}