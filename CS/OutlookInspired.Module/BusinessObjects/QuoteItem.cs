using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using DevExpress.Persistent.Base;


namespace OutlookInspired.Module.BusinessObjects{
    [ImageName("Shopping_Sales")]
    // [CloneView(CloneViewType.ListView, MapsListView)]
    public class QuoteItem :OutlookInspiredBaseObject{
        // public const string MapsListView = "QuoteItem_ListView_Maps";
        public virtual Quote Quote { get; set; }
        [Browsable(false)]
        public virtual Guid? QuoteID { get; set; }
        public virtual Product Product { get; set; }
        [Browsable(false)]
        public virtual Guid? ProductId { get; set; }
        public  virtual int ProductUnits { get; set; }
        [Column(TypeName = CurrencyType)]
        public  virtual decimal ProductPrice { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public  virtual decimal Discount { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public  virtual decimal Total { get; set; }

        
        
        
    }
}