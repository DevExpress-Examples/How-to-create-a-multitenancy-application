using System.IO;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.Win.Editors;
using DevExpress.XtraPdfViewer;
using EditorAliases = OutlookInspired.Module.Services.EditorAliases;

namespace OutlookInspired.Win.Editors{
    [PropertyEditor(typeof(byte[]), EditorAliases.PdfViewerEditor, false)]  
    public class PdfViewerEditor(Type objectType, IModelMemberViewItem info) : WinPropertyEditor(objectType, info){
        public new PdfViewer Control => (PdfViewer)base.Control;
        protected override object CreateControlCore() 
            => new PdfViewer{
                Dock = DockStyle.Fill, DetachStreamAfterLoadComplete = true, ZoomMode = PdfZoomMode.PageLevel,
                NavigationPaneVisibility = PdfNavigationPaneVisibility.Hidden,
                NavigationPaneInitialVisibility = PdfNavigationPaneVisibility.Hidden
            };
        
        protected override void ReadValueCore(){
            if (PropertyValue is not byte[]{ Length: > 0 } bytes) return;
            using var memoryStream = new MemoryStream(bytes);
            Control?.LoadDocument(memoryStream);
            
        }
    }
}