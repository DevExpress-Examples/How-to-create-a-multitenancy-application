using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.Win.Editors;
using DevExpress.Utils;
using DevExpress.XtraEditors.Repository;
using EditorAliases = OutlookInspired.Module.EditorAliases;

namespace OutlookInspired.Win.Editors.ProgressEditor {
    [PropertyEditor(typeof(int), EditorAliases.ProgressEditor, false)]
    [PropertyEditor(typeof(double), EditorAliases.ProgressEditor, false)]
    public class ProgressPropertyEditor(Type objectType, IModelMemberViewItem model)
        : DXPropertyEditor(objectType, model){
        protected override object CreateControlCore() => new ProgressBarControl();

        class ProgressBarControl:DevExpress.XtraEditors.ProgressBarControl{
            protected override object ConvertCheckValue(object val) => val is double doubleValue ? (int)(doubleValue * 100) : base.ConvertCheckValue(val);
        }
        class RepositoryItemProgressBar:DevExpress.XtraEditors.Repository.RepositoryItemProgressBar,IValueCalculator{
            
            public object Calculate(object value) => Convert.ToDecimal(value) * 100;

            protected override int ConvertValue(object val) 
                => val is double doubleValue ? (int)(doubleValue * 100) : base.ConvertValue(val);
        }
        
        protected override RepositoryItem CreateRepositoryItem()
            => new RepositoryItemProgressBar(){
                PercentView = true, ShowTitle = true, DisplayFormat ={ FormatType = FormatType.Numeric, FormatString = "{0}%" },
                Appearance ={
                    Options = { UseTextOptions = true},TextOptions = { HAlignment = HorzAlignment.Center}
                }
            };
    }
    public interface IValueCalculator{
        object Calculate(object eValue);
    }

}
