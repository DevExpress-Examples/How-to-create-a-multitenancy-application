using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Utils;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Base.General;
using DevExpress.Persistent.Validation;
using OutlookInspired.Module.Attributes;
using OutlookInspired.Module.Attributes.Appearance;
using OutlookInspired.Module.Attributes.Validation;
using OutlookInspired.Module.Features;
using OutlookInspired.Module.Features.CloneView;
using OutlookInspired.Module.Features.Maps;
using EditorAliases = OutlookInspired.Module.EditorAliases;

namespace OutlookInspired.Module.BusinessObjects{
	[DefaultProperty(nameof(FullName))]
	[ImageName("BO_Person")]
	[CloneView(CloneViewType.DetailView, LayoutViewDetailView)]
	[CloneView(CloneViewType.ListView, LayoutViewListView)]
	[CloneView(CloneViewType.DetailView, ChildDetailView)]
	[CloneView(CloneViewType.DetailView, MapsDetailView)]
	[VisibleInReports(true)]
	[ForbidDelete()]
	public class Employee :OutlookInspiredBaseObject,IViewFilter,IObjectSpaceLink,IResource,IRouteMapsMarker{
		public const string MapsDetailView = "Employee_DetailView_Maps";
		public const string ChildDetailView = "Employee_DetailView_Child";
		[Obsolete]
		public const string LayoutViewDetailView = "EmployeeLayoutView_DetailView";
		public const string LayoutViewListView = "EmployeeLayoutView_ListView";
		
		[NotMapped][Browsable(false)]
		public new IObjectSpace ObjectSpace{ get; set; }
		[VisibleInListView(false), VisibleInDetailView(false), VisibleInLookupListView(false)]
		object IResource.Id => ID;

		[EditorAlias(EditorAliases.MapHomeOfficePropertyEditor)]
		public Location Location =>new(){Latitude = AddressLatitude,Longitude = AddressLongitude,Title = FullName};

		[Browsable(false)]
		public Int32 OleColor => 0;
		public virtual  EmployeeDepartment Department { get; set; }
		[RuleRequiredField][FontSizeDelta(8)][MaxLength(100)]
		public virtual string Title { get; set; }
		double IBaseMapsMarker.Latitude => AddressLatitude;
		double IBaseMapsMarker.Longitude => AddressLongitude;

		[VisibleInListView(false)]
		public virtual EmployeeStatus Status { get; set; }
		[VisibleInListView(false)]
		public virtual DateTime? HireDate { get; set; }

		[InverseProperty(nameof(EmployeeTask.AssignedEmployee))][Aggregated]
		public virtual ObservableCollection<EmployeeTask> AssignedTasks{ get; set; } = new();
		
		[InverseProperty(nameof(EmployeeTask.AssignedEmployees))]
		public virtual ObservableCollection<EmployeeTask> AssignedEmployeeTasks{ get; set; } = new();
		[InverseProperty(nameof(EmployeeTask.Owner))]
		public virtual ObservableCollection<EmployeeTask> OwnedTasks{ get; set; } = new(); 
		[InverseProperty(nameof(Evaluation.Employee))][Aggregated]
		public virtual ObservableCollection<Evaluation> Evaluations { get; set; }=new();
		[NotMapped]
		public virtual ObservableCollection<Evaluation> Events => Evaluations;
		[VisibleInListView(false)][MaxLength(1000)]
		public virtual string PersonalProfile { get; set; }
		[VisibleInListView(false)]
		public virtual Probation ProbationReason { get; set; }
		[RuleRequiredField][VisibleInListView(false)][MaxLength(100)]
		public virtual string FirstName { get; set; }
		[RuleRequiredField][VisibleInListView(false)][MaxLength(100)]
		public virtual string LastName { get; set; }
		
		[FontSizeDelta(16)][MaxLength(100)]
		public virtual string FullName { get; set; }

		public override void OnSaving(){
			base.OnSaving();
			FullName ??= $"{FirstName} {LastName}";
		}

		[VisibleInListView(false)][VisibleInLookupListView(false)]
		public virtual PersonPrefix Prefix { get; set; }

		[VisibleInDetailView(false)]
		[XafDisplayName(nameof(Prefix))]
		public virtual byte[] PrefixImage => ImageLoader.Instance.GetEnumValueImageInfo(Prefix).ImageBytes;
		
		[Attributes.Validation.Phone][VisibleInListView(false)][MaxLength(100)]
		public virtual string HomePhone { get; set; }
		[RuleRequiredField, Attributes.Validation.Phone][VisibleInListView(false)][MaxLength(100)]
		public virtual string MobilePhone { get; set; }
		[RuleRequiredField, Attributes.Validation.EmailAddress]
		[EditorAlias(EditorAliases.HyperLinkPropertyEditor)][MaxLength(255)]
		public virtual string Email { get; set; }

		[VisibleInDetailView(false)]
		[NotMapped]
		public virtual ObservableCollection<RoutePoint> RoutePoints{ get; set; } = new();
		[VisibleInListView(false)][MaxLength(100)]
		public virtual string Skype { get; set; }
		[VisibleInListView(false)]
		public virtual DateTime? BirthDate { get; set; }
		[VisibleInListView(false)]
		public virtual Picture Picture { get; set; }
		[NotMapped][VisibleInListView(false)][VisibleInDetailView(false)][VisibleInLookupListView(false)][XafDisplayName("A")]
		public string AAddress{ get; set; }
		[NotMapped][VisibleInListView(false)][VisibleInDetailView(false)][VisibleInLookupListView(false)][XafDisplayName("B")]
		public string BAddress{ get; set; }
		public virtual StateEnum State { get; set; }
		[VisibleInListView(false)]
		public virtual double AddressLatitude { get; set; }
		[VisibleInListView(false)]
		public virtual double AddressLongitude { get; set; }
		[RuleRequiredField][FontSizeDelta(2)][MaxLength(255)]
		public virtual string Address { get; set; }
		[RuleRequiredField][MaxLength(100)]
		public virtual string City { get; set; }
        [ZipCode][MaxLength(20)]
		public virtual string ZipCode { get; set; }
		public virtual ObservableCollection<Evaluation> EvaluationsCreatedBy{ get; set; } = new();
		public virtual ObservableCollection<Order> Orders{ get; set; } = new();
		[InverseProperty(nameof(Product.Engineer))]
		public virtual ObservableCollection<Product> Products{ get; set; } = new();
        [InverseProperty(nameof(Product.Support))]
        public virtual ObservableCollection<Product> SupportedProducts{ get; set; } = new();
		public virtual ObservableCollection<Quote> Quotes{ get; set; } = new();
		public virtual ObservableCollection<CustomerCommunication> CustomerCommunications{ get; set; } = new();
		[Browsable(false)]
		public virtual Guid? ProbationReasonId{ get; set; }


		[NotMapped]
		public virtual string Caption{
			get => FullName;
			set => FullName=value;
		}

		[NotMapped][VisibleInListView(false)][VisibleInDetailView(false)][VisibleInLookupListView(false)]
		[FontSizeDelta(2)]
		public virtual string RouteResult{ get; set; }

		[RuleRequiredField(ResultType = ValidationResultType.Warning,CustomMessageTemplate = "Ask your Admin to create a new user and assign him to this Employee. (Click this msg gear to continue)")]
		public virtual ApplicationUser User{ get; set; }

		[Browsable(false)]
		public virtual Guid? UserId{ get; set; }
	}

	public enum EmployeeDepartment {
		[ImageName(nameof(Sales))]
		Sales = 1, 
		[ImageName(nameof(Support))]
		Support,
		[ImageName("ProductQuickShippments")]
		Shipping,
		[ImageName("FunctionsEngineering")]
		Engineering,
		[ImageName("GroupByResource")]
		HumanResources,
		[ImageName("ManageRelations")]
		Management,
		[ImageName("Actions_Info")]
		IT
	}

	public enum EmployeeStatus {
		[ImageName("Salaried")]
		Salaried,
		[ImageName("Commission")]
		Commission,
		[ImageName("Contract")]
		Contract,
		[ImageName("Terminated")]
		Terminated,
		[ImageName("OnLeave")]
		OnLeave
	}
	[DomainComponent]
	public class Location:IMapsMarker{
		public double Latitude{ get; init; }
		public double Longitude{ get; init; }

		[MaxLength(100)]
		public string Title{ get; set; }
		
	}

	

}