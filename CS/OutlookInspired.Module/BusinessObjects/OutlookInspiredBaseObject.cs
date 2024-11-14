

using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using DevExpress.ExpressApp;
using DevExpress.Persistent.BaseImpl.EF;
using OutlookInspired.Module.Attributes.Appearance;

namespace OutlookInspired.Module.BusinessObjects{
    [DeactivateAction("ShowInDocument",Context = "Any;Employee_ListView;")]
    [DeactivateAction("Save", "SaveAndClose", "SaveAndNew", "ShowAllContexts", "NextObject", "PreviousObject",
        Context = Customer.GridViewDetailView + ";" + Customer.LayoutViewDetailView )]
    [DeactivateAction("OpenObject",
        Context = Customer.MapsDetailView + ";" + Employee.MapsDetailView + ";" + Product.MapsDetailView + ";" +
                                              Order.MapsDetailView + ";")]
    public abstract class OutlookInspiredBaseObject:BaseObject{
        protected const string CurrencyType = "decimal(18, 2)";
        [Browsable(false)]
        public virtual long IdInt64{ get; set; }

        [NotMapped][Browsable(false)]
        public new IObjectSpace ObjectSpace{
            get => base.ObjectSpace;
            set => base.ObjectSpace=value;
        }

    }
}