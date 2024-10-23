using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Templates;
using DevExpress.Persistent.Base;
using OutlookInspired.Module.BusinessObjects;

namespace OutlookInspired.Module.Features.Orders{
    public class RefundController:ObjectViewController<ObjectView,Order>{
        public RefundController(){
            TargetObjectType = typeof(Order);
            var refundAction = new SimpleAction(this, "RefundOrder", PredefinedCategory.Edit){
                ImageName = "Refund", SelectionDependencyType = SelectionDependencyType.RequireSingleObject,PaintStyle = ActionItemPaintStyle.Image,
                TargetObjectsCriteria = CriteriaOperator.FromLambda<Order>(order => order.PaymentStatus==PaymentStatus.PaidInFull).ToString(),
                ToolTip = "Issue full refund"
            };
            refundAction.Executed+=EditInvoiceActionOnExecuted;
        }

        private void EditInvoiceActionOnExecuted(object sender, ActionBaseEventArgs e){
            var order = ((Order)View.CurrentObject);
            order.RefundTotal = order.PaymentTotal;
            ObjectSpace.CommitChanges();
        }

    }
}