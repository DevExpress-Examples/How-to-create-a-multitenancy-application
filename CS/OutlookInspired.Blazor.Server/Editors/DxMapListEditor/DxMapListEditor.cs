using System.Collections;
using System.ComponentModel;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Blazor;
using DevExpress.ExpressApp.Blazor.Components;
using DevExpress.ExpressApp.Blazor.Components.Models;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base.General;
using Microsoft.AspNetCore.Components;
using OutlookInspired.Blazor.Server.Components.DevExtreme.Maps.DxMap;

namespace OutlookInspired.Blazor.Server.Editors.DxMapListEditor{
    [ListEditor(typeof(ITreeNode))]
    public class DxMapListEditor(IModelListView model) : ListEditor(model), IComplexListEditor, IComponentContentHolder{
        private RenderFragment _componentContent;
        public DxMapModel ComponentModel { get; private set; }
        public RenderFragment ComponentContent 
            => _componentContent ??= ComponentModelObserver.Create(ComponentModel, ComponentModel.GetComponentContent(AddComponentReferenceCapture));

        protected void AddComponentReferenceCapture(object instance) => _dxMapComponent = (DxMapComponent)instance;

        private ITreeNode[] _selectedObjects = [];
        private IObjectSpace _objectSpace;
        private IEnumerable<ITreeNode> _dataSource;
        private DxMapComponent _dxMapComponent;
        void IComplexListEditor.Setup(CollectionSourceBase collectionSource, XafApplication application) 
            => _objectSpace = collectionSource.ObjectSpace;

        protected override object CreateControlsCore() 
            => ComponentModel = new DxMapModel{
                // GetFieldDisplayText = GetFieldDisplayText,
                // GetKey = GetKey,
                // GetNode = GetNode,
                // HasChildren = HasChildren,
                // RowClick = EventCallback.Factory.Create<string>(this, ComponentModel_RowClick),
                // SelectionChanged = EventCallback.Factory.Create<string[]>(this, ComponentModel_SelectionChanged)
            };

        protected override void AssignDataSourceToControl(object dataSource){
            if (_dataSource is IBindingList bindingList){
                bindingList.ListChanged -= BindingList_ListChanged;
            }
            UpdateDataSource(dataSource);
            if (dataSource is IBindingList newBindingList){
                newBindingList.ListChanged += BindingList_ListChanged;
            }
        }
        private void UpdateDataSource(object dataSource){
            if (ComponentModel is null) return;
            // ComponentModel.Data = (dataSource as IEnumerable)?.Cast<ITreeNode>();
        }
        // private Task<IEnumerable<ITreeNode>> GetDataAsync(string parentKey)
        // {
        //     bool IsRoot(object obj)
        //     {
        //         return obj is ITreeNode node && (node.Parent == null || !_dataSource.Contains(node.Parent));
        //     }
        //     if (parentKey is null)
        //     {
        //         IEnumerable<ITreeNode> rootData = _dataSource?.Where(n => IsRoot(n)) ?? Array.Empty<ITreeNode>();
        //         return Task.FromResult(rootData);
        //     }
        //     ITreeNode parent = GetNode(parentKey);
        //     return Task.FromResult(parent.Children.Cast<ITreeNode>());
        // }
        private string GetFieldDisplayText(object item, string field) => ObjectTypeInfo.FindMember(field).GetValue(item)?.ToString();
        private string GetKey(object item) => _objectSpace.GetObjectHandle(item);
        private ITreeNode GetNode(string key) => (ITreeNode)_objectSpace.GetObjectByHandle(key);
        private bool HasChildren(object item) => ((ITreeNode)item).Children.Count > 0;
        public override void BreakLinksToControls(){
            AssignDataSourceToControl(null);
            _dxMapComponent = null;
            base.BreakLinksToControls();
        }
        public override void Refresh(){
            UpdateDataSource(DataSource);
            // treeListInstance?.Refresh();
        }
        private void BindingList_ListChanged(object sender, ListChangedEventArgs e) => Refresh();

        private void ComponentModel_RowClick(string key){
            _selectedObjects = new ITreeNode[] { GetNode(key) };
            OnSelectionChanged();
            OnProcessSelectedItem();
        }
        private void ComponentModel_SelectionChanged(string[] keys){
            var items = keys.Select(GetNode).ToArray();
            _selectedObjects = items;
            OnSelectionChanged();
        }
        public override SelectionType SelectionType => SelectionType.Full;
        public override IList GetSelectedObjects() => _selectedObjects;
    }
}