using System.Collections;
using System.ComponentModel;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Blazor;
using DevExpress.ExpressApp.Blazor.Components;
using DevExpress.ExpressApp.Blazor.Components.Models;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Model;
using Microsoft.AspNetCore.Components;
using OutlookInspired.Blazor.Server.Components.DevExtreme.Maps;
using OutlookInspired.Blazor.Server.Services;
using OutlookInspired.Module.BusinessObjects;
using OutlookInspired.Module.Services.Internal;

namespace OutlookInspired.Blazor.Server.Editors.Maps{
    [ListEditor(typeof(IMapItem),true)]
    public class MapItemListEditor(IModelListView model)
        : ListEditor(model), IComplexListEditor, IComponentContentHolder{
        public event EventHandler<CustomizeLayersArgs> CustomizeLayers; 
        private RenderFragment _componentContent;
        private DevExtremeMap _mapInstance;
        private List<IMapItem> _selectedItems=new();
        private CollectionSourceBase _collectionSource;

        protected override object CreateControlsCore() 
            => new DevExtremeVectorMapModel();

        protected override void AssignDataSourceToControl(object dataSource){
            if (dataSource is IBindingList bindingList){
                bindingList.ListChanged -= BindingList_ListChanged;
            }
            UpdateDataSource(dataSource);
            if (dataSource is IBindingList newBindingList){
                newBindingList.ListChanged += BindingList_ListChanged;
            }
        }

        
        IEnumerable<string> GenerateAppColors(int count) {
            var appColors = ServiceProvider.GetRequiredService<IColorService>().AppColors;
            return Enumerable.Range(0, count).Select(i => appColors[i % appColors.Count]);
        }

        public Dictionary<string, string> CreatePalette(IEnumerable<string> itemNames) {
            var distinctProducts = itemNames.Distinct().ToList();
            var colors = GenerateAppColors(distinctProducts.Count).ToArray(); 
            return distinctProducts.ToDictionary(product => product, product => colors[distinctProducts.IndexOf(product)]);
        }
        private void UpdateDataSource(object datasource){
            if (Control is null) return;
            var mapItems = ((IEnumerable)datasource).Cast<IMapItem>().ToArray();
            var e = new CustomizeLayersArgs(mapItems);
            OnCustomizeLayers(e);
            Control.Layers =e.Layers;
            Control.Bounds = mapItems.Bounds();
            Control.CustomAttributes = [nameof(IMapItem.City)];
            Control.SelectionChanged = EventCallback.Factory.Create<string[]>(this, items => {
                _selectedItems =mapItems.Where(item => items.Contains(item.City)).ToList();
                OnSelectionChanged();
            });
        }

        public FeatureCollection CreateFeatureCollection(IMapItem[] groupedMapItems) 
            => new(){ Features = groupedMapItems.GroupBy(item => item.City)
                .Select(group => NewFeature(group, items => items.Select(item => item.Total).ToList())).ToList()
            };

        Feature NewFeature(IGrouping<string, IMapItem> group,Func<IGrouping<string,IMapItem>,List<decimal>> valuesSelector){
            var mapItem = group.First();
            return new Feature{
                Geometry = new Geometry{ Coordinates =[mapItem.Longitude, mapItem.Latitude] },
                Properties = new Properties{
                    Values = valuesSelector(group),
                    Tooltip = $"<span class='{mapItem.City}'>{mapItem.City} Total: {group.Sum(item => item.Total)}</span>",
                    City = mapItem.City
                }
            };
        }

        protected override void OnSelectionChanged(){
            base.OnSelectionChanged();
            OnProcessSelectedItem();
        }

        private void BindingList_ListChanged(object sender, ListChangedEventArgs e) => Refresh();

        public override void Refresh(){
            if (_mapInstance == null) return;
            _collectionSource.ResetCollection();
            _mapInstance?.Refresh();
        }

        public new DevExtremeVectorMapModel Control => (DevExtremeVectorMapModel)base.Control;

        public override IList GetSelectedObjects() => _selectedItems;

        public RenderFragment ComponentContent 
            => _componentContent ??= ComponentModelObserver.Create(Control, Control
                .GetComponentContent(o => _mapInstance = (DevExtremeMap)o));

        public override SelectionType SelectionType => SelectionType.Full;

        

        public void Setup(CollectionSourceBase collectionSource, XafApplication application){
            _collectionSource=collectionSource;
        }

        public void ApplyColors<TItem>(TItem[] mapItems,Func<TItem,string> propertySelector) where TItem:IMapItem{
            var palette = CreatePalette(mapItems.Select(propertySelector));
            foreach (var item in mapItems){
                item.Color = palette[propertySelector(item)];
            }
        }

        protected virtual void OnCustomizeLayers(CustomizeLayersArgs e) => CustomizeLayers?.Invoke(this, e);
    }

    public class CustomizeLayersArgs(IMapItem[] mapItems){
        public IMapItem[] MapItems{ get; } = mapItems;
        public List<BaseLayer> Layers{ get; } = new();
    }
}