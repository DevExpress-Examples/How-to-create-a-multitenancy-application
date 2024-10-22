using Microsoft.AspNetCore.Components;

namespace OutlookInspired.Blazor.Server.Editors.LayoutView{

    public class LayoutViewModel:DevExpress.ExpressApp.Blazor.Components.Models.ComponentModelBase{
        public Func<object,byte[]> ImageSelector{
            get => GetPropertyValue<Func<object,byte[]>>();
            set => SetPropertyValue(value);
        }
        public EventCallback<object> SelectionChanged{
            get => GetPropertyValue<EventCallback<object>>();
            set => SetPropertyValue(value);
        }
        public EventCallback ProcessSelectedObject{
            get => GetPropertyValue<EventCallback>();
            set => SetPropertyValue(value);
        }
        public Func<object,string> HeaderSelector{
            get => GetPropertyValue<Func<object,string>>();
            set => SetPropertyValue(value);
        }
        public Func<object,string> FooterSelector{
            get => GetPropertyValue<Func<object,string>>();
            set => SetPropertyValue(value);
        }
        public Func<object,Dictionary<string, string>> InfoItemsSelector{
            get => GetPropertyValue<Func<object,Dictionary<string, string>>>();
            set => SetPropertyValue(value);
        }
        
        public IEnumerable<object> Data{
            get => GetPropertyValue<IEnumerable<object>>();
            set => SetPropertyValue(value);
        }

        public override Type ComponentType => typeof(LayoutView);
    }
}