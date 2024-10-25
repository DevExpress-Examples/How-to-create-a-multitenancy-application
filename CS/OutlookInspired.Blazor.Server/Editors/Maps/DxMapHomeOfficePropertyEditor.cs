using DevExpress.ExpressApp.Blazor.Components.Models;
using DevExpress.ExpressApp.Blazor.Editors;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using OutlookInspired.Module;
using OutlookInspired.Module.BusinessObjects;
using EditorAliases = OutlookInspired.Module.EditorAliases;

namespace OutlookInspired.Blazor.Server.Editors.Maps{
    [PropertyEditor(typeof(IMapsMarker),EditorAliases.MapHomeOfficePropertyEditor, false)]
    public class DxMapHomeOfficePropertyEditor(Type objectType, IModelMemberViewItem model) : BlazorPropertyEditorBase(objectType, model){
        
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
        }
        
    }

}