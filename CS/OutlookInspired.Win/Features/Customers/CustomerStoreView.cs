using System.Collections.ObjectModel;
using OutlookInspired.Module.BusinessObjects;
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
        // protected override void OnDataSourceOfFilterChanged(){
        //     base.OnDataSourceOfFilterChanged();
        //     labelControl1.Text = $@"RECORDS: {ColumnView.DataRowCount}";
        // }

        // public override void Refresh(object currentObject)
        // {
        //     DataSource = ((Customer)currentObject)?.CustomerStores??new ObservableCollection<CustomerStore>();
        //     base.Refresh(currentObject);
        // }

        public override Type ObjectType => typeof(CustomerStore);
    }
}