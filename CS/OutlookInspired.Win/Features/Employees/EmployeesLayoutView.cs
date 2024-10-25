using OutlookInspired.Win.Editors;

namespace OutlookInspired.Win.Features.Employees
{
    public partial class EmployeesLayoutView : ColumnViewUserControl{
        public EmployeesLayoutView(){
            InitializeComponent();
            labelControl1.Text = @"RECORDS: 0";
        }

        protected override void OnDataSourceOrFilterChanged(){
            base.OnDataSourceOrFilterChanged();
            labelControl1.Text = $@"RECORDS: {ColumnView.DataRowCount}";
        }

        
    }
}