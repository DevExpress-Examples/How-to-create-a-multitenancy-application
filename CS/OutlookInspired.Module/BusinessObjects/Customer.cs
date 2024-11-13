using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using OutlookInspired.Module.Attributes;
using OutlookInspired.Module.Attributes.Validation;
using OutlookInspired.Module.Features;
using OutlookInspired.Module.Features.CloneView;
using OutlookInspired.Module.Features.Maps;



namespace OutlookInspired.Module.BusinessObjects {
	[ImageName("BO_Customer")]
	[CloneView(CloneViewType.DetailView, ChildDetailView)]
	[CloneView(CloneViewType.DetailView, LayoutViewDetailView)]
	[CloneView(CloneViewType.ListView, LayoutViewListView)]
	[CloneView(CloneViewType.DetailView, GridViewDetailView)]
	[CloneView(CloneViewType.DetailView, MapsDetailView)]
	[XafDefaultProperty(nameof(Name))]
	public class Customer:OutlookInspiredBaseObject,IViewFilter,ISalesMapsMarker{
		
		public const string ChildDetailView = "Customer_DetailView_Child";
		
		public const string GridViewDetailView = "CustomerGridView_DetailView";
		public const string LayoutViewDetailView = "CustomerLayoutView_DetailView";
		public const string LayoutViewListView = "CustomerLayoutView_ListView";
		public const string MapsDetailView = "Customer_DetailView_Maps";
		[FontSizeDelta(4)][MaxLength(255)]
		public  virtual string HomeOfficeLine { get; set; }
		[XafDisplayName("City")][MaxLength(100)]
		public  virtual string HomeOfficeCity { get; set; }
		[ZipCode]
		[XafDisplayName("ZipCode")][MaxLength(20)]
		public  virtual string HomeOfficeZipCode { get; set; }
		[XafDisplayName("Address")]
		[HideInUI(HideInUI.ListView)]
		[MaxLength(255)]
		public  virtual string BillingAddressLine { get; set; }
		[HideInUI(HideInUI.ListView)]
		[MaxLength(100)]
		public virtual string BillingAddressCity { get; set; }
		[ZipCode][HideInUI(HideInUI.ListView)]
		[MaxLength(20)]
		public  virtual string BillingAddressZipCode { get; set; }
		[RuleRequiredField][XafDisplayName(nameof(Customer))]
		[FontSizeDelta(8)][MaxLength(100)]
		public virtual string Name { get; set; }
		[XafDisplayName("State")]
		public virtual StateEnum HomeOfficeState { get; set; }
		[HideInUI(HideInUI.ListView)]
		public virtual double HomeOfficeLatitude { get; set; }
		[HideInUI(HideInUI.ListView)]
		public virtual double HomeOfficeLongitude { get; set; }
		[HideInUI(HideInUI.ListView)]
		public virtual StateEnum BillingAddressState { get; set; }
		[HideInUI(HideInUI.ListView)]
		public virtual double BillingAddressLatitude { get; set; }
		[HideInUI(HideInUI.ListView)]
		public virtual double BillingAddressLongitude { get; set; }

		[NotMapped]
		[HideInUI(HideInUI.DetailView)]
		public virtual ObservableCollection<MapItem> CitySales{ get; set; } = new();

		[Aggregated]
		public virtual ObservableCollection<CustomerEmployee> Employees{ get; set; } = new(); 
		[Attributes.Validation.Phone][MaxLength(20)]
		public virtual string Phone { get; set; }
		[Attributes.Validation.Phone][HideInUI(HideInUI.ListView)]
		[MaxLength(20)]
		public virtual string Fax { get; set; }
		[Attributes.Validation.Url]
		[EditorAlias(EditorAliases.HyperLinkPropertyEditor)][HideInUI(HideInUI.ListView)]
		[MaxLength(255)]
		public virtual string Website { get; set; }
		[Column(TypeName = CurrencyType)][HideInUI(HideInUI.ListView)]
		public virtual decimal AnnualRevenue { get; set; }
		[HideInUI(HideInUI.ListView)]
		public virtual int TotalStores { get; set; }
		[HideInUI(HideInUI.ListView)]
		public virtual int TotalEmployees { get; set; }
		[HideInUI(HideInUI.ListView)]
		public virtual CustomerStatus Status { get; set; }
		[InverseProperty(nameof(Quote.Customer))] [Aggregated]
		public virtual ObservableCollection<Quote> Quotes{ get; set; } = new();

		[InverseProperty(nameof(CustomerStore.Customer))]
		[Aggregated]
		public virtual ObservableCollection<CustomerStore> CustomerStores{ get; set; } = new();
		[HideInUI(HideInUI.ListView)]
		[EditorAlias(EditorAliases.DxHtmlPropertyEditor)]
		public virtual byte[] Profile { get; set; }
		[ImageEditor(ListViewImageEditorMode = ImageEditorMode.PictureEdit,
			DetailViewImageEditorMode = ImageEditorMode.PictureEdit,ImageSizeMode = ImageSizeMode.Zoom)]
		[HideInUI(HideInUI.ListView)]
		public virtual byte[] Logo { get; set; }
		string IBaseMapsMarker.Title => Name;
		double IBaseMapsMarker.Latitude => BillingAddressLatitude;
		double IBaseMapsMarker.Longitude => BillingAddressLongitude;

		[InverseProperty(nameof(Order.Customer))]
		[Aggregated]
		public virtual ObservableCollection<Order> Orders{ get; set; } = new();
		[HideInUI(HideInUI.DetailView)][NotMapped]
		public virtual List<Order> RecentOrders{
			get{
				var dateTime = DateOnly.FromDateTime(DateTime.Now.AddMonths(-2));
				return ObjectSpace.GetObjectsQuery<Order>()
					.Where(order => order.Customer.ID == ID && order.OrderDate > dateTime).ToList();
			}
		}

		[HideInUI(HideInUI.DetailView)]
		[NotMapped]
		public ObservableCollection<MapItem> Sales{ get; set; } = new();
		
		IEnumerable<Order> ISalesMapsMarker.Orders => Orders;
		
	}
	public enum CustomerStatus {
		Active, Suspended
	}

}
