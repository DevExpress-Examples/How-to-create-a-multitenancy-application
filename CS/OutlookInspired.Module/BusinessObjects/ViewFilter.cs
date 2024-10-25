using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq.Expressions;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl.EF;
using DevExpress.Persistent.Validation;
using OutlookInspired.Module.Attributes.Appearance;
using OutlookInspired.Module.Features;
using EditorAliases = DevExpress.ExpressApp.Editors.EditorAliases;

namespace OutlookInspired.Module.BusinessObjects{

    [XafDefaultProperty(nameof(Name))]
    [DeactivateAction(ListViewProcessCurrentObjectController.ListViewShowObjectActionId)]
    public class ViewFilter:BaseObject{
        
        [RuleRequiredField][MaxLength(100)]
        public virtual string Name{ get; set; }
        [Browsable(false)][MaxLength(255)]
        public virtual string DataTypeName { get; set; }
        
        
        [NotMapped, ImmediatePostData][Browsable(false)]
        [TypeConverter(typeof(LocalizedClassInfoTypeConverter))]
        public Type DataType {
            get => !String.IsNullOrWhiteSpace(DataTypeName) ? XafTypesInfo.Instance.FindTypeInfo(DataTypeName)?.Type : null;
            set => DataTypeName = value != null ? value.FullName : string.Empty;
        }

        [CriteriaOptions(nameof(DataType))]
        [FieldSize(FieldSizeAttribute.Unlimited)]
        [EditorAlias(DevExpress.ExpressApp.Editors.EditorAliases.CriteriaPropertyEditor)]
        public virtual string Criteria { get; set; }

        

        public void SetCriteria<T>(string criteria) where T : IViewFilter{
            DataType = typeof(T);
            Criteria = criteria;
        }

        public void SetCriteria<T>(Expression<Func<T, bool>> lambda) where T:IViewFilter 
            => SetCriteria<T>(CriteriaOperator.FromLambda(lambda).ToString());
    }
}