using Microsoft.AspNetCore.Components;
using OutlookInspired.Blazor.Server.Components.DevExtreme.Maps;

namespace OutlookInspired.Blazor.Server.Editors.Maps{
    public class DevExtremeVectorMapModel:DevExpress.ExpressApp.Blazor.Components.Models.ComponentModelBase{
        public override Type ComponentType => typeof(DevExtremeMap);

        public IEnumerable<BaseLayer> Layers{
            get => GetPropertyValue<IEnumerable<BaseLayer>>();
            set => SetPropertyValue(value);
        }
        public double[] Bounds{
            get => GetPropertyValue<double[]>();
            set => SetPropertyValue(value);
        }
        public string[] CustomAttributes{
            get => GetPropertyValue<string[]>();
            set => SetPropertyValue(value);
        }
        public EventCallback<string[]> SelectionChanged{
            get => GetPropertyValue<EventCallback<string[]>>();
            set => SetPropertyValue(value);
        }

        
    }
}