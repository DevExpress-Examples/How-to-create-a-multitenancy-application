using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Data;
using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;

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

    }
    public static class StageExtensions {
        public static (double min, double max) Range(this Stage stage){
            switch (stage){
                case Stage.High:
                    return (0.6, 1.0);
                case Stage.Medium:
                    return (0.3, 0.6);
                case Stage.Low:
                    return (0.12, 0.3);
                case Stage.Summary:
                    return (0.0, 1.0);
                case Stage.Unlikely:
                    return (0.0, 0.12);
            }

            throw new InvalidOperationException(stage.ToString());
        }

        public static string Color(this Stage stage){
            switch (stage){
                case Stage.High:
                    return "#D11C1C"; // Red
                case Stage.Medium:
                    return "#1177D7"; // Blue
                case Stage.Low:
                    return "#FFB115"; // Yellow
                case Stage.Unlikely:
                    return "#727272"; // Gray
            }

            throw new InvalidOperationException(stage.ToString());
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