using DevExpress.ExpressApp.Blazor;
using Microsoft.AspNetCore.Components;
using OutlookInspired.Blazor.Server.Components.DevExtreme.Maps;
using OutlookInspired.Blazor.Server.Services;
using OutlookInspired.Blazor.Server.Services.Internal;

namespace OutlookInspired.Blazor.Server.Features.Maps{
    public class ModalDxrMapModel:DxrMapModel,IComponentContentHolder{
        RenderFragment IComponentContentHolder.ComponentContent => this.Create(model => model.Create<ModalDxMap>());
    }
}