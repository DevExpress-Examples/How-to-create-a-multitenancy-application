using System.ComponentModel;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Layout;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Templates;
using DevExpress.Persistent.Base;
using OutlookInspired.Module.BusinessObjects;
using OutlookInspired.Module.Services.Internal;

namespace OutlookInspired.Module.Features{
    public interface IViewFilter{
        
    }

    public class ViewFilterController:ObjectViewController<ObjectView,IViewFilter>{
        public const string FilterViewActionId = "FilterView";
        public ViewFilterController(){
            FilterAction = new SingleChoiceAction(this,FilterViewActionId,PredefinedCategory.Filters){
                ImageName = "Action_Filter",PaintStyle = ActionItemPaintStyle.Image
            };
            FilterAction.Executed += (_, e) => {
                if (ManagerFilters(e)) return;
                FilterView();
            };
        }
        
        public SingleChoiceAction FilterAction{ get; }
        
        private void FilterView(){
            var criteria = FilterAction.SelectedItem.Data is ViewFilter viewFilter ? viewFilter.Criteria : null;
            var userControl = View.UserControl();
            if (userControl != null){
                userControl.SetCriteria(criteria);
            }
            else{
                View.ToListView().CollectionSource.Criteria[nameof(ViewFilterController)] = CriteriaOperator.Parse(criteria);
            }
        }
        
        private bool ManagerFilters(ActionBaseEventArgs e){
            if (FilterAction.SelectedItem.Data as string != "Manage") return false;
            CreateViewFilterListView(e.ShowViewParameters);
            AddDialogController(e.ShowViewParameters);
            return true;
        }

        private void AddDialogController(ShowViewParameters showViewParameters){
            var controller = Application.CreateController<DialogController>();
            controller.Activated+=ListViewDialogControllerOnActivated;
            showViewParameters.Controllers.Add(controller);
            controller.AcceptAction.Executed += (_, _) => {
                AddFilterItems();
                FilterAction.DoExecute();
            };
            controller.CancelAction.Executed+= (_, _) => {
                AddFilterItems();
                FilterAction.DoExecute();
            }; 
        }

        private void ListViewDialogControllerOnActivated(object sender, EventArgs e){
            var dialogController = ((DialogController)sender);
            dialogController.Activated-=ListViewDialogControllerOnActivated;
            dialogController.Frame.GetController<NewObjectViewController>().ObjectCreated+=OnObjectCreated;
        }

        private void OnObjectCreated(object sender, ObjectCreatedEventArgs e){
            ((NewObjectViewController)sender).ObjectCreated-=OnObjectCreated;
            ((ViewFilter)e.CreatedObject).DataType = View.ObjectTypeInfo.Type;
        }
        
        private void CreateViewFilterListView(ShowViewParameters showViewParameters){
            var listView = Application.CreateListView(typeof(ViewFilter), true);
            listView.Editor.NewObjectCreated += (_, args) => ((ViewFilter)((ObjectManipulatingEventArgs)args).Object).DataType = View.ObjectTypeInfo.Type;
            listView.CollectionSource.SetCriteria<ViewFilter>(filter => filter.DataTypeName == View.ObjectTypeInfo.Type.FullName);
            showViewParameters.TargetWindow=TargetWindow.NewModalWindow;
            showViewParameters.CreatedView=listView;
        }
        
        protected override void OnDeactivated(){
            base.OnDeactivated();
            Application.ObjectSpaceCreated-=ApplicationOnObjectSpaceCreated;
        }
        
        protected override void OnActivated(){
            base.OnActivated();
            var active = Frame is NestedFrame && Frame.View.IsRoot && (Frame.View is ListView || Frame.View is DetailView && View.ObjectTypeInfo.Type == typeof(Quote));
            FilterAction.Active[nameof(ViewFilterController)] = active;
            if(!active)
                return;
            
            Application.ObjectSpaceCreated += ApplicationOnObjectSpaceCreated;
        }

        protected override void OnViewControlsCreated(){
            base.OnViewControlsCreated();
            AddFilterItems();
            if(View is DetailView detailView) {
                detailView.CustomizeViewItemControl<ControlViewItem>(this, _ => {
                    if(View.ObjectTypeInfo.Type == typeof(Quote)) {
                        FilterAction.DoExecute(item => $"{item.Data}" == "This Month");
                    }
                });
            }
        }

        private void ApplicationOnObjectSpaceCreated(object sender, ObjectSpaceCreatedEventArgs e){
            e.ObjectSpace.Committing+=ObjectSpaceOnCommitting;
            e.ObjectSpace.Disposed+=ObjectSpaceOnDisposed;
        }

        private void ObjectSpaceOnDisposed(object sender, EventArgs e) 
            => ((IObjectSpace)sender).Committing-=ObjectSpaceOnCommitting;

        private void ObjectSpaceOnCommitting(object sender, CancelEventArgs e){
            var objectSpace = ((IObjectSpace)sender);
            if (!objectSpace.ModifiedObjects.Cast<object>().OfType<IViewFilter>().Any()) return;
            objectSpace.Committed+=OnCommitted;
        }

        private void OnCommitted(object sender, EventArgs e){
            ((IObjectSpace)sender).Committed-=ObjectSpaceOnCommitted;
            AddFilterItems();
        }

        private void ObjectSpaceOnCommitted(object sender, EventArgs e) => AddFilterItems();
        
        void AddFilterItems(){
            if(View == null)
                return;

            FilterAction.Items.Clear();

            FilterAction.Items.Add(new ChoiceActionItem("Manage...", "Manage"));

            var count = View is ListView listView ? listView.CollectionSource.GetCount() : ObjectSpace.GetObjectsCount(View.ObjectTypeInfo.Type, null);
            var criteria = View is ListView listView1 ? listView1.CollectionSource.GetTotalCriteria() : null;

            var allItem = new ChoiceActionItem($"All ({count})", "All");
            FilterAction.Items.Add(allItem);

            var viewFilters = ObjectSpace.GetObjectsQuery<ViewFilter>().Where(filter => filter.DataTypeName == View.ObjectTypeInfo.Type.FullName).ToList();
            var choiceActionItems = viewFilters.Select(viewFilter => new ChoiceActionItem($"{viewFilter.Name} ({Count(viewFilter.DataType,criteria)})", viewFilter)).ToList();
            FilterAction.Items.AddRange(choiceActionItems);

            FilterAction.SelectedItem = allItem;
        }
        
        int Count(Type dataType,CriteriaOperator criteria=null){
            using var objectSpace = Application.CreateObjectSpace(dataType);
            return objectSpace.GetObjectsCount(dataType, criteria.Combine(criteria?.ToString()));
        }
    }
}