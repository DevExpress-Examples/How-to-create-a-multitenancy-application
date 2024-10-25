using System.Reflection;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.Win.Editors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraPivotGrid;

namespace OutlookInspired.Win.Editors.ProgressEditor{
    public class PropertyEditorController:ViewController<ListView>{
        Dictionary<PivotGridField, RepositoryItem> AddRepositoryItems(PivotGridControl pivotGridControl,ListView view){
            var items = view.Model.Columns.Where(column => column.Index >= 0)
                .Select(column => {
                    var pivotGridField = pivotGridControl.Fields[column.ModelMember.Name];
                    return pivotGridField != null &&
                           typeof(IInplaceEditSupport).IsAssignableFrom(column.PropertyEditorType)
                        ? (pivotGridField,
                            repositoryItem: ((IInplaceEditSupport)NewPropertyEditor(column)).CreateRepositoryItem())
                        : default;
                })
                .Where(item => item != default).ToArray();
            foreach (var item in items){
                pivotGridControl.RepositoryItems.Add(item.repositoryItem);
            }
            return items.ToDictionary(t => t.pivotGridField, t => t.repositoryItem);
        }

        PropertyEditor NewPropertyEditor(IModelMemberViewItem modelMemberViewItem) 
            => HasPublicParameterlessConstructor(modelMemberViewItem.PropertyEditorType)
                ? (PropertyEditor)CreateInstance(modelMemberViewItem.PropertyEditorType)
                : (PropertyEditor)Activator.CreateInstance(modelMemberViewItem.PropertyEditorType,
                    args:[GetParent<IModelObjectView>(modelMemberViewItem).ModelClass.TypeInfo.Type, modelMemberViewItem]);
        
        object CreateInstance(Type type){
            if (type.IsValueType)
                return Activator.CreateInstance(type);

            if (HasPublicParameterlessConstructor(type))
                return Activator.CreateInstance(type);
            throw new InvalidOperationException($"Type {type.FullName} does not have a parameterless constructor.");
        }


        bool HasPublicParameterlessConstructor(Type type) 
            => type.GetConstructors(BindingFlags.Public | BindingFlags.Instance)
                .Any(ctor => ctor.GetParameters().Length == 0);

        TNode GetParent<TNode>(IModelNode modelNode) where TNode : class 
            => modelNode.Parent as TNode ?? GetParent<TNode>(modelNode.Parent);

        protected override void OnViewControlsCreated(){
            base.OnViewControlsCreated();
            if (View.Editor is not DevExpress.ExpressApp.PivotGrid.Win.PivotGridListEditor pivotGridListEditor) return;
            var pivotGridControl = pivotGridListEditor.PivotGridControl;
            var repositoryItems = AddRepositoryItems(pivotGridControl,View);
            pivotGridControl.CustomCellEdit += (_, e) => {
                if (!repositoryItems.TryGetValue(e.DataField, out var item)) return;
                e.RepositoryItem = item;
            };
            pivotGridControl.CustomCellValue += (_, e) => {
                if (!repositoryItems.TryGetValue(e.DataField, out var item) || item is not IValueCalculator valueCalculator) return;
                e.Value = valueCalculator.Calculate(e.Value);
            };
            pivotGridControl.CustomDrawCell += (_, e) => {
                if (!repositoryItems.TryGetValue(e.DataField, out var item)) return;
                e.Appearance = item.Appearance;
            };
        }
    }
}