using Microsoft.AspNetCore.Components;

namespace OutlookInspired.Blazor.Server.Components.DxGrid{
    public class DxGridDetailRowModel:DevExpress.ExpressApp.Blazor.Components.Models.ComponentModelBase{
        public override Type ComponentType => typeof(DxGridDetailRow);
        public IEnumerable<(string caption,Type objectType)> Tabs {
            get=>GetPropertyValue<IEnumerable<(string caption,Type objectType)>>()??[];
            set => SetPropertyValue(value);
        }

        public EventCallback<int> ActiveTabIndexChanged{
            get => GetPropertyValue<EventCallback<int>>();
            set => SetPropertyValue(value);
        }
        public RenderFragment RenderFragment{
            get => GetPropertyValue<RenderFragment>();
            set => SetPropertyValue(value);
        }
    }
}