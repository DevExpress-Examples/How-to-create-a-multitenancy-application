using OutlookInspired.Win.Editors;

namespace OutlookInspired.Win.Features.Products
{
    public partial class ProductCardView : ColumnViewUserControl
    {
        public ProductCardView()
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