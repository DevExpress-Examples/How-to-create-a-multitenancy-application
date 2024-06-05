﻿using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Layout;
using OutlookInspired.Module.BusinessObjects;
using OutlookInspired.Module.Features.Maps;
using OutlookInspired.Module.Features.ViewFilter;
using OutlookInspired.Module.Services.Internal;

namespace OutlookInspired.Blazor.Server.Features.Quotes{
    public class PaletteController:ViewController<DashboardView>{
        private (string color, Stage stage)[] _palette;
        public PaletteController() => TargetViewId = "Opportunities";
        protected override void OnViewControlsCreated(){
            base.OnViewControlsCreated();
            View.MasterItem().ControlCreated+=OnMasterItemControlCreated;
            View.ChildItem().ControlCreated+=OncChildItemControlCreated;
        }

        private void OncChildItemControlCreated(object sender, EventArgs e){
            var dashboardViewItem = ((DashboardViewItem)sender);
            dashboardViewItem.ControlCreated-=OnMasterItemControlCreated;
            dashboardViewItem.Frame.View.ToDetailView().GetItems<ControlViewItem>().First().ControlCreated+=OnChartControlCreated;
        }

        private void OnChartControlCreated(object sender, EventArgs e){
            var controlViewItem = ((ControlViewItem)sender);
            controlViewItem.ControlCreated-=OnChartControlCreated;
            _palette = ((DxFunnelModel)controlViewItem.Control).ComponentModel.Options.PaletteData;
        }

        private void OnMasterItemControlCreated(object sender, EventArgs e){
            var dashboardViewItem = ((DashboardViewItem)sender);
            dashboardViewItem.ControlCreated-=OnMasterItemControlCreated;
            dashboardViewItem.Frame.GetController<MapsViewController>().MapItAction.Executed+=MapItActionOnExecuted;
        }
        
        protected override void OnDeactivated(){
            base.OnDeactivated();

            var mapItAction = View.MasterItem()?.Frame?.GetController<MapsViewController>()?.MapItAction;

            if(mapItAction != null) {
                mapItAction.Executed -= MapItActionOnExecuted;
            }            
        }

        private void MapItActionOnExecuted(object sender, ActionBaseEventArgs e){
            var controller = Application.CreateController<BlazorMapsViewController>();
            controller.Palette = _palette;
            e.ShowViewParameters.Controllers.Add(controller);
            if (View.MasterItem().Frame.GetController<ViewFilterController>()
                    .FilterAction.SelectedItem.Data is not Module.BusinessObjects.ViewFilter viewFilter) return;
            controller.Criteria = viewFilter.Criteria;
        }
    }
}