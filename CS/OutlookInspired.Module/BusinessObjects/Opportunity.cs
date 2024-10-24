using System.ComponentModel;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Data;
using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;

namespace OutlookInspired.Module.BusinessObjects{
    [DomainComponent][ImageName("BO_Quote")]
    public class Opportunity:IObjectSpaceLink{

        [Key][Browsable(false)]
        public int ID{ get; set; }
        public Stage Stage{ get; set; }

        IObjectSpace IObjectSpaceLink.ObjectSpace{ get; set; }
        public decimal Value{ get; set; }
        public DateTime Date{ get; set; }
    }
}