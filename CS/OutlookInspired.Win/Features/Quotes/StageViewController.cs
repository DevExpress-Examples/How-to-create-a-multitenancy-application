using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Templates;
using DevExpress.ExpressApp.Utils;
using DevExpress.Persistent.Base;
using OutlookInspired.Module.BusinessObjects;
using OutlookInspired.Win.Editors.Maps;

namespace OutlookInspired.Win.Features.Quotes{
    public class StageViewController:ObjectViewController<DetailView,Quote>{
        public StageViewController(){
            TargetViewId = Quote.MapsDetailView;
            StageAction = new SingleChoiceAction(this,"Stage",PredefinedCategory.View);
            StageAction.ItemType=SingleChoiceActionItemType.ItemIsOperation;
            StageAction.ImageMode=ImageMode.UseItemImage;
            StageAction.DefaultItemMode = DefaultItemMode.LastExecutedItem;
            StageAction.PaintStyle=ActionItemPaintStyle.Image;
            StageAction.Executed+=ActionOnExecuted;
            StageAction.Items.AddRange(Enum.GetValues<Stage>().Where(stage => stage!=Stage.Summary)
                .Select(stage => new ChoiceActionItem(stage.ToString(), stage){ImageName = ImageLoader.Instance.GetEnumValueImageName(stage)}).ToArray());
            StageAction.SelectedItem = StageAction.Items.First();
        }

        public SingleChoiceAction StageAction{ get; }
        
        private void ActionOnExecuted(object sender, ActionBaseEventArgs e){
            ((Quote)View.CurrentObject).Stage = (Stage)StageAction.SelectedItem.Data;
            var vectorMapListEditor = View.GetItems<ListPropertyEditor>()
                .Select(editor => editor.ListView?.Editor).OfType<VectorMapListEditor>().First();
            vectorMapListEditor.Refresh();
        }

    }
}