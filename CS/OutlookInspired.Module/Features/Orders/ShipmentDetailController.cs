using System.Drawing;
using DevExpress.Blazor.Internal;
using DevExpress.ExpressApp;
using DevExpress.Pdf;
using DevExpress.XtraReports.UI;
using OutlookInspired.Module.BusinessObjects;
using OutlookInspired.Module.Resources.Reports;

namespace OutlookInspired.Module.Features.Orders{
    public class ShipmentDetailController:ObjectViewController<DetailView,Order>{
        public ShipmentDetailController() => TargetViewId = Order.MapsDetailView;
        protected override void OnActivated(){
            base.OnActivated();
            using var fedExGroundLabel = new FedExGroundLabel();
            var order = ((Order)View.CurrentObject);
            fedExGroundLabel.DataSource = order.Yield();
            order.ShipmentDetail = ToPdf(fedExGroundLabel,WatermarkText(order));
        }
        
        string WatermarkText( Order order) 
            => order.ShipmentStatus switch{
                ShipmentStatus.Received => "Shipment Received",
                ShipmentStatus.Transit => "Shipment in Transit",
                _ => "Awaiting shipment"
            };

        
        byte[] ToPdf(XtraReport report,string waterMarkText=null){
            using var memoryStream = new MemoryStream();
            report.ExportToPdf(memoryStream);
            var bytes = memoryStream.ToArray();
            if (waterMarkText != null){
                using var processor = new PdfDocumentProcessor();
                using var memStream = new MemoryStream(bytes);
                processor.LoadDocument(memStream);
                var pages = processor.Document.Pages;
                using var font = new Font("Segoe UI", 48, FontStyle.Regular);
                foreach (var t in pages){
                    using var graphics = processor.CreateGraphics();
                    var pageLayout = new RectangleF(
                        -(float)t.CropBox.Width * 0.35f,
                        (float)t.CropBox.Height * 0.1f,
                        (float)t.CropBox.Width * 1.25f,
                        (float)t.CropBox.Height);
                        
                    var angle = Math.Asin(pageLayout.Width / (double)pageLayout.Height) * 180.0 / Math.PI;
                    graphics.TranslateTransform(-pageLayout.X, -pageLayout.Y);
                    graphics.RotateTransform((float)angle);

                    using(var textBrush = new SolidBrush(Color.FromArgb(100, Color.Red)))
                        graphics.DrawString(waterMarkText, font, textBrush, new PointF(50, 50));
                    graphics.AddToPageForeground(t);
                }
                using var stream = new MemoryStream();
                processor.SaveDocument(stream);
                return stream.ToArray();

            }

            return bytes;
        }

    }
}