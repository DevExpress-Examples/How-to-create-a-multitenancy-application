using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.Model.Core;
using DevExpress.ExpressApp.Model.NodeGenerators;
using OutlookInspired.Module.Services.Internal;

namespace OutlookInspired.Module.Features.CloneView;
public class CloneViewUpdater : ModelNodesGeneratorUpdater<ModelViewsNodesGenerator> {
    public override void UpdateNode(ModelNode node){
        foreach (var modelClass in node.Application.BOModel){
            foreach (var attribute in modelClass.Attributes<CloneViewAttribute>()
                         .OrderBy(viewAttribute => viewAttribute.ViewType)){
                var modelView = GetModelView(modelClass, attribute.ViewType);
                modelView.CreateView(attribute.ViewId, attribute.DetailView);
            }    
        }
    }

    IModelView GetModelView(IModelClass modelClass, CloneViewType viewType) 
        => viewType == CloneViewType.LookupListView ? modelClass.DefaultLookupListView
            : viewType == CloneViewType.DetailView ? modelClass.DefaultDetailView : modelClass.DefaultListView;
}