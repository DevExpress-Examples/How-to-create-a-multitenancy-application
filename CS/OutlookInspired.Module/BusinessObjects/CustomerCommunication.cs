using System.ComponentModel.DataAnnotations;
using DevExpress.ExpressApp.Editors;
using DevExpress.Persistent.Base;
using EditorAliases = OutlookInspired.Module.EditorAliases;

namespace OutlookInspired.Module.BusinessObjects{
    [ImageName("ProductQuickComparisons")]
    public class CustomerCommunication:OutlookInspiredBaseObject {
        public virtual Employee Employee { get; set; }
        public virtual CustomerEmployee CustomerEmployee { get; set; }
        public virtual DateTime Date { get; set; }
        [MaxLength(100)]
        public virtual string Type { get; set; }
        [EditorAlias(EditorAliases.DxHtmlPropertyEditor)]
        public virtual byte[] Purpose { get; set; }
    }
}