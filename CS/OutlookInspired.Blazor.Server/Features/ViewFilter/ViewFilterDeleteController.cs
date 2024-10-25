using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Utils;
using DevExpress.Persistent.Base;

namespace OutlookInspired.Blazor.Server.Features.ViewFilter{
    public class ViewFilterDeleteController:ObjectViewController<ListView,Module.BusinessObjects.ViewFilter>{
        private readonly SimpleAction _simpleAction;
        private SimpleAction _deleteAction;

        public ViewFilterDeleteController(){
            _simpleAction = new SimpleAction(this, "DeleteViewFilter", PredefinedCategory.ObjectsCreation){
                Caption = "Delete", SelectionDependencyType = SelectionDependencyType.RequireMultipleObjects,
                ImageName = "Delete"
            };
            _simpleAction.Executed += (_, _) => _deleteAction.DoExecute();
        }

        protected override void OnDeactivated(){
            base.OnDeactivated();
            _deleteAction.Active.ResultValueChanged-=ActiveOnResultValueChanged;
            _deleteAction.Enabled.ResultValueChanged-=EnabledOnResultValueChanged;
        }

        protected override void OnActivated(){
            base.OnActivated();
            _deleteAction = Frame.GetController<DeleteObjectsViewController>().DeleteAction;
            _deleteAction.Active.ResultValueChanged+=ActiveOnResultValueChanged;
            _deleteAction.Enabled.ResultValueChanged+=EnabledOnResultValueChanged;
        }

        private void EnabledOnResultValueChanged(object sender, BoolValueChangedEventArgs e) => _simpleAction.Enabled[nameof(ViewFilterDeleteController)] = e.NewValue;

        private void ActiveOnResultValueChanged(object sender, BoolValueChangedEventArgs e) => _simpleAction.Active[nameof(ViewFilterDeleteController)] = e.NewValue;
    }
}