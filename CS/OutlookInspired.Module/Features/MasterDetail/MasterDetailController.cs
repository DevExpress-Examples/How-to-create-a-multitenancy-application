using System.ComponentModel;
using Aqua.EnumerableExtensions;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Layout;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.Persistent.Base;
using DevExpress.Utils.Serializing.Helpers;
using OutlookInspired.Module.Services.Internal;

namespace OutlookInspired.Module.Features.MasterDetail{
    public interface IModelDashboardViewMasterDetail{
        [Category(OutlookInspiredModule.ModelCategory)]
        bool MasterDetail{ get; set; }
    }
    
    public class MasterDetailController:ViewController<DashboardView>,IModelExtender{
        private readonly SimpleAction _processMasterViewSelectedObjectAction;
        private NestedFrame _masterFrame;
        private NestedFrame _childFrame;
        private ControlViewItem _controlViewItem;
        private IUserControl _userControl;

        public MasterDetailController(){
            _processMasterViewSelectedObjectAction = new SimpleAction(this,"ProcessMasterViewSelectedObject",PredefinedCategory.ListView);
            _processMasterViewSelectedObjectAction.Executed+=(_, e) 
                => e.ShowViewParameters.CreatedView = Application.NewDetailView(e.Action.SelectionContext.CurrentObject);
        }

        protected override void OnDeactivated(){
            base.OnDeactivated();
            if (!((IModelDashboardViewMasterDetail)View.Model).MasterDetail)return;
            if (_userControl != null){
                _userControl.CurrentObjectChanged-=UserControlOnCurrentObjectChanged;
                _userControl.ProcessObject-=UserControlOnProcessObject;
            }

            if (_controlViewItem != null){
                _controlViewItem.ControlCreated-=ControlViewItemOnControlCreated;
            }
            if(_masterFrame?.View?.ObjectSpace != null) {
                _masterFrame.View.ObjectSpace.Committed -= ObjectSpaceOnCommitted;
                _masterFrame.View.SelectionChanged -= ViewOnSelectionChanged;
            }
            if(_childFrame?.View?.ObjectSpace != null) {
                _childFrame.View.ObjectSpace.ModifiedChanged -= ObjectSpaceOnModifiedChanged;
            }            
            View.ChildItem().ControlCreated-=OnChildItemControlCreated;
            View.MasterItem().ControlCreated-=OnChildItemControlCreated;
        }

        protected override void OnViewControlsCreated(){
            base.OnViewControlsCreated();
            if (!((IModelDashboardViewMasterDetail)View.Model).MasterDetail)return;
            var dashboardViewItem = View.MasterItem();
            if (dashboardViewItem.Frame != null){
                OnMasterItemControlCreated(dashboardViewItem, EventArgs.Empty);
                OnChildItemControlCreated(View.ChildItem(),EventArgs.Empty);
            }
            else{
                dashboardViewItem.ControlCreated+= OnMasterItemControlCreated;
                View.ChildItem().ControlCreated+=OnChildItemControlCreated;    
            }
        }

        private void OnChildItemControlCreated(object sender, EventArgs e){
            _childFrame = (NestedFrame)((DashboardViewItem)sender)!.Frame;
            _childFrame.View.ObjectSpace.ModifiedChanged+=ObjectSpaceOnModifiedChanged;
        }

        private void ObjectSpaceOnModifiedChanged(object sender, EventArgs e) => ((IObjectSpace)sender).CommitChanges();

        private void OnMasterItemControlCreated(object sender, EventArgs e){
            _masterFrame = (NestedFrame)(((DashboardViewItem)sender)!).Frame;
            _masterFrame.GetController<NewObjectViewController>().UseObjectDefaultDetailView();
            _controlViewItem = _masterFrame.View.ToCompositeView().GetItems<ControlViewItem>().FirstOrDefault();
            if (_controlViewItem != null){
                _controlViewItem.ControlCreated += ControlViewItemOnControlCreated;
            }
            else{
                _masterFrame.View.SelectionChanged += ViewOnSelectionChanged;
            }
        }
        
        private void ViewOnSelectionChanged(object sender, EventArgs e){
            _childFrame.View.SetCurrentObject(_masterFrame.View.CurrentObject);
            RefreshChildUserControls();
        }

        private void ControlViewItemOnControlCreated(object sender, EventArgs e){
            _userControl = (IUserControl)((ControlViewItem)sender).Control;
            _masterFrame.ActiveActions().ForEach(action => action.SelectionContext = _userControl);
            _userControl.CurrentObjectChanged += UserControlOnCurrentObjectChanged;
            _userControl.ProcessObject+=UserControlOnProcessObject;
            _masterFrame.View.ObjectSpace.Committed += ObjectSpaceOnCommitted;
        }

        private void ObjectSpaceOnCommitted(object sender, EventArgs e) => _userControl.Refresh();

        private void UserControlOnProcessObject(object sender, ObjectEventArgs objectEventArgs){
            var userControl = (IUserControl)sender;
            _processMasterViewSelectedObjectAction.SelectionContext = userControl;
            _processMasterViewSelectedObjectAction.DoExecute();
        }
        
        private void UserControlOnCurrentObjectChanged(object sender, EventArgs e){
            var userControl = (IUserControl)sender;
            _masterFrame.View.SetCurrentObject(userControl.CurrentObject);
            _childFrame.View.SetCurrentObject(userControl.CurrentObject);
            RefreshChildUserControls();
        }

        private void RefreshChildUserControls() 
            => _childFrame.View.ToCompositeView().GetItems<ControlViewItem>()
                .Select(item => item.Control).OfType<IUserControl>()
                .ForEach(control => control.Refresh(_childFrame.View.CurrentObject));

        public void ExtendModelInterfaces(ModelInterfaceExtenders extenders) 
            => extenders.Add<IModelDashboardView, IModelDashboardViewMasterDetail>();
    }
}