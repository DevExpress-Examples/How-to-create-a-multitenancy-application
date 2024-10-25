using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Model;

namespace OutlookInspired.Blazor.Server.Editors.Charts{
    [ListEditor(typeof(object))]
    public class DxChartPieListEditor(IModelListView info) :DxChartListEditor(info){
        protected override object CreateControlsCore() => new DxChartPieModel();
        
        public new DxChartPieModel Control => (DxChartPieModel)base.Control;
    }
}