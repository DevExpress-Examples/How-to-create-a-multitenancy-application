namespace OutlookInspired.Blazor.Server.Editors.LayoutViewStacked{

    public class StackedLayoutViewModel:DevExpress.ExpressApp.Blazor.Components.Models.ComponentModelBase{
        public Func<object,byte[]> ImageSelector{
            get => GetPropertyValue<Func<object,byte[]>>();
            set => SetPropertyValue(value);
        }
        
        public Func<object,string> BodySelector{
            get => GetPropertyValue<Func<object,String>>();
            set => SetPropertyValue(value);
        }
        public IEnumerable<object> Data{
            get => GetPropertyValue<IEnumerable<object>>();
            set => SetPropertyValue(value);
        }

        public override Type ComponentType => typeof(StackedLayoutView);
    }
}