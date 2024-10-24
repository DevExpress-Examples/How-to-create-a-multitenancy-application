using OutlookInspired.Module.BusinessObjects;
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

        // protected override void OnDataSourceOfFilterChanged(){
        //     base.OnDataSourceOfFilterChanged();
        //     labelControl1.Text = $@"RECORDS: {ColumnView.DataRowCount}";
        // }

        public override Type ObjectType => typeof(Customer);
    }
}