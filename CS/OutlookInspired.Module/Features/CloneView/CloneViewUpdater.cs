using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.Model.Core;
using DevExpress.ExpressApp.Model.NodeGenerators;

namespace OutlookInspired.Module.Features.CloneView;
public class CloneViewUpdater : ModelNodesGeneratorUpdater<ModelViewsNodesGenerator> {
    public override void UpdateNode(ModelNode node){
        foreach (var modelClass in node.Application.BOModel){
            foreach (var attribute in modelClass.TypeInfo.FindAttributes<CloneViewAttribute>()
                         .OrderBy(viewAttribute => viewAttribute.ViewType)){
                var modelView = GetModelView(modelClass, attribute.ViewType);
                CreateView(modelView, attribute.ViewId, attribute.DetailView);
            }    
        }
    }
    void CreateView( IModelView source,  string viewId,string detailViewId=null) {
        var cloneNodeFrom = ((ModelNode)source).Clone(viewId);
        if (source is not IModelListView || string.IsNullOrEmpty(detailViewId)) return;
        ((IModelListView)cloneNodeFrom).DetailView = source.Application.Views.OfType<IModelDetailView>()
            .FirstOrDefault(view => view.Id == detailViewId)??throw new NullReferenceException(detailViewId);
    }

    IModelView GetModelView(IModelClass modelClass, CloneViewType viewType) 
        => viewType == CloneViewType.LookupListView ? modelClass.DefaultLookupListView
            : viewType == CloneViewType.DetailView ? modelClass.DefaultDetailView : modelClass.DefaultListView;
}