using System.Collections;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Blazor;
using DevExpress.ExpressApp.Blazor.Components;
using DevExpress.ExpressApp.Blazor.Components.Models;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Model;
using Microsoft.AspNetCore.Components;
using OutlookInspired.Module.BusinessObjects;

namespace OutlookInspired.Blazor.Server.Editors.MapItemChart {
    [ListEditor(typeof(MapItem),true)]
    public class MapItemDxChartListEditor(IModelListView info)
        : ListEditor(info), IComponentContentHolder{
        public MapItemDxChartModel MapItemDxChartModel => (MapItemDxChartModel)Control;
        protected override object CreateControlsCore() => new MapItemDxChartModel();
        protected override void AssignDataSourceToControl(object dataSource) {
            if(MapItemDxChartModel == null) return;
            MapItemDxChartModel.Data = dataSource as IEnumerable<MapItem>;
        }
        public override void Refresh() { }
        public override object FocusedObject { get; set; }
        public override IList GetSelectedObjects() => Array.Empty<object>();
        public override SelectionType SelectionType => SelectionType.None;

        private RenderFragment _componentContent;
        RenderFragment IComponentContentHolder.ComponentContent => _componentContent ??= ComponentModelObserver.Create(MapItemDxChartModel, MapItemDxChartModel.GetComponentContent());
    }
}