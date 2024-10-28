using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Templates;
using DevExpress.Persistent.Base;
using OutlookInspired.Module.BusinessObjects;

namespace OutlookInspired.Module.Features{
    public interface IViewFilter{
        
    }

    public class ViewFilterController:ObjectViewController<ObjectView,IViewFilter>{
        public event EventHandler<CustomizeStartItemArgs> CustomizeStartItem; 
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
            ObjectSpace.Committed-=ObjectSpaceOnCommitted;
        }
        
        protected override void OnActivated(){
            base.OnActivated();
            ObjectSpace.Committed+=ObjectSpaceOnCommitted;
        }
        
        protected override void OnViewControlsCreated(){
            base.OnViewControlsCreated();
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
                var criteriaOperator = new GroupOperator(GroupOperatorType.And, criteria, CriteriaOperator.Parse(viewFilter.Criteria));
                var objectsCount = ObjectSpace.GetObjectsCount(viewFilter.DataType, criteriaOperator);
                return new ChoiceActionItem($"{viewFilter.Name} ({objectsCount})", viewFilter);
            }).ToList();
            FilterAction.Items.AddRange(choiceActionItems);

            var e = new CustomizeStartItemArgs(allItem);
            OnCustomizeStartItem(e);
            FilterAction.SelectedItem = e.ChoiceActionItem;
        }
        


        protected virtual void OnCustomizeStartItem(CustomizeStartItemArgs e) => CustomizeStartItem?.Invoke(this, e);
    }

    public class CustomizeStartItemArgs(ChoiceActionItem choiceActionItem) :EventArgs{
        public ChoiceActionItem ChoiceActionItem{ get; set; } = choiceActionItem;
    }
}