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
        public new MapItemDxChartModel Control => (MapItemDxChartModel)base.Control;
        protected override object CreateControlsCore() => new MapItemDxChartModel();
        protected override void AssignDataSourceToControl(object dataSource) {
            if(Control == null) return;
            Control.Data = dataSource as IEnumerable<MapItem>;
        }
        
        public override void Refresh(){ }
        
        public override object FocusedObject { get; set; }
        public override IList GetSelectedObjects() => Array.Empty<object>();
        public override SelectionType SelectionType => SelectionType.None;

        private RenderFragment _componentContent;
        private MapItemDxChart _instance;

        RenderFragment IComponentContentHolder.ComponentContent 
            => _componentContent ??= ComponentModelObserver.Create(Control, Control.GetComponentContent(o => _instance=(MapItemDxChart)o));
    }
}