using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Base.General;
using DevExpress.Persistent.Validation;
using OutlookInspired.Module.Attributes;
using OutlookInspired.Module.Features.CloneView;
using OutlookInspired.Module.Services;
using OutlookInspired.Module.Services.Internal;

namespace OutlookInspired.Module.BusinessObjects{
    [Appearance(nameof(StartOn),AppearanceItemType.ViewItem, "1=1",TargetItems = nameof(StartOn),FontStyle = DevExpress.Drawing.DXFontStyle.Bold,Context = "Employee_Evaluations_ListView")]
    [Appearance(nameof(Manager),AppearanceItemType.ViewItem, "1=1",TargetItems = nameof(Manager),FontStyle = DevExpress.Drawing.DXFontStyle.Bold,Context = EmployeeEvaluationsChildListView)]
    [Appearance(nameof(StartOn)+"_"+EmployeeEvaluationsChildListView,AppearanceItemType.ViewItem, "1=1",TargetItems = nameof(StartOn),FontColor = "Blue",Context = EmployeeEvaluationsChildListView)]
    [Appearance(nameof(Rating),AppearanceItemType.ViewItem, nameof(Rating)+"='"+nameof(EvaluationRating.Good)+"'",TargetItems = "*",FontColor = "Green",Context = "Employee_Evaluations_ListView")]
    [CloneView(CloneViewType.ListView, EmployeeEvaluationsChildListView)]
    [DefaultClassOptions][ImageName("EvaluationYes")][VisibleInReports(false)]
    public class Evaluation :OutlookInspiredBaseObject,IEvent{
	    private string _resourceId;
	    private bool _isUpdateResourcesDelayed;

	    public const string EmployeeEvaluationsChildListView="Employee_Evaluations_ListView_Child";

	    public override void OnSaving(){
		    if (ObjectSpace == null || !_isUpdateResourcesDelayed)
			    return;
		    _isUpdateResourcesDelayed = false;
		    this.Update(Resources);
	    }

		public override void OnCreated() {
			base.OnCreated();
			StartOn = DateTime.Now;
			EndOn = StartOn.Value.AddHours(1);
			Color = Color.White;
		}
		
		[NotMapped]
		[Browsable(false)]
		public object ResourceIdBlazor{
			get => Employee?.ID;
			set => Employee = ObjectSpace.GetObjectByKey<Employee>(value);
		}

		[NotMapped]
		public string Description{
			get => DescriptionBytes.ToDocumentText();
			set => DescriptionBytes=value.Bytes().ToOpenXmlBytes();
		}

		[EditorAlias(EditorAliases.DxHtmlPropertyEditor)]
		[Column(nameof(Description))]
		[XafDisplayName(nameof(Description))]
		public virtual byte[] DescriptionBytes{ get; set; }
		public virtual DateTime? EndOn { get; set; }
		[ImmediatePostData][Browsable(false)]
		public virtual Boolean AllDay { get; set; }
		[Browsable(false)][MaxLength(255)]
		public virtual String Location { get; set; }
		[Browsable(false)]
		public virtual Int32 Label { get; set; }
		[Browsable(false)]
		public virtual Int32 Status { get; set; }
		[Browsable(false)]
		public virtual Int32 Type { get; set; }

		[NotMapped, Browsable(false)]
		public virtual String ResourceId{
			get => _resourceId ??= Resources.ToIds();
			set{
				if (_resourceId == value)
					return;
				_resourceId = value;
				if (ObjectSpace != null)
					this.Update(Resources);
				else
					_isUpdateResourcesDelayed = true;
			}
		}
		
		[Browsable(false)]
		public Object AppointmentId => ID;
		
		DateTime IEvent.StartOn {
			get => StartOn ?? DateTime.MinValue;
			set => StartOn = value;
		}
		DateTime IEvent.EndOn {
			get => EndOn ?? DateTime.MinValue;
			set => EndOn = value;
		}
		
		[RuleRequiredField]
        public virtual Employee Manager{ get; set; }
        [Browsable(false)]
        public virtual Guid? ManagerId{ get; set; }
        [RuleRequiredField]
        public virtual DateTime? StartOn{ get; set; }
        [RuleRequiredField(DefaultContexts.Save)]
        public virtual Employee Employee{ get; set; }
        [FontSizeDelta(8)][MaxLength(100)]
        public virtual string Subject{ get; set; }

        [NotMapped]
        public IList<Employee> Resources => Employee.YieldItem().ToList();
        public virtual EvaluationRating Rating{ get; set; }

        [VisibleInListView(false)]
        public virtual Raise Raise{ get; set; }
        
        [StringLength(300)]
        [NonCloneable]
        [DisplayName("Recurrence")]
        [FieldSize(-1)]
        public virtual string RecurrenceInfoXml { get; set; }
        [VisibleInListView(false)]
        public virtual Bonus Bonus{ get; set; }
        
        [Browsable(false)]
        public virtual Int32 ColorInt { get; protected set; }

        [NotMapped][Browsable(false)]
        public Color Color {
            get => Color.FromArgb(ColorInt);
            set => ColorInt = value.ToArgb();
        }
    }

    public enum Raise{
        [XafDisplayName("RAISE")]
        [ImageName("Action_Deny")]
        No,
        [XafDisplayName("RAISE")]
        [ImageName("BO_Validation")]
        Yes
    }
    public enum Bonus{
        [XafDisplayName("BONUS")]
        [ImageName("Action_Deny")]
        No,
        [XafDisplayName("BONUS")]
        [ImageName("BO_Validation")]
        Yes
    }

    public enum EvaluationRating {
        Unset, Good, Average, Poor
    }

}