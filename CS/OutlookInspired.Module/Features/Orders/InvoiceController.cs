using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Editors;
using DevExpress.XtraRichEdit;
using OutlookInspired.Module.BusinessObjects;

namespace OutlookInspired.Module.Features.Orders{

    public class InvoiceReportDocumentController : ObjectViewController<DetailView, Order>{
        public InvoiceReportDocumentController() => TargetViewId = Order.InvoiceDetailView;
        protected override void OnActivated(){
            base.OnActivated();
            var order = (Order)View.CurrentObject;
            order.InvoiceDocument = order.MailMergeInvoice();
        }
    }

    public class InvoiceController : ObjectViewController<DetailView, Order>{
        public InvoiceController() => TargetViewId = Order.ChildDetailView;

        protected override void OnDeactivated(){
            base.OnDeactivated();
            View.CurrentObjectChanged-=ViewOnCurrentObjectChanged;
        }

        protected override void OnActivated(){
            base.OnActivated();
            View.CurrentObjectChanged+=ViewOnCurrentObjectChanged;
        }
        
        private void ViewOnCurrentObjectChanged(object sender, EventArgs e){
            if (View.CurrentObject==null)return;
            var order = ((Order)View.CurrentObject);
            order.InvoiceDocument = ToPdf(order.MailMergeInvoice());
            var propertyEditor = View.GetItems<PropertyEditor>().First(editor => editor.MemberInfo.Name==nameof(Order.InvoiceDocument));
            propertyEditor.ReadValue();
        }
        
        byte[] ToPdf(byte[] bytes){
            using var richEditDocumentServer = new RichEditDocumentServer();
            richEditDocumentServer.LoadDocument(bytes,DocumentFormat.OpenXml);
            using var memoryStream = new MemoryStream();
            richEditDocumentServer.ExportToPdf(memoryStream);
            return memoryStream.ToArray();
        }

    }
}