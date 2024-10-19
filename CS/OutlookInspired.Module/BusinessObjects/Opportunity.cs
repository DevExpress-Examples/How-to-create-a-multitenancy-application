using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
using OutlookInspired.Module.Services.Internal;

namespace OutlookInspired.Module.BusinessObjects{
    [DomainComponent][ImageName("BO_Quote")]
    public class Opportunity:IObjectSpaceLink{
        [Key][Browsable(false)]
        public Guid ID{ get; set; }=Guid.NewGuid();
        
        [NotMapped]
        public ObservableCollection<QuoteMapItem> Items 
            => new(((IObjectSpaceLink)this).ObjectSpace.Opportunities(Stage));

        [Browsable(false)]
        public Stage Stage{ get; set; }

        IObjectSpace IObjectSpaceLink.ObjectSpace{ get; set; }
    }
}