using System.Collections;
using DevExpress.Data;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.Security;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Utils;
using DevExpress.ExpressApp.Win.Editors;
using DevExpress.ExpressApp.Win.Editors.Grid.Internal;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;

namespace OutlookInspired.Win.Editors.GridListEditor{
    [ListEditor(typeof(object),false)]
    public class ColumnViewListEditor(IModelListView model) : ListEditor(model), IComplexListEditor{
        private ColumnViewListEditorControlProvider _controlProvider;
        private CollectionSourceBase _collectionSource;
        private XafApplication _application;
        private IGridViewDataRowDoubleClickAdapter _gridViewDataRowDoubleClickAdapter;
        
        public event EventHandler<ColumnViewControlCreatingArgs> ColumnViewControlCreating;
        protected override object CreateControlsCore(){
            var e = new ColumnViewControlCreatingArgs();
            OnColumnViewControlCreating(e);
            ProtectDetailViews(e.Control.ColumnView);
            _controlProvider = new(this,e.Control.ColumnView,_collectionSource);
            return e.Control;
        }

        public new IColumnViewUserControl Control => (IColumnViewUserControl)base.Control;

        protected override void AssignDataSourceToControl(object dataSource){
            if (Control == null) return;
            Control.ColumnView.SelectionChanged += ColumnViewOnSelectionChanged;
            Control.ColumnView.DoubleClick += ColumnViewOnDoubleClick;
            Control.ColumnView.DataSourceChanged+=ColumnViewOnDataSourceChanged;
            Control.ColumnView.GridControl.DataSource = dataSource;
            _gridViewDataRowDoubleClickAdapter = CreateGridViewDataRowDoubleClickAdapter(Control.ColumnView.GridControl, (GridView)Control.ColumnView);
            _gridViewDataRowDoubleClickAdapter.DataRowDoubleClick+=GridViewDataRowDoubleClickAdapterOnDataRowDoubleClick;
        }

        public override void BreakLinksToControls(){
            base.BreakLinksToControls();
            if (Control == null) return;
            Control.ColumnView.SelectionChanged -= ColumnViewOnSelectionChanged;
            Control.ColumnView.DoubleClick -= ColumnViewOnDoubleClick;
            Control.ColumnView.DataSourceChanged-=ColumnViewOnDataSourceChanged;
            _gridViewDataRowDoubleClickAdapter.DataRowDoubleClick-=GridViewDataRowDoubleClickAdapterOnDataRowDoubleClick;
        }

        void ProtectDetailViews(ColumnView columnView){
            var gridLevelNodes = columnView.GridControl.LevelTree.Nodes.ToArray()
                .Where(node => {
                    var listElementType = _application.TypesInfo.FindTypeInfo(_collectionSource.ObjectTypeInfo.Type)
                        .FindMember(node.RelationName).ListElementType;
                    return !((IRequestSecurity)_application.Security).IsGranted(new PermissionRequest(listElementType,
                        SecurityOperations.Read));
                });
            foreach (var gridLevelNode in gridLevelNodes){
                Control.ColumnView.GridControl.LevelTree.Nodes.Remove(gridLevelNode);
            }
        }

        private void GridViewDataRowDoubleClickAdapterOnDataRowDoubleClick(object sender, EventArgs e) => OnProcessSelectedItem();

        protected virtual IGridViewDataRowDoubleClickAdapter CreateGridViewDataRowDoubleClickAdapter(
            GridControl grid,
            GridView gridView)
            => new GridViewDataRowDoubleClickAdapter(grid, gridView);

        private void ColumnViewOnDoubleClick(object sender, EventArgs e){
            // if (!IsNotGroupedRow()) return;
            // OnProcessSelectedItem();
        }

        bool IsNotGroupedRow( ) 
            => Control.ColumnView is not GridView view|| !view.IsGroupRow(Control.ColumnView.FocusedRowHandle);

        private void ColumnViewOnSelectionChanged(object sender, SelectionChangedEventArgs e){
            OnFocusedObjectChanging();
            FocusedObject = GetSelectedObjects().Cast<object>().FirstOrDefault();
            OnFocusedObjectChanged();
            OnSelectionChanged();
        }

        private void ColumnViewOnDataSourceChanged(object sender, EventArgs e) => OnDataSourceChanged();
        
        
        public override void Refresh() => _collectionSource.ResetCollection();

        public override IList GetSelectedObjects(){
            if (Control == null) return new List<object>();
            var rows = Control.ColumnView.GetSelectedRows();
            var selectedObjects = rows.Any() ? rows.Select(i => Control.ColumnView.GetRow(i)).ToArray()
                : new[]{Control.ColumnView.FocusedRowHandle}
                    .Select(i => Control.ColumnView.GetRow(i));
            return selectedObjects.ToArray();
        }

        public override SelectionType SelectionType=>SelectionType.Full;
        
        public void Setup(CollectionSourceBase collectionSource, XafApplication application){
            _collectionSource = collectionSource;
            _application = application;
        }

        protected virtual void OnColumnViewControlCreating(ColumnViewControlCreatingArgs e) => ColumnViewControlCreating?.Invoke(this, e);


        public object GetObjectByIndex(int index) => _controlProvider.GetObjectByIndex(index);

        public int GetIndexByObject(object obj) => _controlProvider.GetIndexByObject(obj);


        public IList GetOrderedObjects() => _controlProvider.GetOrderedObjects();


    }

    public class ColumnViewListEditorControlProvider(ListEditor listEditor,ColumnView columnView,CollectionSourceBase collectionSource) :IControlOrderProvider{
        private readonly Dictionary<object, ObjectRecord> _objectRecords = new();
        ObjectRecord CreateObjectRecord(object objectKey, int rowHandle){
            bool isDataViewMode = DataAccessModeHelper.IsViewMode(collectionSource.DataAccessMode);
            var objectRecord =
                collectionSource.DataAccessMode == CollectionSourceDataAccessMode.InstantFeedback
                    ? new XafInstantFeedbackRecord(collectionSource.ObjectTypeInfo.Type, objectKey, rowHandle, isDataViewMode)
                    : new ObjectRecord(collectionSource.ObjectTypeInfo.Type, objectKey, rowHandle, isDataViewMode);
            objectRecord.EvaluatorContextDescriptorGetting += (_, e)
                => e.EvaluatorContextDescriptor = new ObjectRecordContextDescriptor(columnView, e.ObjectSpace,
                    collectionSource.ObjectTypeInfo.Type, collectionSource.DataAccessMode);
            if (objectKey != null && _objectRecords != null)
                _objectRecords[objectKey] = objectRecord;
            return objectRecord;
        }

        bool IsObjectRecordMode => collectionSource != null && DataAccessModeHelper.IsObjectRecordMode(collectionSource.DataAccessMode);
        bool AreEqual(object firstKey, object secondKey) 
            => firstKey.Equals(secondKey) || firstKey is List<object> first && secondKey is List<object> second && first.SequenceEqual(second);
        bool TryFindObjectRecord(object obj, out ObjectRecord resultValue){
            bool objectRecord = false;
            resultValue = null;
            foreach (object key in _objectRecords.Keys){
                if (AreEqual(key, obj)){
                    objectRecord = true;
                    resultValue = _objectRecords[key];
                    break;
                }
            }
            return objectRecord;
        }
        
        object GetObjectKey( int rowHandle){
            object objectKey;
            if (collectionSource.ObjectTypeInfo.KeyMembers.Count > 1){
                objectKey = new List<object>();
                foreach (IMemberInfo keyMember in collectionSource.ObjectTypeInfo.KeyMembers)
                    ((IList) objectKey).Add(columnView.GetRowCellValue(rowHandle, keyMember.Name));
            }
            else
                objectKey = columnView.GetRowCellValue(rowHandle, collectionSource.ObjectTypeInfo.KeyMember.Name);
            return objectKey;
        }

        public object GetObjectByIndex(int index){
            if (columnView is not{ DataController: not null }) return null;
            object objectByIndex = null;
            if (IsObjectRecordMode){
                var rowHandle = index;
                if (columnView.IsDataRow(rowHandle) && columnView.IsRowLoaded(rowHandle)){
                    object objectKey = GetObjectKey(rowHandle);
                    if (objectKey != null)
                        objectByIndex = GetObjectRecord(objectKey,  rowHandle);
                }
            }
            else
                objectByIndex = columnView.GetRow(index);
            return objectByIndex;

        }
        
        ObjectRecord GetObjectRecord(object objectKey, int rowHandle){
            if (objectKey != null ){
                ObjectRecord resultValue;
                if (BaseObjectSpace.CompositeKeyPropertyType.IsAssignableFrom(objectKey.GetType()))                {
                    if (TryFindObjectRecord(objectKey, out resultValue))
                        return resultValue;
                }
                else if (_objectRecords.TryGetValue(objectKey, out resultValue))
                    return resultValue;
            }
            return CreateObjectRecord(objectKey, rowHandle);
        }


        public IList GetOrderedObjects(){
            var orderedObjects = new List<object>();
            if (columnView == null) return orderedObjects;
            if (IsObjectRecordMode){
                for (var index = 0; index < columnView.DataRowCount; ++index){
                    var rowHandle = index;
                    if (columnView.IsDataRow(rowHandle) && columnView.IsRowLoaded(rowHandle)){
                        var objectKey = GetObjectKey( rowHandle);
                        if (objectKey != null)
                            orderedObjects.Add(GetObjectRecord(objectKey, rowHandle));
                    }
                }
            }
            else if (columnView.IsServerMode){
                var num1 = columnView.GetVisibleIndex(columnView.FocusedRowHandle) -
                           WinColumnsListEditor.PageRowCountForServerMode / 2;
                if (num1 < 0)
                    num1 = 0;
                var num2 = num1 + WinColumnsListEditor.PageRowCountForServerMode - 1;
                if (num2 > columnView.RowCount - 1)
                    num2 = columnView.RowCount - 1;
                for (var rowVisibleIndex = num1; rowVisibleIndex <= num2; ++rowVisibleIndex){
                    int visibleRowHandle = columnView.GetVisibleRowHandle(rowVisibleIndex);
                    if (columnView.IsDataRow(visibleRowHandle) && columnView.IsRowLoaded(visibleRowHandle)){
                        object row = columnView.GetRow(visibleRowHandle);
                        if (row != null)
                            orderedObjects.Add(row);
                    }
                }
            }
            else{
                for (var index = 0; index < columnView.DataRowCount; ++index){
                    var rowHandle = index;
                    if (columnView.IsRowLoaded(rowHandle)){
                        object row = columnView.GetRow(rowHandle);
                        if (row != null)
                            orderedObjects.Add(row);
                    }
                }
            }

            return orderedObjects;

        }

        public int GetIndexByObject(object obj){
            var indexByObject = -1;
            if (columnView?.DataSource != null){
                if (IsObjectRecordMode && obj is ObjectRecord objectRecord){
                    if (objectRecord.RowHandle.HasValue)
                        indexByObject = objectRecord.RowHandle.Value;
                }
                else{
                    indexByObject = columnView.GetRowHandle(listEditor.List.IndexOf(obj));
                    if (indexByObject == int.MinValue)
                        indexByObject = -1;
                }
            }
            return indexByObject;

        }
    }

    public class ColumnViewControlCreatingArgs{
        public IColumnViewUserControl Control{ get; set; }
    }
    
    public interface IColumnViewUserControl{
        ColumnView ColumnView{ get; }
    }

}