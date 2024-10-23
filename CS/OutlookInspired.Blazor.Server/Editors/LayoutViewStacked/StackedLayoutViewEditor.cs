using System.Collections;
using System.ComponentModel;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Blazor;
using DevExpress.ExpressApp.Blazor.Components;
using DevExpress.ExpressApp.Blazor.Components.Models;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Model;
using Microsoft.AspNetCore.Components;

namespace OutlookInspired.Blazor.Server.Editors.LayoutViewStacked{
    [ListEditor(typeof(object))]
    public class StackedLayoutViewEditor(IModelListView model) : ListEditor(model), IComponentContentHolder,IComplexListEditor{
        private RenderFragment _componentContent;
        private readonly IList _selectedObjects = new List<object>();
        private CollectionSourceBase _collectionSource;

        protected override object CreateControlsCore() 
            => new StackedLayoutViewModel();

        public new StackedLayoutViewModel Control => (StackedLayoutViewModel)base.Control;
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

        public override void Refresh() => _collectionSource.ResetCollection();
        public void Setup(CollectionSourceBase collectionSource, XafApplication application) => _collectionSource=collectionSource;
        public override object FocusedObject { get; set; }
        public override IList GetSelectedObjects() => _selectedObjects;
        public override SelectionType SelectionType => SelectionType.Full;

        RenderFragment IComponentContentHolder.ComponentContent 
            => _componentContent ??= ComponentModelObserver.Create(Control, Control.GetComponentContent());

    }
}