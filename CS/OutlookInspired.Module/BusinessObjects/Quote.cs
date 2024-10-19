using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DevExpress.Persistent.Base;
using DevExpress.XtraCharts;
using OutlookInspired.Module.Features.CloneView;
using OutlookInspired.Module.Features.ViewFilter;
using OutlookInspired.Module.Services.Internal;
using EditorAliases = OutlookInspired.Module.Services.EditorAliases;


namespace OutlookInspired.Module.BusinessObjects{
    [ImageName("BO_Quote")]
    [CloneView(CloneViewType.DetailView, MapsDetailView)]
    [CloneView(CloneViewType.DetailView, PivotDetailView)]
    public class Quote :OutlookInspiredBaseObject, IViewFilter,IMapsMarker{
        [Obsolete]
        public string City => CustomerStore.City;
        [Obsolete]
        public StateEnum State => CustomerStore.State;
        [Obsolete]
        public const string MapsDetailView = "Quote_DetailView_Maps";
        
        public const string PivotDetailView = "Quote_DetailView_Pivot";
        
        [MaxLength(20)]
        public  virtual string Number { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual CustomerStore CustomerStore { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual DateTime Date { get; set; }
        [Column(TypeName = CurrencyType)]
        public  virtual decimal SubTotal { get; set; }
        [Column(TypeName = CurrencyType)]
        public  virtual decimal ShippingAmount { get; set; }
        [Column(TypeName = CurrencyType)]
        public  virtual decimal Total { get; set; }
        [EditorAlias(EditorAliases.ProgressEditor)]
        
        public virtual  double Opportunity { get; set; }
        [DevExpress.ExpressApp.DC.Aggregated]
        public virtual ObservableCollection<QuoteItem> QuoteItems{ get; set; } = new();

        string IBaseMapsMarker.Title => Number;

        double IBaseMapsMarker.Latitude => CustomerStore.Latitude;

        double IBaseMapsMarker.Longitude => CustomerStore.Longitude;
        [NotMapped][Browsable(false)]
        public Stage Stage { get; set; }

        [NotMapped][VisibleInDetailView(false)]
        public ObservableCollection<QuoteMapItem> Opportunities{
            get{
                var quoteMapItems = ObjectSpace.Opportunities(Stage);
                return new(quoteMapItems);
            }
        }

        [NotMapped][Browsable(false)]
        public PaletteEntry[] PaletteEntries{ get; set; }
    }



}