using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.Security;
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
                var id = ((Order)View.CurrentObject).Customer.ID;
                reportController.ShowReportPreview(ReportAction,selectedItemData == RevenueAnalysis
                    ? CriteriaOperator.FromLambda<OrderItem>(item => item.Order.Customer.ID == id)
                    : CriteriaOperator.Parse($"IsThisMonth([{nameof(OrderItem.Order)}.{nameof(Order.OrderDate)}])"));
            }
            else{
                
                e.ShowViewParameters.TargetWindow=TargetWindow.NewModalWindow;
                var instance = ((SingleChoiceActionExecuteEventArgs)e).SelectedObjects.Cast<object>().First();
                var modelDetailView = (IModelDetailView)e.Action.Application.FindModelView(Order.InvoiceDetailView);
                var objectSpace = Application.CreateObjectSpace(instance.GetType());
                var detailView = Application.CreateDetailView(objectSpace,modelDetailView,false,objectSpace.GetObject(instance));
                e.ShowViewParameters.CreatedView = detailView;
            }
        }

        protected override void OnViewControllersActivated(){
            base.OnViewControllersActivated();
            var items = SelectManyRecursive(ReportAction.Items,item => item.Items).Where(item => item.Data!=null).ToArray();
            foreach (var item in items){
                var reportDataV2 = ObjectSpace.GetObjectsQuery<ReportDataV2>().FirstOrDefault(v2 => v2.DisplayName==(string)item.Data);
                item.Active["ReportProtection"] = reportDataV2==null|| ((IRequestSecurity)Application.Security)
                    .IsGranted(new PermissionRequest(ObjectSpace,reportDataV2.GetType(),SecurityOperations.Read,reportDataV2));
            }

            foreach (var item in items.Where(item => item.Data!=null)){
                item.Enabled["MailMergeProtection"] = ReportAction.Controller.Frame.View.ObjectSpace
                    .GetObjectsQuery<RichTextMailMergeData>()
                    .Any(data => data.Name == (string)item.Data);

            }

        }
        
        IEnumerable<T> SelectManyRecursive<T>(IEnumerable<T> source, Func<T, IEnumerable<T>> childrenSelector){
            foreach (var i in source){
                yield return i;
                var children = childrenSelector(i);
                if (children == null) continue;
                foreach (var child in SelectManyRecursive(children, childrenSelector))
                    yield return child;
            }
        }

    }
}