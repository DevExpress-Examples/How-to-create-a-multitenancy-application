using OutlookInspired.Module.BusinessObjects;
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

        // protected override void OnDataSourceOfFilterChanged()
        // {
        //     base.OnDataSourceOfFilterChanged();
        //     labelControl1.Text = $@"RECORDS: {ColumnView.DataRowCount}";
        // }

        public override Type ObjectType => typeof(Product);
    }
}