using System.Collections;
using DevExpress.Data;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.Security;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;

namespace OutlookInspired.Win.Editors.GridListEditor{
    [ListEditor(typeof(object),false)]
    public class ColumnViewListEditor(IModelListView model) : ListEditor(model), IComplexListEditor{
        private CollectionSourceBase _collectionSource;
        private XafApplication _application;
        public event EventHandler<ColumnViewControlCreatingArgs> ColumnViewControlCreating;
        protected override object CreateControlsCore(){
            var e = new ColumnViewControlCreatingArgs();
            OnColumnViewControlCreating(e);
            ProtectDetailViews(e.Control.ColumnView);
            return e.Control;
        }

        public new IColumnViewUserControl Control => (IColumnViewUserControl)base.Control;

        protected override void AssignDataSourceToControl(object dataSource){
            if (Control == null) return;
            Control.ColumnView.SelectionChanged += ColumnViewOnSelectionChanged;
            Control.ColumnView.DoubleClick += ColumnViewOnDoubleClick;
            Control.ColumnView.FocusedRowChanged+=ColumnViewOnFocusedRowChanged;
            Control.ColumnView.FocusedRowObjectChanged+=ColumnViewOnFocusedRowObjectChanged;
            Control.ColumnView.Click+=ColumnViewOnClick;
            Control.ColumnView.DataSourceChanged+=ColumnViewOnDataSourceChanged;
            Control.ColumnView.GridControl.DataSource = dataSource;
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

        private void ColumnViewOnDoubleClick(object sender, EventArgs e){
            if (!IsNotGroupedRow()) return;
            OnProcessSelectedItem();
        }
        
        bool IsNotGroupedRow( ) 
            => Control.ColumnView is not GridView view|| !view.IsGroupRow(Control.ColumnView.FocusedRowHandle);

        private void ColumnViewOnSelectionChanged(object sender, SelectionChangedEventArgs e){
            OnFocusedObjectChanging();
            FocusedObject = GetSelectedObjects().Cast<object>().FirstOrDefault();
            OnFocusedObjectChanged();
            OnSelectionChanged();
        }

        public override void BreakLinksToControls(){
            base.BreakLinksToControls();
            if (Control == null) return;
            Control.ColumnView.SelectionChanged -= ColumnViewOnSelectionChanged;
            Control.ColumnView.DoubleClick -= ColumnViewOnDoubleClick;
            Control.ColumnView.FocusedRowChanged-=ColumnViewOnFocusedRowChanged;
            Control.ColumnView.FocusedRowObjectChanged-=ColumnViewOnFocusedRowObjectChanged;
            Control.ColumnView.Click-=ColumnViewOnClick;
            Control.ColumnView.DataSourceChanged-=ColumnViewOnDataSourceChanged;
        }

        private void ColumnViewOnDataSourceChanged(object sender, EventArgs e) => OnDataSourceChanged();

        private void ColumnViewOnClick(object sender, EventArgs e){
            
        }

        private void ColumnViewOnFocusedRowObjectChanged(object sender, FocusedRowObjectChangedEventArgs e){
            
        }

        private void ColumnViewOnFocusedRowChanged(object sender, FocusedRowChangedEventArgs e){
            
        }

        public override void Refresh() => _collectionSource.ResetCollection();

        public override IList GetSelectedObjects(){
            if (Control == null) return new List<object>();
            var rows = Control.ColumnView.GetSelectedRows();
            var selectedObjects = rows.Any() ? rows.Select(i => Control.ColumnView.GetRow(i)).ToArray()
                : new[]{Control.ColumnView.FocusedRowHandle}
                    .Select(i => Control.ColumnView.GetRow(i));
            return selectedObjects
                // .Select(o => {
                //     if (o is not XafDataViewRecord record) return o;
                //     var typeInfo = _collectionSource.ObjectTypeInfo;
                //     return _collectionSource.ObjectSpace.GetObjectByKey(typeInfo.Type, record[typeInfo.KeyMember.Name]);
                //
                // })
                .ToArray();

        }

        public override SelectionType SelectionType=>SelectionType.Full;
        
        public void Setup(CollectionSourceBase collectionSource, XafApplication application){
            _collectionSource = collectionSource;
            _application = application;
        }

        protected virtual void OnColumnViewControlCreating(ColumnViewControlCreatingArgs e) => ColumnViewControlCreating?.Invoke(this, e);
    }

    public class ColumnViewControlCreatingArgs{
        public IColumnViewUserControl Control{ get; set; }
    }
    
    public interface IColumnViewUserControl{
        ColumnView ColumnView{ get; }
    }

}