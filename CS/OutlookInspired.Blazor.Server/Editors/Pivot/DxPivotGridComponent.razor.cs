using DevExpress.Blazor;
using DevExpress.Blazor.Internal;
using DevExpress.Blazor.PivotGrid.Internal;

namespace OutlookInspired.Blazor.Server.Editors.Pivot;
public interface IDxPivotGridModel{
    IEnumerable<object> Data{ get; set; }
    IEnumerable<IPivotField> Fields{ get; set; }
    bool ExpandAllRows{ get; set; }
}

public class DxPivotGridModel : DevExpress.ExpressApp.Blazor.Components.Models.ComponentModelBase, IDxPivotGridModel{
    public IEnumerable<object> Data {
        get => GetPropertyValue<IEnumerable<object>>()??Enumerable.Empty<object>();
        set => SetPropertyValue(value);
    }
    public IEnumerable<IPivotField> Fields {
        get => GetPropertyValue<IEnumerable<IPivotField>>();
        set => SetPropertyValue(value);
    }
    
    public bool ExpandAllRows {
        get => GetPropertyValue<bool>();
        set => SetPropertyValue(value);
    }
    public override Type ComponentType => typeof(DxPivotGridComponent);
}

public interface IPivotField{
    string Name{ get; }
    PivotGridSortOrder SortOrder{ get; }
    PivotGridFieldArea Area{ get; }
    PivotGridGroupInterval GroupInterval{ get; }
    string Caption{ get; }
    PivotGridSummaryType SummaryType{ get; }
    string DisplayFormat{ get;  }
    bool IsProgressBar{ get;  }
    
}

public class MyPivotGrid<T> : DxPivotGrid<T>{
    public new MyPivotGridModel<T> Model => (MyPivotGridModel<T>)base.Model;
    protected override DevExpress.Blazor.Base.PivotGridModelBase CreateModelCore() => new MyPivotGridModel<T>(this);
}

public class MyPivotGridModel<T>(DxPivotGrid<T> pivotGridComponent) : PivotGridModel<T>(pivotGridComponent){
    public new void Update() => base.Update();
    public new PivotGridState<T> State => base.State;
}