using System.ComponentModel;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Data;
using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;

namespace OutlookInspired.Module.BusinessObjects{
    [DomainComponent][ImageName("BO_Quote")]
    public class Opportunity:IObjectSpaceLink{

        [DevExpress.ExpressApp.Data.Key][Browsable(false)]
        public int ID{ get; set; }
        
        
        [Browsable(false)]
        public Stage Stage{ get; set; }

        IObjectSpace IObjectSpaceLink.ObjectSpace{ get; set; }
        public decimal Value{ get; set; }
    }

    [DomainComponent]
    public class QuoteAnalysis{
        [Key]
        public int ID{ get; set; }
        public StateEnum State { get; set; }
        public string City { get; set; }
        public decimal Total { get; set; }
        public double Opportunity { get; set; }
    }
}