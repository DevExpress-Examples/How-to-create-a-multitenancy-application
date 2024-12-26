using DevExpress.Blazor;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Blazor.Components;
using DevExpress.ExpressApp.Blazor.Components.Models;
using DevExpress.ExpressApp.Blazor.Editors;
using DevExpress.ExpressApp.EFCore;
using OutlookInspired.Blazor.Server.Components.DxGrid;
using OutlookInspired.Module.BusinessObjects;

namespace OutlookInspired.Blazor.Server.Features.Orders {
    public class OrderListViewDetailRowController : ViewController<ListView> {
        public OrderListViewDetailRowController() => TargetViewId = Order.ListViewDetail;

        protected override void OnViewControlsCreated() {
            base.OnViewControlsCreated();
            if (View.Editor is not DxGridListEditor editor) return;
            editor.GridModel.AutoCollapseDetailRow = true;
            var orderItemModel = OrderItemModel();
            editor.GridModel.DetailRowTemplate = value => {
                if(value.DataItem is EFCoreServerModeViewRecord viewRecord) {
                    if(viewRecord.ContainsMember("ID")) {
                        var keyValue = viewRecord["ID"];
                        orderItemModel.Data = ObjectSpace.GetObjectByKey<Order>(keyValue).OrderItems;
                        var orderItemsContent = orderItemModel.GetComponentContent();
                        var detailRowModel = new DxGridDetailRowModel { RenderFragment = orderItemsContent };
                        return ComponentModelObserver.Create(detailRowModel, detailRowModel.GetComponentContent());
                    }
                }
                return null;
            };
        }

        private static MyDxGridModel OrderItemModel() 
            => new(){
                AllowSelectRowByClick = true,
                Columns = [
                    new GridDataColumnModel{ FieldName = nameof(OrderItem.Product) },
                    new GridDataColumnModel{ FieldName = nameof(OrderItem.ProductUnits) },
                    new GridDataColumnModel{ FieldName = nameof(OrderItem.ProductPrice)},
                    new GridDataColumnModel{ FieldName = nameof(OrderItem.Discount) },
                    new GridDataColumnModel{ FieldName = nameof(OrderItem.Total) }
                ],
                SummaryItems = [
                    new GridSummaryItemModel(){SummaryType = GridSummaryItemType.Count,FieldName = nameof(OrderItem.ProductUnits)},
                    new GridSummaryItemModel(){SummaryType = GridSummaryItemType.Sum,FieldName = nameof(OrderItem.Discount)},
                    new GridSummaryItemModel(){SummaryType = GridSummaryItemType.Sum,FieldName = nameof(OrderItem.Total)}
                ],
            };

    }
}