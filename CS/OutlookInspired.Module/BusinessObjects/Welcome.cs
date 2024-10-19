using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
using OutlookInspired.Module.Attributes.Appearance;
using OutlookInspired.Module.Services;
using OutlookInspired.Module.Services.Internal;

namespace OutlookInspired.Module.BusinessObjects{
    [DomainComponent][ForbidCRUD][ForbidNavigation]
    [ImageName("About")]
    public class Welcome : NonPersistentBaseObject {
        public Welcome() => About = GetType().Assembly.GetManifestResourceStream(s => s.EndsWith("Welcome.pdf")).Bytes();
        

        [EditorAlias(EditorAliases.PdfViewerEditor)]
        public byte[] About{ get; set; }
    }
}