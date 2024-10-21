using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Data;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.EFCore;
using DevExpress.Persistent.Base;
using OutlookInspired.Module.Features.CloneView;
using OutlookInspired.Module.Services.Internal;

namespace OutlookInspired.Module.BusinessObjects{
    [DomainComponent]
    
    public class QuoteMapItem:IMapsMarker,IMapItem,IObjectSpaceLink{
        
        [Key]
        public int ID{ get; set; }
        public Stage Stage { get; set; }
        public DateTime Date { get; set; }
        public string City{ get; set; }
        string IBaseMapsMarker.Title => City;
        public double Latitude{ get; set; }
        public double Longitude { get; set; }

        public decimal Total{
            get => Value;
            init => Value=value;
        }

        public string Color{ get; set; }


        public string Name => Enum.GetName(typeof (Stage), Stage);
        public int Index => (int) Stage;
        public decimal Value { get; set; }
        IObjectSpace IObjectSpaceLink.ObjectSpace{ get; set; }

        public static QuoteMapItem[] QueryAll(IObjectSpace objectSpace){
            var quoteMapItems = objectSpace.GetObjectsQuery<Quote>()
                .Select(quote => new{
                    quote.Total,
                    quote.Date,
                    quote.CustomerStore.City,
                    quote.CustomerStore.Latitude,
                    quote.CustomerStore.Longitude,
                })
                .ToArray()
                .Select((t, i) => {
                    return new QuoteMapItem(){
                        Latitude = t.Latitude,
                        City = t.City,
                        ID = i,
                        Date = t.Date,
                        Longitude = t.Longitude,
                        Total = t.Total,
                        Value = t.Total

                    };

                }).ToArray();
            return quoteMapItems;
        }
    }
    
    public enum Stage{
        [ImageName(nameof(High))]
        High,
        [ImageName(nameof(Medium))]
        Medium,
        [ImageName(nameof(Low))]
        Low,
        [ImageName("Unlike")]
        Unlikely,
        [ImageName(nameof(Summary))]
        Summary
    }

}