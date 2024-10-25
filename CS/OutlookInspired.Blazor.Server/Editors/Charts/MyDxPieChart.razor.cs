namespace OutlookInspired.Blazor.Server.Editors.Charts;

public class DxChartPieModel : DxChartModel{
    
    public override Type ComponentType => typeof(MyDxPieChart);

    public bool Stick{
        get => GetPropertyValue<bool>();
        set => SetPropertyValue(value);
    }
}