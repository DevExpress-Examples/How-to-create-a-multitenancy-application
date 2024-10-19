using System.ComponentModel;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.Templates;

namespace OutlookInspired.Module.Controllers{
    public interface IModelViewHideToolbar{
        [Category(OutlookInspiredModule.ModelCategory)]
        bool HideToolBar{ get; set; }
    }

    public class HideToolBarController:ViewController,IModelExtender{
        protected override void OnViewControlsCreated(){
            base.OnViewControlsCreated();
            if (View.Model is not IModelViewHideToolbar{ HideToolBar: true }) return;
            ((ISupportActionsToolbarVisibility)Frame.Template).SetVisible(false);
        }

        public void ExtendModelInterfaces(ModelInterfaceExtenders extenders){
            extenders.Add<IModelListView, IModelViewHideToolbar>();
            extenders.Add<IModelDashboardView, IModelViewHideToolbar>();
        }
    }
}