using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.Model.Core;
using DevExpress.ExpressApp.Model.NodeGenerators;

namespace OutlookInspired.Module.ModelUpdaters{
    public class DashboardViewsModelUpdater : ModelNodesGeneratorUpdater<ModelViewsNodesGenerator>{
        
        public const string Opportunities = "Opportunities";
        
        public override void UpdateNode(ModelNode node) => ((IModelViews)node).AddNode<IModelDashboardView>(Opportunities);
    }
}