using DevExpress.Persistent.Base;

namespace OutlookInspired.Module.BusinessObjects{
    public enum Period{
        [ImageName("CustomerQuickSales")]
        ThisMonth,
        [ImageName("SalesAnalysis")]
        ThisYear,
        [ImageName("Demo_SalesOverview")]
        Lifetime,
        FixedDate,
    }
}