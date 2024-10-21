using System.Runtime.CompilerServices;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Model.Core;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.Updating;
using DevExpress.ExpressApp.ReportsV2;
using OutlookInspired.Module.Controllers;
using OutlookInspired.Module.Features.CloneView;
using OutlookInspired.Module.Features.Customers;
using OutlookInspired.Module.Features.Employees;
using OutlookInspired.Module.Features.MasterDetail;
using OutlookInspired.Module.Features.Orders;
using OutlookInspired.Module.Features.Products;
using OutlookInspired.Module.Features.Quotes;
using OutlookInspired.Module.Features.ViewFilter;
using OutlookInspired.Module.ModelUpdaters;
using OutlookInspired.Module.Services.Internal;
using ReportController = OutlookInspired.Module.Features.Customers.ReportController;
using OutlookInspired.Module.BusinessObjects;


[assembly:InternalsVisibleTo("OutlookInspired.Win")]
[assembly:InternalsVisibleTo("OutlookInspired.Blazor.Server")]
namespace OutlookInspired.Module;
public sealed class OutlookInspiredModule : ModuleBase{
	public const string ModelCategory = "OutlookInspired";
    public OutlookInspiredModule() {
		RequiredModuleTypes.Add(typeof(DevExpress.ExpressApp.SystemModule.SystemModule));
		RequiredModuleTypes.Add(typeof(DevExpress.ExpressApp.Security.SecurityModule));
		RequiredModuleTypes.Add(typeof(DevExpress.ExpressApp.Objects.BusinessClassLibraryCustomizationModule));
		RequiredModuleTypes.Add(typeof(DevExpress.ExpressApp.Chart.ChartModule));
		RequiredModuleTypes.Add(typeof(DevExpress.ExpressApp.ConditionalAppearance.ConditionalAppearanceModule));
		RequiredModuleTypes.Add(typeof(DevExpress.ExpressApp.Dashboards.DashboardsModule));
		RequiredModuleTypes.Add(typeof(DevExpress.ExpressApp.Notifications.NotificationsModule));
		RequiredModuleTypes.Add(typeof(DevExpress.ExpressApp.Office.OfficeModule));
		RequiredModuleTypes.Add(typeof(DevExpress.ExpressApp.PivotChart.PivotChartModuleBase));
		RequiredModuleTypes.Add(typeof(DevExpress.ExpressApp.PivotGrid.PivotGridModule));
		RequiredModuleTypes.Add(typeof(ReportsModuleV2));
		RequiredModuleTypes.Add(typeof(DevExpress.ExpressApp.Scheduler.SchedulerModuleBase));
		RequiredModuleTypes.Add(typeof(DevExpress.ExpressApp.TreeListEditors.TreeListEditorsModuleBase));
		RequiredModuleTypes.Add(typeof(DevExpress.ExpressApp.Validation.ValidationModule));
		RequiredModuleTypes.Add(typeof(DevExpress.ExpressApp.ViewVariantsModule.ViewVariantsModule));
		DevExpress.ExpressApp.Security.SecurityModule.UsedExportedTypes = UsedExportedTypes.Custom;
    }
    
    public override IEnumerable<ModuleUpdater> GetModuleUpdaters(IObjectSpace objectSpace, Version versionFromDB) {
	    yield return new PredefinedReportsUpdater(Application, objectSpace, versionFromDB)
		    .AddOrderReports().AddCustomerReports().AddProductReports();
	    yield return new DatabaseUpdate.Updater(objectSpace, versionFromDB);
    }

    protected override IEnumerable<Type> GetDeclaredControllerTypes() 
	    =>[typeof(MailMergeController),typeof(ReportController),typeof(QuoteMapItemController),typeof(HideToolBarController),
		    typeof(CommunicationController),typeof(FollowUpController),typeof(InvoiceReportDocumentController),typeof(InvoiceController),typeof(PayController),typeof(RefundController),typeof(Features.Orders.ReportController),typeof(ShipmentDetailController),
		    typeof(Features.Products.ReportController),typeof(MapOrderController), typeof(MasterDetailController),typeof(ViewFilterController),
		    typeof(MapProductController),typeof(MapCustomerController),typeof(MapEmployeeController),typeof(MapOpportunitiesController)
	    ];

    public override void Setup(XafApplication application) {
	    base.Setup(application);
	    application.ObjectSpaceCreated += Application_ObjectSpaceCreated;
    }

	private void Application_ObjectSpaceCreated(object sender, ObjectSpaceCreatedEventArgs e) {
		if(e.ObjectSpace is NonPersistentObjectSpace nonPersistentObjectSpace) {
            nonPersistentObjectSpace.ObjectByKeyGetting += nonPersistentObjectSpace_ObjectByKeyGetting;
            nonPersistentObjectSpace.ObjectsGetting+=NonPersistentObjectSpaceOnObjectsGetting;
        }
		if (e.ObjectSpace is not CompositeObjectSpace { Owner: not CompositeObjectSpace } compositeObjectSpace) return;
		compositeObjectSpace.PopulateAdditionalObjectSpaces((XafApplication)sender);
	}

	[Obsolete]
	private void NonPersistentObjectSpaceOnObjectsGetting(object sender, ObjectsGettingEventArgs e) {
		// if (e.ObjectType == typeof(QuoteMapItem)){
		// 	e.Objects=Enum.GetValues<Stage>().Where(stage1 => stage1!=Stage.Summary)
		// 		.SelectMany(stage => NewQuoteMapItem(stage, (IObjectSpace)sender, Opportunity.Map.GetValueOrDefault(stage, (0.0, 0.12))))
		// 		.ToArray()
		// 		;
		// }
		//  if (e.ObjectType == typeof(Opportunity)){
		// 	e.Objects = Enum.GetValues<Stage>().Where(stage1 => stage1!=Stage.Summary)
		// 		.Select(stage => NewOpportunity(stage, (IObjectSpace)sender, Map.GetValueOrDefault(stage, (0.0, 0.12))))
		// 		.Select((item, i) => {
		// 			item.ID = i;
		// 			return item;
		// 		}).ToList();
		// }
	}




	public override void AddGeneratorUpdaters(ModelNodesGeneratorUpdaters updaters) {
	    base.AddGeneratorUpdaters(updaters);
	    updaters.Add(new CloneViewUpdater(),  new DataAccessModeUpdater(),new NavigationItemsModelUpdater(),new DashboardViewsModelUpdater());
    }

    private void nonPersistentObjectSpace_ObjectByKeyGetting(object sender, ObjectByKeyGettingEventArgs e) {
        if (e.ObjectType!=typeof(Welcome)) return;
        e.Object = ((IObjectSpace)sender).CreateObject<Welcome>();
    }
}

