using System.Collections;
using DevExpress.Data;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Model;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using OutlookInspired.Win.Features.Employees;

namespace OutlookInspired.Win.Editors.Layout{
    [ListEditor(typeof(object),false)]
    public class LayoutViewListEditor(IModelListView model) : ListEditor(model), IComplexListEditor{
        private CollectionSourceBase _collectionSource;

        protected override object CreateControlsCore() => new EmployeesLayoutView();

        public new EmployeesLayoutView Control => (EmployeesLayoutView)base.Control;

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
                    .Select(i => Control.ColumnView.GetRow(i)).ToArray();
            return selectedObjects;

        }

        public override SelectionType SelectionType=>SelectionType.Full;
        
        public void Setup(CollectionSourceBase collectionSource, XafApplication application) => _collectionSource = collectionSource;
    }
}