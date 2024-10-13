using System.ComponentModel;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.Office;
using DevExpress.ExpressApp.Templates;
using DevExpress.Map.Native;
using DevExpress.Persistent.Base;
using OutlookInspired.Module.BusinessObjects;
using OutlookInspired.Module.Services.Internal;

namespace OutlookInspired.Module.Features.Maps{
    [Obsolete]
    public abstract class MapsViewController:ObjectViewController<ObjectView,IMapsMarker>,IModelExtender{
        public static readonly string BindKey = Environment.GetEnvironmentVariable("BingKey");
        public const string MapItActionId = "MapIt_old";

        protected MapsViewController(){
            MapItAction = MapIt();
            TravelModeAction = TravelMode();
            ExportMapAction = Export();
            PrintPreviewMapAction = PrintPreview();
            PrintAction = Print();
            SalesPeriodAction=SalesPeriod();
            StageAction=Stage();
        }

        private SingleChoiceAction SalesPeriod() 
            => NewSingleChoiceAction("SalesPeriod_old","Period", Enum.GetValues<Period>().Where(period => period!=Period.FixedDate)
                    .Select(period => new ChoiceActionItem(period.ToString(), period){ImageName = period.ImageName()}).ToArray());
        private SingleChoiceAction Stage() 
            => NewSingleChoiceAction("Stage_old",Enum.GetValues<Stage>().Where(stage => stage!=BusinessObjects.Stage.Summary)
                    .Select(stage => new ChoiceActionItem(stage.ToString(), stage){ImageName = stage.ImageName()}).ToArray());

        private SimpleAction Print() 
            => new(this,"MapPrint",PopupActionsCategory()){ImageName = "Print"};

        private SimpleAction PrintPreview() 
            => new(this,"MapPrintPreview",PopupActionsCategory()){ImageName = "PrintPreview"};

        private SimpleAction Export() 
            => new(this,"MapExport",PopupActionsCategory()){ImageName = "Export"};

        private SingleChoiceAction TravelMode() 
            => NewSingleChoiceAction("TravelMode_ols", new ChoiceActionItem("Driving", "Driving"){ ImageName = "Driving" },
                new ChoiceActionItem("Walking", "Walking"){ ImageName = "Walking" });

        protected abstract PredefinedCategory PopupActionsCategory();

        private SimpleAction MapIt(){
            var action = new SimpleAction(this, MapItActionId, PredefinedCategory.View){
                SelectionDependencyType = SelectionDependencyType.RequireSingleObject, ImageName = "MapIt",
                PaintStyle = ActionItemPaintStyle.Image
            };
            action.Executed+=(_, e) => {
                e.NewDetailView(GetViewId(), TargetWindow.NewModalWindow);
                Configure(e.ShowViewParameters);
            };
            return action;
        }

        protected virtual void Configure(ShowViewParameters parameters){
            
        }

        private SingleChoiceAction NewSingleChoiceAction(string actionId,string caption, params ChoiceActionItem[] items){
            var action = new SingleChoiceAction(this, actionId, PopupActionsCategory().ToString()){
                ItemType = SingleChoiceActionItemType.ItemIsOperation, PaintStyle = ActionItemPaintStyle.Image,
                ImageMode = ImageMode.UseItemImage,
                DefaultItemMode = DefaultItemMode.LastExecutedItem, TargetViewType = ViewType.DetailView,
                Active ={ [nameof(MapsViewController)] = false },
            };
            action.Items.AddRange(items);
            action.SelectedIndex = 0;
            if (caption != null){
                action.Caption = caption;
            }
            return action;
        }

        private SingleChoiceAction NewSingleChoiceAction(string actionId,params ChoiceActionItem[] items) 
            => NewSingleChoiceAction(actionId, null, items);

        private string GetViewId() 
            => View.ObjectTypeInfo.Type switch{
                { } t when t == typeof(Employee) => Employee.MapsDetailView,
                { } t when t == typeof(Customer) => Customer.MapsDetailView,
                { } t when t == typeof(Product) => Product.MapsDetailView,
                { } t when t == typeof(Order) => Order.MapsDetailView,
                { } t when t == typeof(Quote) => Quote.MapsDetailView,
                _ => throw new NotImplementedException(View.ObjectTypeInfo.Type.Name)
            };

        public SingleChoiceAction StageAction{ get;  }
        public SingleChoiceAction SalesPeriodAction{ get; }
        public SimpleAction PrintAction{ get;  }
        public SimpleAction PrintPreviewMapAction{ get;  }
        public SingleChoiceAction TravelModeAction{ get; }
        public SimpleAction MapItAction{ get; }
        public SimpleAction ExportMapAction{ get; }

        protected override void OnActivated(){
            base.OnActivated();
            ChangeMapItAction(typeof(ISalesMapsMarker),"Sales Map");
            ChangeMapItAction(typeof(Order),"Shipping Map");
            ChangeMapItAction(typeof(Quote),"Opportunities Map");
            MapItAction.Active[nameof(MapsViewController)] = Frame is NestedFrame&&Frame.View.IsRoot;
            if (typeof(ISalesMapsMarker).IsAssignableFrom(View.ObjectTypeInfo.Type)){
                MapItAction.Active[nameof(ISalesMapsMarker)] = Application.CanRead(typeof(OrderItem));    
            }
            TravelModeAction.Active[nameof(MapsViewController)] = View.ObjectTypeInfo.Type==typeof(Employee);
            TravelModeAction.Active[nameof(MapItAction)] =!MapItAction.Active&& Frame.Context==FrameContext()&&!Frame.View.IsRoot;
            if (View.Id==Employee.MapsDetailView){
                Frame.GetController<RichTextShowInDocumentControllerBase>().ShowInDocumentAction
                    .Active[nameof(TravelModeAction)] = false;    
            }
            
            SalesPeriodAction.Active[nameof(MapsViewController)]= typeof(ISalesMapsMarker).IsAssignableFrom(View.ObjectTypeInfo.Type);
            SalesPeriodAction.Active[nameof(MapItAction)] =TravelModeAction.Active[nameof(MapItAction)];
            StageAction.Active[nameof(MapsViewController)]= typeof(Quote).IsAssignableFrom(View.ObjectTypeInfo.Type);
            StageAction.Active[nameof(MapItAction)] =TravelModeAction.Active[nameof(MapItAction)];
            ExportMapAction.Active[nameof(MapsViewController)] = TravelModeAction.Active[nameof(MapItAction)] ||
                                                                 SalesPeriodAction.Active[nameof(MapItAction)] ||
                                                                 StageAction.Active[nameof(MapItAction)];
            PrintAction.Active[nameof(MapsViewController)] =ExportMapAction.Active;
            PrintPreviewMapAction.Active[nameof(MapsViewController)] =ExportMapAction.Active;
            
        }

        protected abstract string FrameContext();

        private void ChangeMapItAction(Type objectType,string caption){
            if (objectType.IsAssignableFrom(View.ObjectTypeInfo.Type)){
                MapItAction.Caption = caption;
                MapItAction.ToolTip = MapItAction.Caption;
                MapItAction.Active.RemoveItem(ActionBase.RequireSingleObjectContext);
            }
        }

        public void ExtendModelInterfaces(ModelInterfaceExtenders extenders) 
            => extenders.Add<IModelOptions,IModelOptionsHomeOffice>();
    }

    public interface IModelOptionsHomeOffice{
        IModelHomeOffice HomeOffice{ get; }    
    }
    
    public interface IModelHomeOffice:IMapsMarker,IModelNode{
        [DefaultValue("Glendale")]
        string City{ get; set; }
        [DefaultValue("91203")]
        string ZipCode{ get; set; }
        [DefaultValue("505 N. Brand Blvd")]
        string Line{ get; set; }
        [DefaultValue(StateEnum.CA)]
        StateEnum State{ get; set; }
        [DefaultValue(34.1532866)]
        new double Latitude{ get; set; }
        [DefaultValue(-118.2555815)]
        new double Longitude{ get; set; }
    }

    public interface IMapsRouteController{
        event EventHandler<RouteCalculatedArgs> RouteCalculated;
    }

    public class RouteCalculatedArgs:EventArgs{
        public RoutePoint[] RoutePoints{ get; }

        public RouteCalculatedArgs(RoutePoint[] routePoints,double distance,TimeSpan time,TravelMode travelMode){
            RoutePoints = routePoints;
            Distance = distance;
            Time = time;
            TravelMode = travelMode;
        }

        public double Distance{ get;  }
        public TimeSpan Time{ get;  }
        public TravelMode TravelMode{ get; }
    }
}