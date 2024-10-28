using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Base;
using OutlookInspired.Win.Editors.GridListEditor;


namespace OutlookInspired.Win.Editors{

    
    public partial class ColumnViewUserControl : UserControl, IColumnViewUserControl{
        public ColumnView ColumnView=>(ColumnView)Controls.OfType<GridControl>().First().MainView;
        public ColumnViewUserControl() => Load += OnLoad;

        private void OnLoad(object sender, EventArgs e){
            ColumnView.ColumnFilterChanged += (_, _) => OnDataSourceOrFilterChanged();
            ColumnView.DataSourceChanged += (_, _) => OnDataSourceOrFilterChanged();
            ColumnView.DataError+=(_, args) => throw new AggregateException(args.DataException.Message,args.DataException);
            OnDataSourceOrFilterChanged();
        }
        
        protected virtual void OnDataSourceOrFilterChanged(){
            
        }
        
    }
}
