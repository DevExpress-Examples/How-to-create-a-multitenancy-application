using System.ComponentModel;
using System.Text;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl.EF;
using DevExpress.Persistent.Validation;


namespace OutlookInspired.Module.BusinessObjects{
    [ImageName("AttachFile")]
    public class TaskAttachedFile :OutlookInspiredBaseObject{
        
        public virtual EmployeeTask EmployeeTask { get; set; }
        [ExpandObjectMembers(ExpandObjectMembers.Never), RuleRequiredField()]
        public virtual FileData File { get; set; }
        [Browsable(false)]
        public virtual Guid? EmployeeTaskId{ get; set; }
        
        [EditorAlias(EditorAliases.DxHtmlPropertyEditor)]
        public string Preview 
            => File != null && Path.GetExtension(File.FileName) == ".rtf" ? GetString(File.Content) : null;
        
        string GetString( byte[] bytes, Encoding encoding = null) 
            => bytes == null ? null : (encoding ?? Encoding.UTF8).GetString(bytes);
    }
}