using System.ComponentModel.DataAnnotations;
using DevExpress.ExpressApp.DC;
using OutlookInspired.Module.Attributes.Appearance;

namespace OutlookInspired.Module.BusinessObjects{
    public interface IColoredItem{
        string Color{ get; set; }
    }

    public interface IMapItem : IColoredItem{
        string City{ get; set; }
        double Latitude{ get; set; }
        double Longitude{ get; set; }
        decimal Total{ get; init; }
    }

    [DomainComponent]
    [DeactivateAction("New")]
    public class MapItem : IMapItem{
        [DevExpress.ExpressApp.Data.Key]
        public int ID{ get; set; }
        [MaxLength(100)]
        public string City{ get; set; }
        public double Latitude{ get; set; }
        public double Longitude { get; set; }
        public decimal Total { get; init; }
        [MaxLength(100)]
        public string CustomerName{ get; init; }
        [MaxLength(100)]
        public string ProductName{ get; init; }

        public ProductCategory ProductCategory{ get; set; }
        [MaxLength(20)]
        public string Color{ get; set; }
        public static double[] PredefinedBound(string name){
            switch (name){
                case "usa":
                    return[-124.566244, 49.384358, -66.934570, 24.396308];
                
            }

            throw new InvalidOperationException(name);
        }

        public static double[] GetBounds<TMapItem>( TMapItem[] mapItems,double[] defaultBounds=null) where TMapItem:IMapItem
            => !mapItems.Any() ? (defaultBounds??PredefinedBound("usa")) : 
                new[]{(mapItems.Min(item => item.Longitude) - (mapItems.Max(item => item.Longitude) - mapItems.Min(item => item.Longitude)) * 0.1)}
                    .Concat(new[]{mapItems.Max(item => item.Latitude) + (mapItems.Max(item => item.Latitude) - mapItems.Min(item => item.Latitude)) * 0.1}.AsEnumerable())
                    .Concat(new[]{mapItems.Max(item => item.Longitude) + (mapItems.Max(item => item.Longitude) - mapItems.Min(item => item.Longitude)) * 0.1}.AsEnumerable())
                    .Concat(new[]{mapItems.Min(item => item.Latitude) - (mapItems.Max(item => item.Latitude) - mapItems.Min(item => item.Latitude)) * 0.1}.AsEnumerable()).ToArray();

        

        
    }
}