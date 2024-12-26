using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Templates;
using DevExpress.Persistent.Base;
using OutlookInspired.Module.BusinessObjects;
using OutlookInspired.Module.Features.Reports;
using static OutlookInspired.Module.OutlookInspiredModule;


namespace OutlookInspired.Module.Features.Products{

    public class ProductReportController:ObjectViewController<ObjectView,Product>,IReportController{
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
            ReportAction.SelectionDependencyType = SelectionDependencyType.RequireSingleObject;
            ReportAction.Executed+=ReportActionOnExecuted;
        }

        public SingleChoiceAction ReportAction{ get; }

        private void ReportActionOnExecuted(object sender, ActionBaseEventArgs e){
            var reportController = Frame.GetController<ShowReportController>();
            reportController.ShowReportPreview(ReportAction,(string)ReportAction.SelectedItem.Data == ProductProfile
                ? CriteriaOperator.FromLambda<Product>(product => product.ID == ((Product)View.CurrentObject).ID)
                : CriteriaOperator.FromLambda<OrderItem>(orderItem => orderItem.Product.ID == ((Product)View.CurrentObject).ID), "Product");
        }
    }
}