using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;

namespace OutlookInspired.Module.BusinessObjects {
	[DefaultProperty(nameof(FullName))]
	[ImageName("BO_Employee")]
	public class CustomerEmployee :OutlookInspiredBaseObject{
		[RuleRequiredField][MaxLength(100)]
		public virtual string FirstName { get; set; }
		[RuleRequiredField][MaxLength(100)]
		public virtual string LastName { get; set; }
		[PersistentAlias("Concat(" +nameof(FirstName) + ", ' ', " +nameof(LastName)+ ")")]
		public string FullName => (string)EvaluateAlias();

		public virtual PersonPrefix Prefix { get; set; }
		[RuleRequiredField, Attributes.Validation.Phone][MaxLength(100)]
		public virtual string MobilePhone { get; set; }
		[RuleRequiredField, Attributes.Validation.EmailAddress][MaxLength(255)]
		public virtual string Email { get; set; }
		public virtual Picture Picture { get; set; }
		
		public virtual Customer Customer { get; set; }
		[Browsable(false)]
		public virtual Guid CustomerId { get; set; }
		
		public virtual CustomerStore CustomerStore { get; set; }
		[MaxLength(100)]
		public virtual string Position { get; set; }
		public virtual bool IsPurchaseAuthority { get; set; }
		[Aggregated]
		public virtual ObservableCollection<CustomerCommunication> CustomerCommunications{ get; set; } = new();
		

	}
	public enum PersonPrefix {
		[ImageName("Doctor")]
		Dr,
		[ImageName("Mr")]
		Mr,
		[ImageName("Ms")]
		Ms,
		[ImageName("Miss")]
		Miss,
		[ImageName("Mrs")]
		Mrs
	}

}
