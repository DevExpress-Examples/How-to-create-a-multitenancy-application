using DevExpress.ExpressApp.Data;
using DevExpress.ExpressApp.DC;
using OutlookInspired.Module.Features;

namespace OutlookInspired.Module.BusinessObjects{
    [DomainComponent]
    public class QuoteAnalysis:IViewFilter{
        [Key]
        public int ID{ get; set; }
        public StateEnum State { get; set; }
        public string City { get; set; }
        public DateTime Date { get; set; }
        public decimal Total { get; set; }
        public double Opportunity { get; set; }
    }
}