using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Templates;
using DevExpress.Persistent.Base;
using OutlookInspired.Module.BusinessObjects;
using OutlookInspired.Module.Features.Maps;

namespace OutlookInspired.Module.Features.Orders{
    public class PayController:ObjectViewController<ObjectView,Order>{
        public PayController(){
            TargetObjectType = typeof(Order);
            var payOrderAction = new SimpleAction(this, "PayOrder", PredefinedCategory.Edit){
                ImageName = "Payment", SelectionDependencyType = SelectionDependencyType.RequireSingleObject,PaintStyle = ActionItemPaintStyle.Image,
                TargetObjectsCriteria = CriteriaOperator.FromLambda<Order>(order => order.PaymentStatus==PaymentStatus.Unpaid).ToString(),
                ToolTip = "Mark as paid"
            };
            payOrderAction.Executed+=EditInvoiceActionOnExecuted;
        }

        private void EditInvoiceActionOnExecuted(object sender, ActionBaseEventArgs e){
            var order = ((Order)View.CurrentObject);
            order.PaymentTotal = order.TotalAmount;
            ObjectSpace.CommitChanges();
        }

    }
}