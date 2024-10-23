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

namespace OutlookInspired.Module.Features.Customers{
    public class CustomerReportController:ObjectViewController<ListView,Customer>{
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
            if (selectedItemData == SalesSummaryReport){
                ReportAction.ShowReportPreview(View.ObjectTypeInfo.Type, CriteriaOperator.FromLambda<OrderItem>(item 
                    => item.Order.Customer.ID == ((Customer)View.CurrentObject).ID),"Customer");
            }
            else if (selectedItemData == LocationsReport){
                ReportAction.ShowReportPreview(View.ObjectTypeInfo.Type,CriteriaOperator.FromLambda<Customer>(customer
                    => customer.ID == ((Customer)View.CurrentObject).ID));
            }
            else if (selectedItemData == Contacts){
                ReportAction.ShowReportPreview(View.ObjectTypeInfo.Type,CriteriaOperator.FromLambda<CustomerEmployee>(customerEmployee
                    => customerEmployee.Customer.ID == ((Customer)View.CurrentObject).ID));
            }
        }

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