using System.Linq.Expressions;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.EFCore;
using OutlookInspired.Module.BusinessObjects;

namespace OutlookInspired.Module.Services.Internal{
    internal static class MapExtensions{
        static readonly double[] UsaBounds =[-124.566244, 49.384358, -66.934570, 24.396308];

        public static string[] Palette(this MapItem[] mapItems,Type objectType) 
            => mapItems.Select(item => item.PropertyValue(objectType)).Distinct().Count().DistinctColors().ToArray();

        public static double[] Bounds<TMapItem>(this TMapItem[] mapItems) where TMapItem:IMapItem
            => !mapItems.Any() ? UsaBounds : 
                (mapItems.Min(item => item.Longitude) - (mapItems.Max(item => item.Longitude) - mapItems.Min(item => item.Longitude)) * 0.1).YieldItem()
                .Concat(mapItems.Max(item => item.Latitude) + (mapItems.Max(item => item.Latitude) - mapItems.Min(item => item.Latitude)) * 0.1)
                .Concat(mapItems.Max(item => item.Longitude) + (mapItems.Max(item => item.Longitude) - mapItems.Min(item => item.Longitude)) * 0.1)
                .Concat(mapItems.Min(item => item.Latitude) - (mapItems.Max(item => item.Latitude) - mapItems.Min(item => item.Latitude)) * 0.1).ToArray();

        [Obsolete]
        public static string MapItemProperty(this Type salesMarkerType)
            => salesMarkerType switch{
                _ when typeof(Customer).IsAssignableFrom(salesMarkerType) => nameof(MapItem.ProductName),
                _ when typeof(Product).IsAssignableFrom(salesMarkerType) => nameof(MapItem.CustomerName),
                _ => throw new InvalidOperationException($"Invalid type provided. {salesMarkerType}")
            };

        [Obsolete]
        public static MapItem[] Sales(this IObjectSpace objectSpace,Expression<Func<OrderItem,bool>> expression, Period period, string city = null) 
            => objectSpace.GetObjectsQuery<OrderItem>().Where(expression).Where(period,city)
                .Select(item => new {
                    CustomerName = item.Order.Customer.Name, ProductName = item.Product.Name, ProductCategory = item.Product.Category,
                    item.Total, item.Order.Store.Latitude, item.Order.Store.Longitude, item.Order.Store.City
                }).ToArray().Select(t => new MapItem{
                    ProductCategory = t.ProductCategory,ProductName = t.ProductName,City = t.City,Latitude = t.Latitude,Longitude = t.Longitude,
                    Total = t.Total,CustomerName = t.CustomerName
                })
                .Do((item, i) => item.ID=i).ToArray();

        [Obsolete]
        public static IQueryable<CustomerStore> Stores(this IObjectSpace objectSpace,Period period,DateTime dateTime=default) 
            => objectSpace.GetObjectsQuery<Order>()
                .Where(period, dateTime: dateTime).GroupBy(order => order.Store)
                .Select(orders => orders.Key);

        static IQueryable<Order> Where(this IQueryable<Order> source, Period period, string city=null,DateTime dateTime = default) 
            => source.Where(order => period == Period.ThisYear ? order.OrderDate.Year == DateTime.Now.Year : period == Period.ThisMonth
                ? order.OrderDate.Month == DateTime.Now.Month && order.OrderDate.Year == DateTime.Now.Year
                : period != Period.FixedDate || order.OrderDate.Month == dateTime.Month &&
                order.OrderDate.Year == dateTime.Year && order.OrderDate.Day == dateTime.Day)
                .Where(order => city==null||order.Store.City==city);
        
        static IQueryable<OrderItem> Where(this IQueryable<OrderItem> source, Period period, string city=null) 
            => source.Where(item => (period == Period.ThisYear ? item.Order.OrderDate.Year == DateTime.Now.Year
                : period == Period.ThisMonth ? item.Order.OrderDate.Month == DateTime.Now.Month && item.Order.OrderDate.Year == DateTime.Now.Year
                : period != Period.FixedDate) &&(city==null||item.Order.Store.City==city));

        public static string OpportunityCallout(this IObjectSpace objectSpace,QuoteMapItem item) 
            => $"TOTAL<br><color=206,113,0><b><size=+4>{objectSpace.Opportunity(item.Stage, item.City)}</color></size></b><br>{item.City}";
        
        public static decimal Opportunity(this IObjectSpace objectSpace,Stage stage,string city)    
            => objectSpace.Quotes(stage).Where(q => q.CustomerStore.City == city).TotalSum(q => q.Total);

        [Obsolete]
        public static CustomerStore[] Stores(this IObjectSpace objectSpace, Stage stage) 
            => objectSpace.Quotes(stage).Select(quote => quote.CustomerStore).Distinct().ToArray();

        public static QuoteMapItem[] Opportunities(this IObjectSpace objectSpace, Stage stage,string criteria=null)
            => objectSpace.Quotes(stage,criteria).ToArray().Select(quote => {
                var mapItem = objectSpace.CreateObject<QuoteMapItem>();
                mapItem.Stage = stage;
                mapItem.Value = quote.Total;
                mapItem.Date = quote.Date;
                mapItem.City = quote.CustomerStore.City;
                mapItem.Latitude = quote.CustomerStore.Latitude;
                mapItem.Longitude = quote.CustomerStore.Longitude;
                return mapItem;
            }).ToArray().Do((item, i) => item.ID=i).ToArray();
         
        public static IEnumerable<QuoteMapItem> Opportunities(this IObjectSpace objectSpace,string criteria=null)
            => Enum.GetValues<Stage>().Where(stage1 => stage1!=Stage.Summary).Select(stage1 => new QuoteMapItem{ Stage = stage1, 
                    Value = ((IQueryable<Quote>)objectSpace.YieldAll().OfType<EFCoreObjectSpace>().First().Query(typeof(Quote), criteria))
                        .Where(stage1).TotalSum(q => q.Total) }).Do((item, i) => item.ID=i);

        private static IQueryable<Quote> Quotes(this IObjectSpace objectSpace, Stage stage,string criteria=null) 
            => ((IQueryable<Quote>)((EFCoreObjectSpace)objectSpace).Query(typeof(Quote), criteria)).Where(stage);

        public static decimal TotalSum<T>(this IEnumerable<T> query, Expression<Func<T, decimal>> selector){
            var source = query.AsQueryable().Select(selector);
            return !source.Any() ? 0M : source.AsEnumerable().Sum();
        }
        
        static IQueryable<Quote> Where(this IQueryable<Quote> quotes, Stage stage){
            var (min, max) = new Dictionary<Stage, (double, double)>{
                [Stage.High] = (0.6, 1.0), [Stage.Medium] = (0.3, 0.6),
                [Stage.Low] = (0.12, 0.3), [Stage.Summary] = (0.0, 1.0),
            }.GetValueOrDefault(stage, (0.0, 0.12));
            return quotes.Where(quote => quote.Opportunity > min && quote.Opportunity < max);
        }

    }
}