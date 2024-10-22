using System.Collections;
using System.ComponentModel;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Blazor;
using DevExpress.ExpressApp.Blazor.Components;
using DevExpress.ExpressApp.Blazor.Components.Models;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Model;
using Microsoft.AspNetCore.Components;
using OutlookInspired.Blazor.Server.Services;
using OutlookInspired.Module.BusinessObjects;

namespace OutlookInspired.Blazor.Server.Editors.Maps{
    [ListEditor(typeof(IMapItem),true)]
    public class MapItemListEditor(IModelListView model)
        : ListEditor(model), IComplexListEditor, IComponentContentHolder{
        public event EventHandler<CustomizeLayersArgs> CustomizeLayers; 
        private RenderFragment _componentContent;
        private DevExtremeMap _mapInstance;
        private List<IMapItem> _selectedItems=new();
        private CollectionSourceBase _collectionSource;
        private IMapItem[] _mapItems;

        protected override object CreateControlsCore()
            => new DevExtremeVectorMapModel{
                SelectionChanged = EventCallback.Factory.Create<string[]>(this, items => {
                    _selectedItems =_mapItems.Where(item => items.Contains(item.City)).ToList();
                    OnSelectionChanged();
                })
        };

        protected override void AssignDataSourceToControl(object dataSource){
            if (dataSource is IBindingList bindingList){
                bindingList.ListChanged -= BindingList_ListChanged;
            }
            if (Control is null) return;
            _mapItems = ((IEnumerable)dataSource).Cast<IMapItem>().ToArray();
            var e = new CustomizeLayersArgs(_mapItems);
            OnCustomizeLayers(e);
            Control.Layers =e.Layers;
            Control.CustomAttributes = [nameof(IMapItem.City)];
            if (dataSource is IBindingList newBindingList){
                newBindingList.ListChanged += BindingList_ListChanged;
            }
        }
        
        IEnumerable<string> GenerateAppColors(int count) {
            var appColors = ServiceProvider.GetRequiredService<IMapColorService>().AppColors;
            return Enumerable.Range(0, count).Select(i => appColors[i % appColors.Count]);
        }

        public Dictionary<string, string> CreatePalette(IEnumerable<string> itemNames) {
            var distinctProducts = itemNames.Distinct().ToList();
            var colors = GenerateAppColors(distinctProducts.Count).ToArray(); 
            return distinctProducts.ToDictionary(product => product, product => colors[distinctProducts.IndexOf(product)]);
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
        
        public static FeatureCollection CreateFeatureCollection(IMapItem[] mapItems, Func<IGrouping<string,IMapItem>,List<decimal>> itemsSelector) 
            => new(){ Features = mapItems.GroupBy(item => item.City)
                .Select(items => NewFeature(items, itemsSelector)).ToList()
            };

        static Feature NewFeature(IGrouping<string, IMapItem> group,Func<IGrouping<string,IMapItem>,List<decimal>> valuesSelector){
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

        public new DevExtremeVectorMapModel Control => (DevExtremeVectorMapModel)base.Control;

        public override IList GetSelectedObjects() => _selectedItems;

        public RenderFragment ComponentContent 
            => _componentContent ??= ComponentModelObserver.Create(Control, Control
                .GetComponentContent(o => _mapInstance = (DevExtremeMap)o));

        public override SelectionType SelectionType => SelectionType.Full;

        

        public void Setup(CollectionSourceBase collectionSource, XafApplication application){
            _collectionSource=collectionSource;
        }

        public void ApplyColors<TItem>(TItem[] mapItems,Func<TItem,string> propertySelector) where TItem: class, IMapItem{
            var palette = CreatePalette(mapItems.Select(propertySelector));
            foreach (var item in mapItems){
                item.Color = palette[propertySelector(item)];
            }
        }

        protected virtual void OnCustomizeLayers(CustomizeLayersArgs e) => CustomizeLayers?.Invoke(this, e);

        public static double[] GetBounds<TMapItem>( TMapItem[] mapItems,double[] defaultBounds) where TMapItem:IMapItem
            => !mapItems.Any() ? defaultBounds : 
                new[]{(mapItems.Min(item => item.Longitude) - (mapItems.Max(item => item.Longitude) - mapItems.Min(item => item.Longitude)) * 0.1)}
                    .Concat(new[]{mapItems.Max(item => item.Latitude) + (mapItems.Max(item => item.Latitude) - mapItems.Min(item => item.Latitude)) * 0.1}.AsEnumerable())
                    .Concat(new[]{mapItems.Max(item => item.Longitude) + (mapItems.Max(item => item.Longitude) - mapItems.Min(item => item.Longitude)) * 0.1}.AsEnumerable())
                    .Concat(new[]{mapItems.Min(item => item.Latitude) - (mapItems.Max(item => item.Latitude) - mapItems.Min(item => item.Latitude)) * 0.1}.AsEnumerable()).ToArray();
    }

    public class CustomizeLayersArgs(IMapItem[] mapItems){
        public IMapItem[] MapItems{ get; } = mapItems;
        public List<BaseLayer> Layers{ get; } = new();
    }
}