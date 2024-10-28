using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.Model.Core;
using DevExpress.ExpressApp.SystemModule;
using static OutlookInspired.Module.ModelUpdaters.DashboardViewsModelUpdater;

namespace OutlookInspired.Module.ModelUpdaters{
    public class NavigationItemsModelUpdater:ModelNodesGeneratorUpdater<NavigationItemNodeGenerator>{
        public const string CustomerListView = "Customer_ListView";
        public const string EmployeeListView = "Employee_ListView";
        public const string EmployeeCardListView = "EmployeeCardListView";
        public const string CustomerCardListView = "CustomerLayoutView_ListView";
        public const string OrderListView = "Order_ListView";
        public const string OrderGridView = "Order_ListView_Detail";
        public const string ProductListView = "Product_ListView";
        public const string ProductCardView = "ProductLayoutView_ListView";
        public const string EvaluationListView = "Evaluation_ListView";
        public const string ModelDifferenceListView = "ModelDifference_ListView";
        public const string WelcomeDetailView = "Welcome_DetailView";
        public const string ReportDataV2ListView = "ReportDataV2_ListView";
        public const string UserListView = "ApplicationUser_ListView";
        public const string UserDetailView = "ApplicationUser_DetailView";
        public const string RoleListView = "PermissionPolicyRole_ListView";
        public const string RichTextMailMergeDataListView = "RichTextMailMergeData_ListView";
        
        public override void UpdateNode(ModelNode node){
            foreach (var node1 in node.Nodes.SelectMany(modelNode => modelNode.Nodes).Cast<IModelNode>().ToArray()){
                node1.Remove();
            }
            foreach (var view in new[]{WelcomeDetailView, EvaluationListView,Opportunities,EmployeeListView,OrderListView,CustomerListView,ProductListView}){
                NewNavigationItem(node.Application, "Default", view);
            }
            foreach (var view in new[]{UserListView,RoleListView,ModelDifferenceListView}){
                NewNavigationItem(node.Application, "Admin Portal", view);
            }

            foreach (var view in new []{ReportDataV2ListView,RichTextMailMergeDataListView}){
                NewNavigationItem(node.Application, "Reports", view);
            }
            ((IModelNavigationItem)node.Nodes.First()["Reports"]).ImageName = "Navigation_Item_Report";
        }
        
        void NewNavigationItem(IModelApplication modelApplication, string defaultGroup, string viewId,
            string imageName = null){
            var item = ShowNavigationItemController.GenerateNavigationItem(modelApplication, defaultGroup, viewId, null, viewId, null);
            item.ImageName = imageName;
        }
    }
}