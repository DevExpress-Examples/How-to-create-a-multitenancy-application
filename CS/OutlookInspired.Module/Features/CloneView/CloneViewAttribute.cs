namespace OutlookInspired.Module.Features.CloneView{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
    public class CloneViewAttribute(CloneViewType viewType, string viewId) : Attribute{
        public string ViewId{ get; } = viewId;
        public CloneViewType ViewType{ get; } = viewType;
        public string DetailView{ get; set; }
    }
    public enum CloneViewType{
        DetailView,
        ListView,
        LookupListView
    }

}