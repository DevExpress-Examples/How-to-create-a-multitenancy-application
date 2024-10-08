using System.Linq.Expressions;
using OutlookInspired.Module.BusinessObjects;

namespace OutlookInspired.Blazor.Server.Editors.MapItemChart;
public class MapItemDxChartModel : DevExpress.ExpressApp.Blazor.Components.Models.ComponentModelBase {
    public IEnumerable<MapItem> Data {
        get => GetPropertyValue<IEnumerable<MapItem>>();
        set => SetPropertyValue(value);
    }
    public Expression<Func<MapItem, string>> ArgumentField {
        get => GetPropertyValue<Expression<Func<MapItem, String>>>();
        set => SetPropertyValue(value);
    }
    public Expression<Func<MapItem, decimal>> ValueField {
        get => GetPropertyValue<Expression<Func<MapItem, Decimal>>>();
        set => SetPropertyValue(value);
    }
    public Expression<Func<MapItem, string>> NameField {
        get => GetPropertyValue<Expression<Func<MapItem, String>>>();
        set => SetPropertyValue(value);
    }
    public string Height {
        get => GetPropertyValue("70vh");
        set => SetPropertyValue(value);
    }
    public override Type ComponentType => typeof(MapItemDxChart);
}