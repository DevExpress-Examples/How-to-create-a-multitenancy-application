using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Blazor;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Model;
using Microsoft.AspNetCore.Components;
using OutlookInspired.Blazor.Server.ComponentModels;
using System.Collections;
using OutlookInspired.Blazor.Server.Services.Internal;

namespace OutlookInspired.Blazor.Server.Editors {
    public abstract class ChartListEditor<T, TArgument, TValue, TName>(IModelListView info)
        : ListEditor(info), IComponentContentHolder{
        public ChartModel<T, TArgument, TValue, TName> ChartModel => (ChartModel<T, TArgument, TValue, TName>)Control;
        protected override object CreateControlsCore() => new ChartModel<T, TArgument, TValue, TName>();
        protected override void AssignDataSourceToControl(object dataSource) {
            if(ChartModel == null) return;
            ChartModel.Data = dataSource as IEnumerable<T>;
        }
        public override void Refresh() { }
        public override object FocusedObject { get; set; }
        public override IList GetSelectedObjects() => Array.Empty<object>();
        public override SelectionType SelectionType => SelectionType.None;

        private RenderFragment _componentContent;
        public RenderFragment ComponentContent => _componentContent ??= ChartModel.Create();
    }
}