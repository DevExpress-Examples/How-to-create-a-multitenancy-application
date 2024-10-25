namespace OutlookInspired.Module.Attributes{
    [AttributeUsage(AttributeTargets.Property)]
    public class FontSizeDeltaAttribute(int delta) : Attribute{
        public int Delta{ get; } = delta;

        public string Style(){
            var size = Delta == 8 ? "1.8" : "1.2";
            return $"line-height: {size}rem;font-size: {size}rem";
        }
    }
}