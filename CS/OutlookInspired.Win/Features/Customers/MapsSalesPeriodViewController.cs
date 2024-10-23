using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Templates;
using DevExpress.Persistent.Base;
using OutlookInspired.Module.BusinessObjects;
using OutlookInspired.Module.Features.Maps;
using OutlookInspired.Win.Editors.Maps;
using ImageLoader = DevExpress.ExpressApp.Utils.ImageLoader;

namespace OutlookInspired.Win.Features.Customers{
    public class MapsSalesPeriodViewController:ObjectViewController<DetailView,ISalesMapsMarker>{
        public MapsSalesPeriodViewController(){
            SalesPeriodAction = new SingleChoiceAction(this,"SalesPeriod",PredefinedCategory.View);
            SalesPeriodAction.ItemType=SingleChoiceActionItemType.ItemIsOperation;
            SalesPeriodAction.ImageMode=ImageMode.UseItemImage;
            SalesPeriodAction.DefaultItemMode = DefaultItemMode.LastExecutedItem;
            SalesPeriodAction.PaintStyle=ActionItemPaintStyle.Image;
            SalesPeriodAction.Executed+=SalesPeriodActionOnExecuted;
            SalesPeriodAction.Items.AddRange(Enum.GetValues<Period>().Where(period => period!=Period.FixedDate)
                .Select(period => new ChoiceActionItem(period.ToString(), period){
                    ImageName = ImageLoader.Instance.GetEnumValueImageName(period)
                }).ToArray());
            SalesPeriodAction.SelectedItem = SalesPeriodAction.Items.First();
        }

        public SingleChoiceAction SalesPeriodAction{ get; }

        protected override void OnViewControlsCreated(){
            base.OnViewControlsCreated();
            var hasMapItemVectorMapListEditor = View.GetItems<ListPropertyEditor>()
                .Any(editor => editor.ListView?.Editor is MapItemListEditor);
            Active["hasMapItemVectorMapListEditor"] = hasMapItemVectorMapListEditor;
        }

        private void SalesPeriodActionOnExecuted(object sender, ActionBaseEventArgs e){
            throw new NotImplementedException();
            // ((ISalesMapsMarker)View.CurrentObject).SalesPeriod = (Period)SalesPeriodAction.SelectedItem.Data;
            // var vectorMapListEditor = View.GetItems<ListPropertyEditor>()
                // .Select(editor => editor.ListView?.Editor).OfType<MapItemListEditor>().First();
            // vectorMapListEditor.Refresh();
        }
    }
}