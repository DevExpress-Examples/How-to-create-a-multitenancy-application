using DevExpress.ExpressApp;
using OutlookInspired.Blazor.Server.Editors.LayoutView;
using OutlookInspired.Module.BusinessObjects;

namespace OutlookInspired.Blazor.Server.Features.Products{
    public class ProductLayoutViewController:ObjectViewController<ListView, Product>{
        public ProductLayoutViewController() => TargetViewId = Product.LayoutViewListView;
        
        protected override void OnViewControlsCreated(){
            base.OnViewControlsCreated();
            var model = ((LayoutViewListEditor)View.Editor).Control;
            model.ImageSelector = o => ((Product)o).PrimaryImage.Data;
            model.HeaderSelector = o => ((Product)o).Name;
            model.InfoItemsSelector = o => {
                var product = ((Product)o);
                return new Dictionary<string, string>{
                    { "COST", product.Cost.ToString("C") },
                    { "SALE PRICE", product.SalePrice.ToString("C") }                };
            };
            model.FooterSelector = o => ((Product)o).DescriptionString;
        }
        
    }
}