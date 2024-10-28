using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Templates;
using DevExpress.Persistent.Base;
using OutlookInspired.Module.BusinessObjects;
using OutlookInspired.Module.Features.Reports;
using static OutlookInspired.Module.OutlookInspiredModule;


namespace OutlookInspired.Module.Features.Customers{
    public class CustomerReportController:ObjectViewController<ListView,Customer>,IReportController{
        public const string ReportActionId = "CustomerReport";
        public CustomerReportController(){
            TargetObjectType = typeof(Customer);
            ReportAction = new SingleChoiceAction(this, ReportActionId, PredefinedCategory.Reports){
                ImageName = "BO_Report", SelectionDependencyType = SelectionDependencyType.RequireSingleObject,PaintStyle = ActionItemPaintStyle.Image,
                Items ={
                    new ChoiceActionItem("Sales",SalesSummaryReport){ImageName ="CustomerQuickSales"},
                    new ChoiceActionItem("Employees",Contacts){ImageName = "EmployeeProfile"},
                    new ChoiceActionItem("Locations",LocationsReport){ImageName = "CustomerQuickLocations"}
                },
                ItemType = SingleChoiceActionItemType.ItemIsOperation
            };
            ReportAction.Executed+=ReportActionOnExecuted;
        }

        public SingleChoiceAction ReportAction{ get; }
        

        private void ReportActionOnExecuted(object sender, ActionBaseEventArgs e){
            var selectedItemData = (string)ReportAction.SelectedItem.Data;
            var reportController = Frame.GetController<ShowReportController>();
            if (selectedItemData == SalesSummaryReport){
                reportController.ShowReportPreview(ReportAction,CriteriaOperator.FromLambda<OrderItem>(item 
                    => item.Order.Customer.ID == ((Customer)View.CurrentObject).ID),"Customer");
            }
            else if (selectedItemData == LocationsReport){
                reportController.ShowReportPreview(ReportAction,CriteriaOperator.FromLambda<Customer>(customer
                    => customer.ID == ((Customer)View.CurrentObject).ID));
            }
            else if (selectedItemData == Contacts){
                reportController.ShowReportPreview(ReportAction,CriteriaOperator.FromLambda<CustomerEmployee>(customerEmployee
                    => customerEmployee.Customer.ID == ((Customer)View.CurrentObject).ID));
            }
        }


    }
}