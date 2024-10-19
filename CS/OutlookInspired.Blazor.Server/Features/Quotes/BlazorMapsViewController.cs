using DevExpress.ExpressApp.Actions;
using OutlookInspired.Blazor.Server.Components.DevExtreme.Maps;
using OutlookInspired.Blazor.Server.Features.Maps;
using OutlookInspired.Blazor.Server.Services.Internal;
using OutlookInspired.Module.BusinessObjects;
using OutlookInspired.Module.Services.Internal;
using MapsViewController = OutlookInspired.Module.Features.Maps.MapsViewController;

namespace OutlookInspired.Blazor.Server.Features.Quotes{
    [Obsolete]
    public class BlazorMapsViewController:BlazorMapsViewController<Quote,DxVectorMapModel,DxVectorMap>{
        protected override void OnDeactivated(){
            base.OnDeactivated();
            // Frame.GetController<MapsViewController>().StageAction.Executed-=StageActionOnExecuted;
        }

        protected override void OnActivated(){
            base.OnActivated();
            // Frame.GetController<MapsViewController>().StageAction.Executed+=StageActionOnExecuted;
        }

        private void StageActionOnExecuted(object sender, ActionBaseEventArgs e) => CustomizeModel().Redraw=true;

        protected override DxVectorMapModel CustomizeModel(DxVectorMapModel model){
            var stage = (Stage)Frame.GetController<MapsViewController>().StageAction.SelectedItem.Data;
            var mapItems = ObjectSpace.Opportunities(stage,Criteria).ToArray();
            model.Options = mapItems.VectorMapOptions<QuoteMapItem, BubbleLayer>(Palette.Where(t => t.stage==stage).Select(t => t.color).ToArray(),
                items => items.Sum(item => item.Total).YieldItem().Select(arg => arg.RoundNumber()).ToList());
            var quoteMapItem = mapItems.FirstOrDefault();
            if (quoteMapItem != null){
                model.Options.Annotations.Add(new Annotation(){
                    Coordinates = new[]{ quoteMapItem.Longitude,quoteMapItem.Latitude },
                    Data = ObjectSpace.OpportunityCallout(quoteMapItem)
                });    
            }
            return model;
        }

        public (string color, Stage stage)[] Palette{ get; set; }
        public string Criteria{ get; set; }
    }
}