using OutlookInspired.Win.Editors;

namespace OutlookInspired.Win.Features.Customers
{
    
    public partial class CustomerStoreView : ColumnViewUserControl
    {
        public CustomerStoreView()
        {
            InitializeComponent();
            labelControl1.Text = $@"RECORDS: 0";
        }
        protected override void OnDataSourceOrFilterChanged(){
            base.OnDataSourceOrFilterChanged();
            labelControl1.Text = $@"RECORDS: {ColumnView.DataRowCount}";
        }

        
    }
}