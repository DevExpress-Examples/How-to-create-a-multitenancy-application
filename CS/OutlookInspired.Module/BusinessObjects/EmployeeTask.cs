using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using OutlookInspired.Module.Attributes;
using OutlookInspired.Module.Features.CloneView;
using OutlookInspired.Module.Services;
using OutlookInspired.Module.Services.Internal;

namespace OutlookInspired.Module.BusinessObjects{
    [Appearance(nameof(DueDate),AppearanceItemType.ViewItem, "1=1",TargetItems = nameof(DueDate),FontStyle = DevExpress.Drawing.DXFontStyle.Bold)]
    [CloneView(CloneViewType.ListView, AssignedTasksChildListView)]
    public class EmployeeTask:OutlookInspiredBaseObject{
        public const string AssignedTasksChildListView="Employee_AssignedTasks_ListView_Child";
        
        public virtual ObservableCollection<Employee> AssignedEmployees{ get; set; } = new();
        [RuleRequiredField]
        [FontSizeDelta(8)][MaxLength(100)]
        public virtual string Subject { get; set; }
        
        public virtual byte[] Description { get; set; }
        
        [EditorAlias(EditorAliases.DxHtmlPropertyEditor)]
        public virtual byte[] RtfTextDescription { get; set; }
        public virtual DateTime? StartDate { get; set; }
        public virtual DateTime? DueDate { get; set; }
        public virtual EmployeeTaskStatus Status { get; set; }
        [VisibleInListView(false)][VisibleInLookupListView(false)]
        public virtual EmployeeTaskPriority Priority { get; set; }

        [VisibleInDetailView(false)][XafDisplayName(nameof(Priority))]
        public byte[] PriorityImage => Priority.ImageInfo().ImageBytes;
        
        [EditorAlias(EditorAliases.ProgressEditor)]
        public virtual int Completion { get; set; }
        public virtual bool Reminder { get; set; }
        public virtual DateTime? ReminderDateTime { get; set; }

        public virtual Employee AssignedEmployee { get; set; }
        [Browsable(false)]
        public virtual Guid? AssignedEmployeeId { get; set; }
        [Appearance("Disable Owner",AppearanceItemType.ViewItem, "1=1",Enabled = false)]
        public virtual Employee Owner { get; set; }
        [Browsable(false)]
        public virtual Guid? OwnerId { get; set; }
        [Browsable(false)]
        public virtual CustomerEmployee CustomerEmployee { get; set; }
        public virtual  EmployeeTaskFollowUp FollowUp { get; set; }
        public  virtual bool Private { get; set; }
        [MaxLength(100)]
        public  virtual string Category { get; set; }

        [Aggregated]
        public virtual ObservableCollection<TaskAttachedFile> AttachedFiles{ get; set; } = new();
        public  virtual bool AttachedCollectionsChanged { get; set; }
        public  virtual long? ParentId { get; set; }
        [MaxLength(100)]
        public  virtual string Predecessors { get; set; }
        public override string ToString() => $"{Subject} - {Description}, due {DueDate}, {Status},\r\nOwner: {Owner}";
        public bool Overdue 
            => Status != EmployeeTaskStatus.Completed && DueDate.HasValue && DateTime.Now >= DueDate.Value.Date.AddDays(1);
        [VisibleInDetailView(false)]
        public int AttachedFilesCount => AttachedFiles?.Count ?? 0;
        [VisibleInDetailView(false)]
        public string AssignedEmployeesFullList => AssignedEmployees == null ? "" : string.Join(", ", AssignedEmployees.Select(x => x.FullName));
    }

    public enum EmployeeTaskStatus {
        [ImageName(nameof(NotStarted))]
        NotStarted,
        [ImageName(nameof(Completed))]
        Completed,
        [ImageName(nameof(InProgress))]
        InProgress,
        [ImageName(nameof(NeedAssistance))]
        NeedAssistance,
        [ImageName(nameof(Deferred))]
        Deferred
    }
    public enum EmployeeTaskPriority {
        [ImageName("PriorityLow")]
        Low,
        [ImageName("PriorityNormal")]
        Normal,
        [ImageName("PriorityHigh")]
        High,
        [ImageName("PriorityUrgent")]
        Urgent
    }

    public enum EmployeeTaskFollowUp {
        [ImageName(nameof(Today))]
        Today,
        [ImageName(nameof(Tomorrow))]
        Tomorrow,
        [ImageName(nameof(ThisWeek))]
        ThisWeek,
        [ImageName(nameof(NextWeek))]
        NextWeek,
        [ImageName(nameof(NoDate))]
        NoDate,
        [ImageName(nameof(Custom))]
        Custom
    }

}