using DevExpress.Blazor;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Blazor.Components;
using DevExpress.ExpressApp.Blazor.Components.Models;
using DevExpress.ExpressApp.Blazor.Editors;
using Microsoft.AspNetCore.Components;
using OutlookInspired.Blazor.Server.Components.DxGrid;
using OutlookInspired.Blazor.Server.Editors.LayoutView;
using OutlookInspired.Module.BusinessObjects;

namespace OutlookInspired.Blazor.Server.Features.Customers {
    public class CustomerListViewDetailRowController : ViewController<ListView> {
        public CustomerListViewDetailRowController() => TargetViewId = $"{nameof(Customer)}_ListView";

        protected override void OnViewControlsCreated() {
            base.OnViewControlsCreated();
            if (View.Editor is not DxGridListEditor editor) return;
            editor.GridModel.AutoCollapseDetailRow = true;
            var customerEmployeeModel = CustomerEmployeeModel();
            var recentOrdersModel = RecentOrdersModel();
            editor.GridModel.DetailRowTemplate = value => {
                var customer = ((Customer)value.DataItem);
                customerEmployeeModel.Data = customer.Employees;
                var customerEmployeeContent = customerEmployeeModel.GetComponentContent();
                recentOrdersModel.Data = customer.RecentOrders;
                var detailRowModel = new DxGridDetailRowModel{
                    RenderFragment = customerEmployeeContent,
                    Tabs =[("Employess",typeof(Customer)),("Recent Orders",typeof(Order))]
                };
                RenderFragment recentOrdersContent = null;
                detailRowModel.ActiveTabIndexChanged = EventCallback.Factory.Create<int>(this, i => {
                    if (i == 0){
                        detailRowModel.RenderFragment =customerEmployeeContent;
                    }
                    else{
                        detailRowModel.RenderFragment =recentOrdersContent ??= recentOrdersModel.GetComponentContent();
                    }
                    
                });
                return ComponentModelObserver.Create(detailRowModel, detailRowModel.GetComponentContent());
            };
        }

        private static MyDxGridModel RecentOrdersModel() => new(){
            AllowSelectRowByClick = true,
            Columns = [
                new GridDataColumnModel{ FieldName = nameof(Order.InvoiceNumber) },
                new GridDataColumnModel{ FieldName = nameof(Order.Employee) },
                new GridDataColumnModel{ FieldName = nameof(Order.OrderDate),SortIndex = 0},
                new GridDataColumnModel{ FieldName = nameof(Order.ShipDate) },
                new GridDataColumnModel{ FieldName = nameof(Order.SaleAmount) },
                new GridDataColumnModel{ FieldName = nameof(Order.ShippingAmount) },
                new GridDataColumnModel{ FieldName = nameof(Order.TotalAmount) }
            ],
            SummaryItems = [
                new GridSummaryItemModel(){SummaryType = GridSummaryItemType.Count,FieldName = nameof(Order.InvoiceNumber)},
                new GridSummaryItemModel(){SummaryType = GridSummaryItemType.Sum,FieldName = nameof(Order.SaleAmount)},
                new GridSummaryItemModel(){SummaryType = GridSummaryItemType.Sum,FieldName = nameof(Order.TotalAmount)},
                new GridSummaryItemModel(){SummaryType = GridSummaryItemType.Sum,FieldName = nameof(Order.ShippingAmount)},
            ],
        };

        private static LayoutViewModel CustomerEmployeeModel(){
            var customerEmployeeModel = new LayoutViewModel{
                HeaderSelector = o => ((CustomerEmployee)o).FullName,
                InfoItemsSelector = o => {
                    var customerEmployee = ((CustomerEmployee)o);
                    return new Dictionary<string, string>{{ "EMAIL", $"<a href='mailto:{customerEmployee.Email}'>{customerEmployee.Email}</a>" }, 
                        { "PHONE", customerEmployee.MobilePhone } };
                },
                ImageSelector = o => ((CustomerEmployee)o).Picture.Data
            };
            return customerEmployeeModel;
        }
    }
}