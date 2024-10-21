using System.Linq.Expressions;
using DevExpress.Blazor;

namespace OutlookInspired.Blazor.Server.Editors.Charts;
public interface IDxChartModel{
    ChartSeriesType SeriesType{ get; set; }
    IEnumerable<object> Data{ get; set; }
    Expression<Func<object, string>> ArgumentField{ get; set; }
    Expression<Func<object, decimal>> ValueField{ get; set; }
    Expression<Func<object, string>> NameField{ get; set; }
    Func<IEnumerable<decimal>, decimal> SummaryMethod{ get; set; }
    Action<ChartSeriesPointCustomizationSettings> CustomizeSeriesPoint { get; set; }
    string Height{ get; set; }
}

public class DxChartModel : DevExpress.ExpressApp.Blazor.Components.Models.ComponentModelBase,IDxChartModel {
    public ChartSeriesType SeriesType{
        get => GetPropertyValue<ChartSeriesType>();
        set => SetPropertyValue(value);
    }

    public IEnumerable<object> Data {
        get => GetPropertyValue<IEnumerable<object>>();
        set => SetPropertyValue(value);
    }
    public Expression<Func<object, string>> ArgumentField {
        get => GetPropertyValue<Expression<Func<object, String>>>();
        set => SetPropertyValue(value);
    }
    public Expression<Func<object, decimal>> ValueField {
        get => GetPropertyValue<Expression<Func<object, Decimal>>>();
        set => SetPropertyValue(value);
    }
    public Expression<Func<object, string>> NameField {
        get => GetPropertyValue<Expression<Func<object, String>>>();
        set => SetPropertyValue(value);
    }
    public Func<IEnumerable<decimal>, decimal> SummaryMethod  {
        get => GetPropertyValue<Func<IEnumerable<decimal>, decimal>>();
        set => SetPropertyValue(value);
    }

    public Action<ChartSeriesPointCustomizationSettings> CustomizeSeriesPoint{
        get => GetPropertyValue<Action<ChartSeriesPointCustomizationSettings>>();
        set => SetPropertyValue(value);
    }

    public string Height {
        get => GetPropertyValue("70vh");
        set => SetPropertyValue(value);
    }
    
    public override Type ComponentType => typeof(MyDxChart);
}

