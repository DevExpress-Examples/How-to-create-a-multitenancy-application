using DevExpress.Blazor;
using Microsoft.AspNetCore.Components;

namespace OutlookInspired.Blazor.Server.Components.DxGrid{
    public class MyDxGridModel:DevExpress.ExpressApp.Blazor.Components.Models.ComponentModelBase{
        public override Type ComponentType => typeof(MyDxGrid);

        public IEnumerable<IGridDataColumn> Columns{
            get=>GetPropertyValue<IEnumerable<IGridDataColumn>>();
            set => SetPropertyValue(value);
        }
        
        public IEnumerable<IGridSummaryItem> SummaryItems {
            get=>GetPropertyValue<IEnumerable<IGridSummaryItem>>();
            set => SetPropertyValue(value);
        }
        
        public IEnumerable<object> Data {
            get=>GetPropertyValue<IEnumerable<object>>();
            set => SetPropertyValue(value);
        }
        
        public bool AllowSelectRowByClick {
            get=>GetPropertyValue<bool>();
            set => SetPropertyValue(value);
        }
    }

    class GridSummaryItemModel:IGridSummaryItem{
        public string Name{ get; set; }
        public string FieldName{ get; set; }
        public GridSummaryItemType SummaryType{ get; set; }
        public string FooterColumnName{ get; set; }
        public string ValueDisplayFormat{ get; set; }
        public string DisplayText{ get; set; }
        public bool Visible{ get; set; }
    }
    class GridDataColumnModel:IGridDataColumn{
        public string Name{ get; set; }
        public string Caption{ get; set; }
        public int VisibleIndex{ get; set; }
        public bool Visible{ get; set; }
        public string Width{ get; set; }
        public int MinWidth{ get; set; }
        public GridTextAlignment TextAlignment{ get; set; }
        public GridTextAlignment? CaptionAlignment{ get; set; }
        public bool ShowInColumnChooser{ get; set; }
        public bool? AllowReorder{ get; set; }
        public GridColumnFixedPosition FixedPosition{ get; set; }
        public RenderFragment<GridColumnHeaderCaptionTemplateContext> HeaderCaptionTemplate{ get; set; }
        public RenderFragment<GridColumnGroupFooterTemplateContext> GroupFooterTemplate{ get; set; }
        public RenderFragment<GridColumnFooterTemplateContext> FooterTemplate{ get; set; }
        public bool ReadOnly{ get; set; }
        public string FieldName{ get; set; }
        public GridUnboundColumnType UnboundType{ get; set; }
        public string UnboundExpression{ get; set; }
        public int SortIndex{ get; set; }
        public GridColumnSortOrder SortOrder{ get; set; }
        public GridColumnSortMode SortMode{ get; set; }
        public int GroupIndex{ get; set; }
        public GridColumnGroupInterval GroupInterval{ get; set; }
        public bool? AllowSort{ get; set; }
        public GridFilterMenuButtonDisplayMode FilterMenuButtonDisplayMode{ get; set; }
        public bool? AllowGroup{ get; set; }
        public string DisplayFormat{ get; set; }
        public object FilterRowValue{ get; set; }
        public GridFilterRowOperatorType FilterRowOperatorType{ get; set; }
        public bool FilterRowEditorVisible{ get; set; }
        public bool DataRowEditorVisible{ get; set; }
        public GridColumnFilterMode FilterMode{ get; set; }
        public bool SearchEnabled{ get; set; }
        public bool ExportEnabled{ get; set; }
        public int ExportWidth{ get; set; }
        public RenderFragment EditSettings{ get; set; }
        public RenderFragment<GridDataColumnFilterRowCellTemplateContext> FilterRowCellTemplate{ get; set; }
        public RenderFragment<GridDataColumnGroupRowTemplateContext> GroupRowTemplate{ get; set; }
        public RenderFragment<GridDataColumnCellDisplayTemplateContext> CellDisplayTemplate{ get; set; }
        public RenderFragment<GridDataColumnCellEditTemplateContext> CellEditTemplate{ get; set; }
        public RenderFragment<GridDataColumnFilterMenuTemplateContext> FilterMenuTemplate{ get; set; }
    }
    
}