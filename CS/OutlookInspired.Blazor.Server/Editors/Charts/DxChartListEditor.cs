using System.Collections;
using System.ComponentModel;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Blazor;
using DevExpress.ExpressApp.Blazor.Components;
using DevExpress.ExpressApp.Blazor.Components.Models;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Model;
using Microsoft.AspNetCore.Components;

namespace OutlookInspired.Blazor.Server.Editors.Charts {
    [ListEditor(typeof(object))]
    public class DxChartListEditor(IModelListView info) :ListEditor(info), IComponentContentHolder{
        protected override object CreateControlsCore() => new DxChartModel();
        public new DxChartModel Control => (DxChartModel)base.Control;
        protected override void AssignDataSourceToControl(object dataSource) {
            if(Control == null||dataSource==null) return;
            if (dataSource is IBindingList bindingList){
                bindingList.ListChanged -= BindingList_ListChanged;
            }
            Control.Data = ((IEnumerable)dataSource).Cast<object>();
            if (dataSource is IBindingList newBindingList){
                newBindingList.ListChanged += BindingList_ListChanged;
            }
        }

        private void BindingList_ListChanged(object sender, ListChangedEventArgs e) => Refresh();

        public override void Refresh(){
            if (Control==null||DataSource is not IEnumerable dataSource) return;
            Control.Data = dataSource.Cast<object>();
        }

        public override object FocusedObject { get; set; }
        public override IList GetSelectedObjects() => Array.Empty<object>();
        public override SelectionType SelectionType => SelectionType.None;

        private RenderFragment _componentContent;

        RenderFragment IComponentContentHolder.ComponentContent 
            => _componentContent ??= ComponentModelObserver.Create(Control, Control.GetComponentContent());
        
    }
}