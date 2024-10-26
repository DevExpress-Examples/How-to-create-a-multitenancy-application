using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Office;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.Office.Services;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl.EF;
using DevExpress.XtraRichEdit;
using OutlookInspired.Module.BusinessObjects;
using MergeMode = DevExpress.XtraRichEdit.API.Native.MergeMode;

namespace OutlookInspired.Module.Features.Orders{

    public class InvoiceReportDocumentController : ObjectViewController<DetailView, Order>{
        public InvoiceReportDocumentController() => TargetViewId = Order.InvoiceDetailView;
        static RichEditDocumentServer CreateDocumentServer(byte[] bytes, params object[] dataSource) 
            => new(){
                OpenXmlBytes = bytes,
                Options ={
                    MailMerge ={
                        DataSource = dataSource
                    }
                }
            };

        public static byte[] MailMergeInvoice( Order order){
            var template = MailMergeData(order.ObjectSpace,"Order").Template;
            var richEditDocumentServer = CreateDocumentServer(template,order);
            return MailMergeInvoice(richEditDocumentServer, order);
        }

        static RichTextMailMergeData MailMergeData(IObjectSpace objectSpace, string name) 
            => objectSpace.GetObjectsQuery<RichTextMailMergeData>().First(data => data.Name==name);

        static void MailMerge<T>( IRichEditDocumentServer documentServer,IRichTextMailMergeData mailMergeData, MergeMode mergeMode,params T[] dataSource){
            using var mergedServer = CreateDocumentServer(mailMergeData.Template,dataSource);
            using var memoryStream = new MemoryStream(mailMergeData.Template);
            mergedServer.LoadDocumentTemplate(memoryStream, DocumentFormat.OpenXml);
            mergedServer.Options.MailMerge.DataSource = dataSource;
            var options = mergedServer.Document.CreateMailMergeOptions();
            options.MergeMode = mergeMode;
            mergedServer.MailMerge(options, documentServer.Document);
        }

        static void CalculateDocumentVariable(CalculateDocumentVariableEventArgs e,Order order, IRichEditDocumentServer richEditDocumentServer){
            switch (e.VariableName){
                case nameof(Order.OrderItems):
                    MailMerge(richEditDocumentServer,MailMergeData(order.ObjectSpace,"OrderItem"),MergeMode.JoinTables, order.OrderItems.ToArray());
                    e.PreserveInsertedContentFormatting = true;
                    e.KeepLastParagraph = false;
                    e.Value = richEditDocumentServer;
                    e.Handled = true;
                    break;
                case "Total":
                    e.Value = order.OrderItems.Sum(x => x.Total);
                    e.Handled = true;
                    break;
                case "TotalDue":
                    e.Value = order.OrderItems.Sum(x => x.Total) + order.ShippingAmount;
                    e.Handled = true;
                    break;
            }
        }

        static byte[] MailMerge<T>(IRichEditDocumentServer documentServer,params T[] datasource){
            using var stream = new MemoryStream();
            documentServer.GetService<IUriStreamService>().RegisterProvider(new ImageStreamProviderBase(
                documentServer.Options.MailMerge, datasource, XafTypesInfo.Instance.FindTypeInfo(typeof(T))));
            documentServer.MailMerge(documentServer.CreateMailMergeOptions(), stream, DocumentFormat.OpenXml);
            return stream.ToArray();
        }

        static byte[] MailMergeInvoice(IRichEditDocumentServer richEditDocumentServer, Order order){
            richEditDocumentServer.CalculateDocumentVariable +=
                (_, e) => CalculateDocumentVariable(e, order, richEditDocumentServer);
            return MailMerge(richEditDocumentServer, order);
        }

        protected override void OnActivated(){
            base.OnActivated();
            var order = (Order)View.CurrentObject;
            order.InvoiceDocument = MailMergeInvoice(order);
            Frame.GetController<DialogController>().SaveOnAccept = true;
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
            if (View.CurrentObject is not Order order) return;
            var orderInvoiceDocument = InvoiceReportDocumentController.MailMergeInvoice(order);
            order.InvoiceDocument = ToPdf(orderInvoiceDocument);
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