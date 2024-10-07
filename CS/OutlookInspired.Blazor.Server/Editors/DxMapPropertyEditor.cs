using DevExpress.ExpressApp.Blazor.Components.Models;
using DevExpress.ExpressApp.Blazor.Editors;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using OutlookInspired.Blazor.Server.Components.DevExtreme.Maps.DxMap;
using OutlookInspired.Module.BusinessObjects;
using OutlookInspired.Module.Features.Maps;
using EditorAliases = OutlookInspired.Module.Services.EditorAliases;

namespace OutlookInspired.Blazor.Server.Editors{
    [PropertyEditor(typeof(Location),EditorAliases.DxHomeOfficeMapPropertyEditor, false)]
    public class DxMapPropertyEditor(Type objectType, IModelMemberViewItem model) : BlazorPropertyEditorBase(objectType, model){
        protected override IComponentModel CreateComponentModel() => new DxMapModel();
        public override DxMapModel ComponentModel => (DxMapModel)base.ComponentModel;
        protected override void ReadValueCore(){
            base.ReadValueCore();
            ComponentModel.Markers = new Dictionary<string, IMapsMarker>{
                { "Location", (IMapsMarker)View.CurrentObject },
                { "HomeOffice", ((IModelOptionsHomeOffice)Model.Application.Options).HomeOffice }
            };
            ComponentModel.Center=ComponentModel.Markers["Location"];
        }
    }

}