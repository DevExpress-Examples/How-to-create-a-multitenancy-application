using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Security;
using DevExpress.ExpressApp.Templates;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl.EF;
using OutlookInspired.Module.BusinessObjects;
using OutlookInspired.Module.Services.Internal;
using static OutlookInspired.Module.Services.Internal.ReportsExtensions;

namespace OutlookInspired.Module.Features.Products{
    public class ProductReportController:ObjectViewController<ObjectView,Product>{
        public const string ReportActionId = "ProductReport";
        public ProductReportController(){
            TargetObjectType = typeof(Product);
            ReportAction = new SingleChoiceAction(this, ReportActionId, PredefinedCategory.Reports){
                ImageName = "BO_Report", SelectionDependencyType = SelectionDependencyType.RequireMultipleObjects,PaintStyle = ActionItemPaintStyle.Image,
                Items ={
                    new ChoiceActionItem("Sales",Sales){ImageName ="CustomerQuickSales"},
                    new ChoiceActionItem("Shippments",OrdersReport){ImageName = "ProductQuickShippments"},
                    new ChoiceActionItem("Comparisons",ProductProfile){ImageName = "ProductQuickComparisons"},
                    new ChoiceActionItem("Top Sales Person",TopSalesPerson){ImageName = "ProductQuicTopSalesperson"}
                },
                ItemType = SingleChoiceActionItemType.ItemIsOperation
            };
            ReportAction.Executed+=ReportActionOnExecuted;
        }

        public SingleChoiceAction ReportAction{ get; }

        private void ReportActionOnExecuted(object sender, ActionBaseEventArgs e) 
            => ReportAction.ShowReportPreview((string)ReportAction.SelectedItem.Data==ProductProfile?CriteriaOperator.FromLambda<Product>(
                product => product.ID == ((Product)View.CurrentObject).ID):CriteriaOperator.FromLambda<OrderItem>(
                orderItem => orderItem.Product.ID == ((Product)View.CurrentObject).ID),"Product");

        protected override void OnViewControllersActivated(){
            base.OnViewControllersActivated();
            var items = ReportAction.Items.SelectManyRecursive(item => item.Items);
            foreach (var item in items.Where(item => item.Data!=null)){
                var reportDataV2 = ObjectSpace.GetObjectsQuery<ReportDataV2>().First(v2 => v2.DisplayName==(string)item.Data);
                var isGranted = Application.Security.IsGranted(new PermissionRequest(ObjectSpace,
                    reportDataV2.GetType(), SecurityOperations.Read, reportDataV2));
                item.Active["ReportProtection"] = isGranted;
            }
        }
    }
}