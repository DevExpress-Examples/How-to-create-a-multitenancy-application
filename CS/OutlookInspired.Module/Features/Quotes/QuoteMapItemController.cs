 using DevExpress.ExpressApp;
using OutlookInspired.Module.BusinessObjects;

namespace OutlookInspired.Module.Features.Quotes{
    [Obsolete]
    public class QuoteMapItemController:ViewController<DashboardView>{
        public QuoteMapItemController() => TargetViewId = "Opportunities";
        
        protected override void OnFrameAssigned(){
            base.OnFrameAssigned();
            if (Frame.Context != TemplateContext.ApplicationWindow) return;
            Application.ObjectSpaceCreated-=Application_ObjectSpaceCreated;
            Application.ObjectSpaceCreated += Application_ObjectSpaceCreated;
        }

        private void Application_ObjectSpaceCreated(object sender, ObjectSpaceCreatedEventArgs e){
            // if (e.ObjectSpace is not NonPersistentObjectSpace objectSpace) return;
            // objectSpace.ObjectsGetting+= (o, args) => {
            //     if (args.ObjectType != typeof(QuoteMapItem)) return;
            //     args.Objects = ((NonPersistentObjectSpace)o)!.Opportunities(args.Criteria?.ToString()).ToBindingList();
            // };
        }

    }
}