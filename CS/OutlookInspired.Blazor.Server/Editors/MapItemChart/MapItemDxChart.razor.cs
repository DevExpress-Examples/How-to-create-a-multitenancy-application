using System.Linq.Expressions;
using OutlookInspired.Module.BusinessObjects;

namespace OutlookInspired.Blazor.Server.Editors.MapItemChart;
public class MapItemDxChartModel : DevExpress.ExpressApp.Blazor.Components.Models.ComponentModelBase {
    public IEnumerable<IMapItem> Data {
        get => GetPropertyValue<IEnumerable<MapItem>>();
        set => SetPropertyValue(value);
    }
    public Expression<Func<IMapItem, string>> ArgumentField {
        get => GetPropertyValue<Expression<Func<IMapItem, String>>>();
        set => SetPropertyValue(value);
    }
    public Expression<Func<IMapItem, decimal>> ValueField {
        get => GetPropertyValue<Expression<Func<IMapItem, Decimal>>>();
        set => SetPropertyValue(value);
    }
    public Expression<Func<IMapItem, string>> NameField {
        get => GetPropertyValue<Expression<Func<IMapItem, String>>>();
        set => SetPropertyValue(value);
    }
    public Func<IEnumerable<decimal>, decimal> SummaryMethod  {
        get => GetPropertyValue<Func<IEnumerable<decimal>, decimal>>();
        set => SetPropertyValue(value);
    }
    public string Height {
        get => GetPropertyValue("70vh");
        set => SetPropertyValue(value);
    }
    public override Type ComponentType => typeof(MapItemDxChart);
}