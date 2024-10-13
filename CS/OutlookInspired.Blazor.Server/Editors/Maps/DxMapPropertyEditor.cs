using DevExpress.ExpressApp.Blazor.Components.Models;
using DevExpress.ExpressApp.Blazor.Editors;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using Microsoft.AspNetCore.Components;
using OutlookInspired.Blazor.Server.Components.DevExtreme.Maps.DxMap;
using OutlookInspired.Module.BusinessObjects;
using OutlookInspired.Module.Features.Maps;
using EditorAliases = OutlookInspired.Module.Services.EditorAliases;

namespace OutlookInspired.Blazor.Server.Editors.Maps{
    [PropertyEditor(typeof(Location),EditorAliases.MapPropertyEditor, false)]
    public class DxMapPropertyEditor(Type objectType, IModelMemberViewItem model) : BlazorPropertyEditorBase(objectType, model){
        public event EventHandler<RouteCalculatedArgs> RouteCalculated;
        protected override IComponentModel CreateComponentModel() => new DxMapModel();
        public override DxMapModel ComponentModel => (DxMapModel)base.ComponentModel;
        protected override void ReadValueCore(){
            base.ReadValueCore();
            var modelHomeOffice = ((IModelOptionsHomeOffice)Model.Application.Options).HomeOffice;
            ComponentModel.Markers = new Dictionary<string, IMapsMarker>{
                { "Location", (IMapsMarker)View.CurrentObject },
                { "HomeOffice", new MapsMarker("Home Office",modelHomeOffice.Latitude, modelHomeOffice.Longitude) }
            };
            ComponentModel.Center=ComponentModel.Markers["Location"];
            ComponentModel.CalculateRoute = RouteCalculated != null;
            ComponentModel.RouteCalculated=EventCallback.Factory.Create<RouteCalculatedArgs>(this,OnRouteCalculated);
        }

        protected virtual void OnRouteCalculated(RouteCalculatedArgs e) => RouteCalculated?.Invoke(this, e);
    }

}