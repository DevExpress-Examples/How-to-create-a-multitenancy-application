using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Templates;
using DevExpress.Persistent.Base;
using OutlookInspired.Blazor.Server.Editors.Maps;
using OutlookInspired.Module.BusinessObjects;
using ImageLoader = DevExpress.ExpressApp.Utils.ImageLoader;

namespace OutlookInspired.Blazor.Server.Features.Customers{
    public class MapsSalesPeriodViewController:ObjectViewController<DetailView,Customer>{
        const string HasMapItemVectorMapListEditor = "hasMapItemVectorMapListEditor";
        public MapsSalesPeriodViewController(){
            SalesPeriodAction = new SingleChoiceAction(this,"SalesPeriod",PredefinedCategory.PopupActions){
                    ItemType = SingleChoiceActionItemType.ItemIsOperation,
                    ImageMode = ImageMode.UseItemImage,
                    DefaultItemMode = DefaultItemMode.LastExecutedItem,
                    PaintStyle = ActionItemPaintStyle.Image
                };
            SalesPeriodAction.Executed+=SalesPeriodActionOnExecuted;
            SalesPeriodAction.Items.AddRange(Enum.GetValues<Period>().Where(period => period!=Period.FixedDate)
                .Select(period => new ChoiceActionItem(period.ToString(), period){
                    ImageName = ImageLoader.Instance.GetEnumValueImageName(period)
                }).ToArray());
            SalesPeriodAction.SelectedItem = SalesPeriodAction.Items.First();
            
            SalesPeriodAction.Active[HasMapItemVectorMapListEditor] = false;
        }

        public SingleChoiceAction SalesPeriodAction{ get; }

        protected override void OnActivated(){
            base.OnActivated();
            View.CustomizeViewItemControl<ListPropertyEditor>(this, _ 
                => SalesPeriodAction.Active[HasMapItemVectorMapListEditor] = true,nameof(Customer.CitySales));
        }
        
        private void SalesPeriodActionOnExecuted(object sender, ActionBaseEventArgs e){
            ((Customer)View.CurrentObject).SalesPeriod = (Period)SalesPeriodAction.SelectedItem.Data;
            var vectorMapListEditor = View.GetItems<ListPropertyEditor>()
                .Select(editor => editor.ListView?.Editor).OfType<MapItemListEditor>().First();
            vectorMapListEditor.Refresh();
        }
    }
}