using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Model;
using DevExpress.XtraRichEdit;
using EditorAliases = OutlookInspired.Module.Services.EditorAliases;
using RichTextPropertyEditor = DevExpress.ExpressApp.Office.Win.RichTextPropertyEditor;

namespace OutlookInspired.Win.Editors{
    [PropertyEditor(typeof(byte[]), EditorAliases.PrintLayoutRichTextEditor, false)]  
    public class PrintLayoutRichTextEditor(Type objectType, IModelMemberViewItem info)
        : RichTextPropertyEditor(objectType, info){
        protected override object CreateControlCore(){
            var controlCore = base.CreateControlCore();
            RichEditControl.ActiveViewType=RichEditViewType.PrintLayout;
            return controlCore;
        }
    }
}