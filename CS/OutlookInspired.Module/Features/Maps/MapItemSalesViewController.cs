using System.Linq.Expressions;
using DevExpress.ExpressApp;
using OutlookInspired.Module.BusinessObjects;

namespace OutlookInspired.Module.Features.Maps{
    [Obsolete]
    public class MapItemSalesViewController:ObjectViewController<ListView,MapItem>{
        public Period Period{ get; set; }

        protected override void OnActivated(){
            base.OnActivated();
            return;
            // Period = (Period)Frame.GetController<MapsSalesPeriodViewController>().SalesPeriodAction.SelectedItem.Data;
            var salesMapsMarker = ((ISalesMapsMarker)((PropertyCollectionSource)View.CollectionSource).MasterObject);
            salesMapsMarker.Sales.Clear();
            var mapItems = salesMapsMarker is Customer customer?Sales(item => item.Order.Customer.ID == customer.ID, Period):
                Sales(item => item.Order.Customer.ID == ((Product)salesMapsMarker).ID, Period);
            foreach (var sale in mapItems){
                salesMapsMarker.Sales.Add(sale);
            }        
        }
        
        MapItem[] Sales(Expression<Func<OrderItem,bool>> expression, Period period, string city = null) 
            => ObjectSpace.GetObjectsQuery<OrderItem>().Where(expression)
                .Where(item => (period == Period.ThisYear ? item.Order.OrderDate.Year == DateTime.Now.Year
                    : period == Period.ThisMonth ? item.Order.OrderDate.Month == DateTime.Now.Month && item.Order.OrderDate.Year == DateTime.Now.Year
                    : period != Period.FixedDate) &&(city==null||item.Order.Store.City==city))
                .Select(item => new {
                    CustomerName = item.Order.Customer.Name, ProductName = item.Product.Name, ProductCategory = item.Product.Category,
                    item.Total, item.Order.Store.Latitude, item.Order.Store.Longitude, item.Order.Store.City
                }).ToArray().Select((t, i) => new MapItem{
                    ProductCategory = t.ProductCategory,ProductName = t.ProductName,City = t.City,Latitude = t.Latitude,Longitude = t.Longitude,
                    Total = t.Total,CustomerName = t.CustomerName,ID = i
                })
                .ToArray();

    }
}