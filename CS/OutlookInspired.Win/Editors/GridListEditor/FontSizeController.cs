using DevExpress.ExpressApp.SystemModule;
using DevExpress.XtraGrid.Views.BandedGrid;
using OutlookInspired.Module.Attributes;

namespace OutlookInspired.Win.Editors.GridListEditor{
    public class FontSizeController:ListViewControllerBase{
        protected override void OnViewControlsCreated(){
            base.OnViewControlsCreated();
            if (View.Editor is not DevExpress.ExpressApp.Win.Editors.GridListEditor gridListEditor) return;
            if (gridListEditor.GridView is not AdvBandedGridView gridView) return;
            IncreaseFontSize(gridView);
        }
        
        void IncreaseFontSize(AdvBandedGridView gridView){
            var attributedMembers = View.ObjectTypeInfo.Members.SelectMany(memberInfo => memberInfo.FindAttributes<FontSizeDeltaAttribute>()
                .Select(attribute => (attribute, memberInfo)));
            var columns = attributedMembers
                .ToDictionary(attribute => gridView.Columns[attribute.memberInfo.BindingName].VisibleIndex, attribute => attribute.attribute.Delta);
            gridView.CustomDrawCell += (_, e) => {
                if (!columns.TryGetValue(e.Column.VisibleIndex, out var fontSizeDelta)) return;
                e.Appearance.FillRectangle(e.Cache, e.Bounds);
                e.Appearance.FontSizeDelta = fontSizeDelta;
                e.Appearance.DrawString(e.Cache, e.DisplayText, e.Bounds);
                e.Handled = true;
            };
        }
        


    }
}