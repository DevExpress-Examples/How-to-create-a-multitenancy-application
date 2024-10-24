using OutlookInspired.Win.Editors;

namespace OutlookInspired.Win.Features.Orders
{
    public partial class OrderDetailView : ColumnViewUserControl
    {
        public OrderDetailView()
        {
            InitializeComponent();
            labelControl1.Text = @"RECORDS: 0";
        }

        protected override void OnDataSourceOrFilterChanged()
        {
            base.OnDataSourceOrFilterChanged();
            labelControl1.Text = $@"RECORDS: {ColumnView.DataRowCount}";
        }


        
    }
}
