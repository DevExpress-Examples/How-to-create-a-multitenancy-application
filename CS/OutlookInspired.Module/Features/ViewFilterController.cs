using System.ComponentModel;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Blazor.Templates;
using DevExpress.ExpressApp.Layout;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Templates;
using DevExpress.Persistent.Base;
using OutlookInspired.Module.BusinessObjects;

namespace OutlookInspired.Module.Features{
    public interface IViewFilter{
        
    }

    public class ViewFilterController:ObjectViewController<ObjectView,IViewFilter>{
        public const string FilterViewActionId = "FilterView";
        public ViewFilterController(){
            FilterAction = new SingleChoiceAction(this,FilterViewActionId,PredefinedCategory.View){
                ImageName = "Action_Filter",PaintStyle = ActionItemPaintStyle.Image
            };
            FilterAction.Executed += (_, e) => {
                if (ManagerFilters(e)) return;
                FilterView((ListView)View);
            };
        }

        public SingleChoiceAction FilterAction{ get; }

        public void FilterView(ListView listView){
            var criteria = FilterAction.SelectedItem.Data is ViewFilter viewFilter ? viewFilter.Criteria : null;
            listView.CollectionSource.Criteria[nameof(ViewFilterController)] = ObjectSpace.ParseCriteria(criteria);
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
                FilterAction.DoExecute(FilterAction.SelectedItem);
            };
            controller.CancelAction.Executed+= (_, _) => {
                AddFilterItems();
                FilterAction.DoExecute(FilterAction.SelectedItem);
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
            listView.CollectionSource.Criteria[nameof(ViewFilterController)] =
                CriteriaOperator.FromLambda<ViewFilter>(filter => filter.DataTypeName == View.ObjectTypeInfo.Type.FullName);
            showViewParameters.TargetWindow=TargetWindow.NewModalWindow;
            showViewParameters.CreatedView=listView;
        }
        
        protected override void OnDeactivated(){
            base.OnDeactivated();
            Application.ObjectSpaceCreated-=ApplicationOnObjectSpaceCreated;
        }
        
        protected override void OnActivated(){
            base.OnActivated();
            Application.ObjectSpaceCreated += ApplicationOnObjectSpaceCreated;
        }

        protected override void OnViewControlsCreated(){
            base.OnViewControlsCreated();
            FilterAction.Active["PopTemplate"] = Frame.Template is not PopupWindowTemplate;
            AddFilterItems();
            if(View is DetailView detailView) {
                detailView.CustomizeViewItemControl<ControlViewItem>(this, _ => {
                    if(View.ObjectTypeInfo.Type == typeof(Quote)) {
                        FilterAction.SelectedItem = FilterAction.Items.First(item => $"{item.Data}" == "This Month");
                        FilterAction.DoExecute(FilterAction.SelectedItem);
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
            if(View == null) return;

            FilterAction.Items.Clear();
            FilterAction.Items.Add(new ChoiceActionItem("Manage...", "Manage"));

            var allItemsCount = View is ListView listView ? listView.CollectionSource.GetCount() : ObjectSpace.GetObjectsCount(View.ObjectTypeInfo.Type, null);
            var criteria = View is ListView listView1 ? listView1.CollectionSource.GetTotalCriteria() : null;
            var allItem = new ChoiceActionItem($"All ({allItemsCount})", "All");
            FilterAction.Items.Add(allItem);

            var viewFilters = ObjectSpace.GetObjectsQuery<ViewFilter>().Where(filter => filter.DataTypeName == View.ObjectTypeInfo.Type.FullName).ToList();
            var choiceActionItems = viewFilters.Select(viewFilter => {
                var objectsCount = ObjectSpace.GetObjectsCount(viewFilter.DataType, Combine(criteria,viewFilter.Criteria));
                return new ChoiceActionItem($"{viewFilter.Name} ({objectsCount})", viewFilter);
            }).ToList();
            FilterAction.Items.AddRange(choiceActionItems);

            FilterAction.SelectedItem = allItem;
        }
        
        CriteriaOperator Combine( CriteriaOperator criteriaOperator,string criteria,GroupOperatorType type=GroupOperatorType.And){
            var @operator = CriteriaOperator.Parse(criteria);
        
            return !Object.ReferenceEquals(criteriaOperator, null) ? new GroupOperator(type, @operator, criteriaOperator) : @operator;
        }

        
    }
}