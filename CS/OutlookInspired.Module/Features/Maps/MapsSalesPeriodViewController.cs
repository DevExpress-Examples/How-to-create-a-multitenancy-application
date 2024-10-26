using System.Collections.ObjectModel;
using System.Linq.Expressions;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Templates;
using DevExpress.Persistent.Base;
using OutlookInspired.Module.BusinessObjects;
using ImageLoader = DevExpress.ExpressApp.Utils.ImageLoader;

namespace OutlookInspired.Module.Features.Maps{
    public class MapsSalesPeriodViewController:ObjectViewController<DetailView,ISalesMapsMarker>{
        const string HasMapItemVectorMapListEditor = "hasMapItemVectorMapListEditor";
        public MapsSalesPeriodViewController(){
            SalesPeriodAction = new SingleChoiceAction(this,"SalesPeriod",PredefinedCategory.PopupActions){
                    ItemType = SingleChoiceActionItemType.ItemIsOperation,
                    ImageMode = ImageMode.UseItemImage,
                    DefaultItemMode = DefaultItemMode.LastExecutedItem,
                    PaintStyle = ActionItemPaintStyle.Image
                };
            SalesPeriodAction.Executed+=SalesPeriodActionOnExecuted;
            SalesPeriodAction.Items.AddRange(Enum.GetValues<Period>().Where(period => period!=Period.FixedDate)
                .Select(period => new ChoiceActionItem(period.ToString(), period){
                    ImageName = ImageLoader.Instance.GetEnumValueImageName(period)
                }).ToArray());
            SalesPeriodAction.SelectedItem = SalesPeriodAction.Items.First();
            
            SalesPeriodAction.Active[HasMapItemVectorMapListEditor] = false;
        }

        public SingleChoiceAction SalesPeriodAction{ get; }

        protected override void OnActivated(){
            base.OnActivated();
            View.CustomizeViewItemControl<ListPropertyEditor>(this, _ 
                => {
                SalesPeriodAction.Active[HasMapItemVectorMapListEditor] = true;
                SalesPeriodAction.DoExecute(SalesPeriodAction.SelectedItem);
            },nameof(ISalesMapsMarker.CitySales));
        }

        MapItem[] Sales() {
            Expression<Func<OrderItem, bool>> expression = View.CurrentObject switch{
                Customer customer => item => item.Order.Customer.ID == customer.ID,
                Product product => item => item.Product.ID == product.ID,
                _ => throw new NotImplementedException(View.ObjectTypeInfo.FullName)
            };
            var period = (Period)SalesPeriodAction.SelectedItem.Data;
            return ObjectSpace.GetObjectsQuery<OrderItem>().Where(expression)
                .Where(item => (period == Period.ThisYear ? item.Order.OrderDate.Year == DateTime.Now.Year
                    : period == Period.ThisMonth ? item.Order.OrderDate.Month == DateTime.Now.Month &&
                                                   item.Order.OrderDate.Year == DateTime.Now.Year
                    : period != Period.FixedDate) )
                .Select(item => new{
                    CustomerName = item.Order.Customer.Name, ProductName = item.Product.Name,
                    ProductCategory = item.Product.Category,
                    item.Total, item.Order.Store.Latitude, item.Order.Store.Longitude, item.Order.Store.City
                }).ToArray().Select((t, i) => new MapItem{
                    ProductCategory = t.ProductCategory, ProductName = t.ProductName, City = t.City,
                    Latitude = t.Latitude, Longitude = t.Longitude,
                    Total = t.Total, CustomerName = t.CustomerName, ID = i
                })
                .ToArray();
        }

        private void SalesPeriodActionOnExecuted(object sender, ActionBaseEventArgs e){
            var period = (Period)SalesPeriodAction.SelectedItem.Data;
            var mapItems = Sales();
            var currentObject = (ISalesMapsMarker)View.CurrentObject;
            currentObject.Sales = new ObservableCollection<MapItem>(mapItems);
            currentObject.CitySales = new ObservableCollection<MapItem>(mapItems);
            if (!mapItems.Any()){
                var message = "No sales found. ";
                if (period != Period.Lifetime){
                    message += "Switched to Lifetime sales period.";
                    SalesPeriodAction.SelectedItem = SalesPeriodAction.Items.First(item => (Period)item.Data == Period.Lifetime);
                    SalesPeriodAction.DoExecute(SalesPeriodAction.SelectedItem);
                }
                Application.ShowViewStrategy.ShowMessage(message,InformationType.Info);
            }
            View.Refresh();
        }
    }
}