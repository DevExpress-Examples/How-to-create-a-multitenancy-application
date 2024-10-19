using System.Collections;
using DevExpress.Blazor;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Blazor;
using DevExpress.ExpressApp.Blazor.Components;
using DevExpress.ExpressApp.Blazor.Components.Models;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Model;
using Microsoft.AspNetCore.Components;

namespace OutlookInspired.Blazor.Server.Editors.Pivot {
    [ListEditor(typeof(object))]
    public class PivotGridListEditor(IModelListView info)
        : ListEditor(info), IComponentContentHolder{
        public new DxPivotGridModel Control => (DxPivotGridModel)base.Control;
        protected override object CreateControlsCore() => new DxPivotGridModel{ Data = [] };

        protected override void AssignDataSourceToControl(object dataSource) {
            if(Control == null||dataSource==null) return;
            Control.Data = ((IEnumerable)dataSource).Cast<object>();
        }

        public override void Refresh(){
            if (DataSource == null) return;
            Control.Data = ((IEnumerable)DataSource).Cast<object>();
        }
        
        public override object FocusedObject { get; set; }
        public override IList GetSelectedObjects() => Array.Empty<object>();
        public override SelectionType SelectionType => SelectionType.None;

        private RenderFragment _componentContent;
        
        RenderFragment IComponentContentHolder.ComponentContent 
            => _componentContent ??= ComponentModelObserver.Create(Control, Control.GetComponentContent());
    }

    public class PivotField:IPivotField{
        public string Format{ get; set; }
        public string Name{ get; set; }
        public PivotGridSortOrder SortOrder{ get; set; }
        public PivotGridFieldArea Area{ get; set; }
        public PivotGridGroupInterval GroupInterval{ get; set; }
        public string Caption{ get; set; }
        public PivotGridSummaryType SummaryType{ get; set; }
        public string DisplayFormat{ get; set; }
        public bool IsProgressBar{ get; set; }
        
    }
}