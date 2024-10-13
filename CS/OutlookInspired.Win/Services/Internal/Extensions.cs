using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Win.Editors;
using DevExpress.Map.Dashboard;
using DevExpress.Persistent.Base;
using DevExpress.Utils.Extensions;
using DevExpress.XtraCharts;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraMap;
using DevExpress.XtraPivotGrid;
using OutlookInspired.Module.Attributes;
using OutlookInspired.Module.Services.Internal;
using IColorizer = DevExpress.XtraCharts.IColorizer;
using KeyColorColorizer = DevExpress.XtraMap.KeyColorColorizer;

namespace OutlookInspired.Win.Services.Internal{
    internal static class Extensions{
        public static void ProtectDetailViews(this XafApplication application, GridControl gridControl,Type objectType) 
            => gridControl.LevelTree.Nodes.ToArray()
                .Where(node => !application.CanRead(application.TypesInfo.FindTypeInfo(objectType).FindMember(node.RelationName).ListElementType))
                .Do(node => gridControl.LevelTree.Nodes.Remove(node))
                .Enumerate();

        public static ChartControl ApplyColors(this ChartControl chartControl,KeyColorColorizer colorizer){
            colorizer.Colors.Clear();
            colorizer.Colors.BeginUpdate();
            chartControl.GetPaletteEntries(20).ForEach(entry => colorizer.Colors.Add(entry.Color));
            colorizer.Colors.EndUpdate();
            chartControl.Series[0].View.Colorizer = (IColorizer)colorizer;
            return chartControl;
        }
        
        
        public static void To(this IZoomToRegionService zoomService, GeoPoint pointA, GeoPoint pointB, double margin = 0.2){
            if (pointA == null || pointB == null || zoomService == null) return;
            var (latDiff, longDiff) = (pointB.Latitude - pointA.Latitude, pointB.Longitude - pointA.Longitude);
            var (latPad, longPad) = (margin.CalculatePadding(latDiff), margin.CalculatePadding(longDiff));
            zoomService.ZoomToRegion(new GeoPoint(pointA.Latitude - latPad, pointA.Longitude - longPad),
                new GeoPoint(pointB.Latitude + latPad, pointB.Longitude + longPad),
                new GeoPoint((pointA.Latitude + pointB.Latitude) / 2, (pointA.Longitude + pointB.Longitude) / 2));
        }

        [Obsolete]
        public static IZoomToRegionService Zoom(this MapControl mapControl) 
            => (IZoomToRegionService)((IServiceProvider)mapControl).GetService(typeof(IZoomToRegionService));

        public static void To(this IZoomToRegionService zoomService, IEnumerable<IMapsMarker> mapsMarkers, double margin = 0.25){
            var points = mapsMarkers.Select(m => m.ToGeoPoint()).Where(p => p != null && !Equals(p, new GeoPoint(0, 0))).ToList();
            if (!points.Any()) return;
            zoomService.To(new GeoPoint(points.Min(p => p.Latitude), points.Min(p => p.Longitude)),
                new GeoPoint(points.Max(p => p.Latitude), points.Max(p => p.Longitude)), margin);
        }
        
        [Obsolete]
        static double CalculatePadding(this double margin,double delta) 
            => delta > 0 ? Math.Max(0.1, delta * margin) : delta < 0 ? Math.Min(-0.1, delta * margin) : 0;

        [Obsolete]
        public static GeoPoint ToGeoPoint(this IMapsMarker mapsMarker) 
            => new(mapsMarker.Latitude, mapsMarker.Longitude);
        
        

        public static Dictionary<PivotGridField, RepositoryItem> AddRepositoryItems(this PivotGridControl pivotGridControl,ListView view) 
            => view.Model.Columns.Where(column => column.Index>=0)
                .Select(column => {
                    var pivotGridField = pivotGridControl.Fields[column.ModelMember.Name];
                    return pivotGridField != null && typeof(IInplaceEditSupport).IsAssignableFrom(column.PropertyEditorType)
                        ? (pivotGridField, repositoryItem: ((IInplaceEditSupport)column.NewPropertyEditor()).CreateRepositoryItem()) : default;
                })
                .WhereNotDefault()
                .Do(t => pivotGridControl.RepositoryItems.Add(t.repositoryItem))
                .ToDictionary(t => t.pivotGridField, t => t.repositoryItem);
        
        

        
        

    }
}