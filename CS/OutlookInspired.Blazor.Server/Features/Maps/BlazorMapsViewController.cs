using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Layout;
using DevExpress.Persistent.Base;
using Microsoft.AspNetCore.Components;
using OutlookInspired.Blazor.Server.Components.DevExtreme.Maps;

namespace OutlookInspired.Blazor.Server.Features.Maps{
    [Obsolete]
    public abstract class BlazorMapsViewController<TMapsMarker, TMapModel, TComponent> : ObjectViewController<DetailView, TMapsMarker>
        where TMapsMarker : IMapsMarker where TMapModel : MapModel<TComponent> where TComponent : ComponentBase{
        public Module.Features.Maps.MapsViewController MapsViewController{ get; private set; }

        public TMapModel Model => (TMapModel)View.GetItems<ControlViewItem>().First().Control;
        protected override void OnDeactivated(){
            base.OnDeactivated();
            if (!Active)return;
            Frame.GetController<Module.Features.Maps.MapsViewController>().PrintAction.Executed-=PrintActionOnExecuted;
        }

        protected override void OnActivated(){
            base.OnActivated();
            if (!(Active[nameof(NestedFrame)] = Frame is not NestedFrame&&View.CurrentObject!=null))return;
            MapsViewController = Frame.GetController<Module.Features.Maps.MapsViewController>();
            MapsViewController.PrintAction.Executed+=PrintActionOnExecuted;
            View.CustomizeViewItemControl<ControlViewItem>(this, item => {
                if (item.Control is not TMapModel model) return;
                CustomizeModel(model);
            });
        }

        protected abstract TMapModel CustomizeModel(TMapModel model);
        
        protected TMapModel CustomizeModel() => CustomizeModel(Model);

        private void PrintActionOnExecuted(object sender, ActionBaseEventArgs e) 
            => ((MapModel<TComponent>)View.GetItems<ControlViewItem>().First().Control).PrintMap = true;

    }
}