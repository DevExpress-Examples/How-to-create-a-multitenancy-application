using OutlookInspired.Win.Editors;

namespace OutlookInspired.Win.Features.Customers
{
    public partial class CustomerLayoutView : ColumnViewUserControl
    {
        public CustomerLayoutView()
        {
            InitializeComponent();
            labelControl1.Text = @"RECORDS: 0";
        }

        protected override void OnDataSourceOrFilterChanged(){
            base.OnDataSourceOrFilterChanged();
            labelControl1.Text = $@"RECORDS: {ColumnView.DataRowCount}";
        }

        
    }
}