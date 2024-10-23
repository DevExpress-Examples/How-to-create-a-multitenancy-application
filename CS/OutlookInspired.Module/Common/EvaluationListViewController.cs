using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.EFCore;
using DevExpress.ExpressApp.Scheduler;
using DevExpress.ExpressApp;
using OutlookInspired.Module.BusinessObjects;

namespace OutlookInspired.Module.Controllers {
    public class EvaluationListViewController : ObjectViewController<ListView, Evaluation> {
        SchedulerListEditorBase editor;
        public EvaluationListViewController() {
            TargetViewNesting = Nesting.Nested;
        }
        protected override void OnActivated() {
            base.OnActivated();
            editor = View.Editor as SchedulerListEditorBase;
            if (editor != null) {
                editor.ResourceDataSourceCreated += ResourceDataSourceCreated;
            }
        }
        protected override void OnDeactivated() {
            base.OnDeactivated();
            if (editor != null) {
                editor.ResourceDataSourceCreated -= ResourceDataSourceCreated;
                editor = null;
            }
        }
        private void ResourceDataSourceCreated(object sender, ResourceDataSourceCreatedEventArgs e) {
            var collectionSource = View.CollectionSource as PropertyCollectionSource;
            if (collectionSource?.MasterObjectType == typeof(Employee)) {
                var collection = e.DataSource as EFCoreCollection;
                if (collection != null) {
                    var employee = (Employee)collectionSource?.MasterObject;
                    collection.Criteria = CriteriaOperator.Parse("ID = ?", employee?.ID);
                }
            }
        }
    }
}
