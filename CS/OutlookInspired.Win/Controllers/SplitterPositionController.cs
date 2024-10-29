using DevExpress.ExpressApp;
using DevExpress.XtraEditors;
using OutlookInspired.Module;

namespace OutlookInspired.Win.Controllers{
    
    public class SplitterPositionController : ViewController<ListView> {
        Control _container;

        protected override void OnActivated(){
            base.OnActivated();
            Active[nameof(SplitterPositionController)] =
                View.Model.MasterDetailMode == MasterDetailMode.ListViewAndDetailView &&
                ((IModelListViewSplitterRelativePosition)View.Model.SplitLayout).RelativePosition > 0;
        }

        protected override void OnViewControlsCreated() {
            base.OnViewControlsCreated();
            if (View.Model.MasterDetailMode != MasterDetailMode.ListViewAndDetailView) return;
            _container = (Control)View.LayoutManager.Container;
            _container.Layout += Container_Layout;
            if(_container is SplitContainerControl control) {
                control.FixedPanel = SplitFixedPanel.None;
            }
        }

        private void Container_Layout(object sender, LayoutEventArgs e){
            var width = _container.ClientSize.Width * ((IModelListViewSplitterRelativePosition)View.Model.SplitLayout).RelativePosition / 100;
            switch (_container){
                case SplitContainerControl splitContainerControl:
                    splitContainerControl.SplitterPosition = width;
                    break;
                case SidePanelContainer sidePanelContainer:
                    sidePanelContainer.FixedPanelWidth = width;
                    break;
            }
        }

        protected override void OnDeactivated() {
            base.OnDeactivated();
            if (_container == null) return;
            _container.Layout -= Container_Layout ;
        }

    }
}