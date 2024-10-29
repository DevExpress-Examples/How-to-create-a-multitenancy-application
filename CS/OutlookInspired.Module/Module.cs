﻿using System.Runtime.CompilerServices;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.Model.Core;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.Updating;
using DevExpress.ExpressApp.ReportsV2;
using OutlookInspired.Module.Controllers;
using OutlookInspired.Module.Features.CloneView;
using OutlookInspired.Module.Features.Customers;
using OutlookInspired.Module.Features.Employees;
using OutlookInspired.Module.Features.Orders;
using OutlookInspired.Module.Features.Products;
using OutlookInspired.Module.Features.Quotes;
using OutlookInspired.Module.ModelUpdaters;
using OutlookInspired.Module.BusinessObjects;
using OutlookInspired.Module.Common;
using OutlookInspired.Module.Features;
using OutlookInspired.Module.Features.Maps;
using OutlookInspired.Module.Features.Reports;
using OutlookInspired.Module.Resources.Reports;
using CustomerProfile = OutlookInspired.Module.Resources.Reports.CustomerProfile;
using ProductProfile = OutlookInspired.Module.Resources.Reports.ProductProfile;


[assembly:InternalsVisibleTo("OutlookInspired.Win")]
[assembly:InternalsVisibleTo("OutlookInspired.Blazor.Server")]
namespace OutlookInspired.Module;
public sealed class OutlookInspiredModule : ModuleBase{
	public const string ModelCategory = "OutlookInspired";
	public const string RevenueReport = "Revenue Report";
	public const string RevenueAnalysis = "Revenue Analysis";
	public const string Contacts = "Contacts";
	public const string LocationsReport = "Locations";
	public const string SalesSummaryReport = "Sales Summary Report";
	public const string CustomerProfile = "Profile";
	public const string OrdersReport = "Orders";
	public const string ProductProfile = "Profile";
	public const string Sales = "Sales";
	public const string TopSalesPerson = "Top Sales Person";
	public const string FedExGroundLabel = nameof(FedExGroundLabel);
	
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
	    var predefinedReportsUpdater = new PredefinedReportsUpdater(Application, objectSpace, versionFromDB);
	    AddOrderReports(predefinedReportsUpdater);
	    AddCustomerReports(predefinedReportsUpdater);
	    AddProductReports(predefinedReportsUpdater);
	    yield return predefinedReportsUpdater;
	    yield return new DatabaseUpdate.Updater(objectSpace, versionFromDB);
    }
    void AddOrderReports(PredefinedReportsUpdater predefinedReportsUpdater){
	    predefinedReportsUpdater.AddPredefinedReport<FedExGroundLabel>(FedExGroundLabel, typeof(Order));
	    predefinedReportsUpdater.AddPredefinedReport<SalesRevenueReport>(RevenueReport, typeof(Order));
	    predefinedReportsUpdater.AddPredefinedReport<SalesRevenueAnalysisReport>(RevenueAnalysis, typeof(Order));
    }

    PredefinedReportsUpdater AddCustomerReports( PredefinedReportsUpdater predefinedReportsUpdater){
	    predefinedReportsUpdater.AddPredefinedReport<CustomerContactsDirectory>(Contacts, typeof(Customer));
	    predefinedReportsUpdater.AddPredefinedReport<CustomerLocationsDirectory>(LocationsReport, typeof(Customer));
	    predefinedReportsUpdater.AddPredefinedReport<CustomerSalesSummaryReport>(SalesSummaryReport, typeof(Customer));
	    predefinedReportsUpdater.AddPredefinedReport<CustomerProfile>(CustomerProfile, typeof(Customer));
	    return predefinedReportsUpdater;
    }
    void AddProductReports(PredefinedReportsUpdater predefinedReportsUpdater){
	    predefinedReportsUpdater.AddPredefinedReport<ProductOrders>(OrdersReport, typeof(Product));
	    predefinedReportsUpdater.AddPredefinedReport<ProductProfile>(ProductProfile, typeof(Product));
	    predefinedReportsUpdater.AddPredefinedReport<ProductSalesSummary>(Sales, typeof(Product));
	    predefinedReportsUpdater.AddPredefinedReport<ProductTopSalesperson>(TopSalesPerson, typeof(Product));
    }

    protected override IEnumerable<Type> GetDeclaredControllerTypes() 
	    =>[typeof(MailMergeController),typeof(CustomerReportController),typeof(QuoteMapItemListViewController),typeof(HideToolBarController),
		    typeof(CommunicationController),typeof(FollowUpController),typeof(InvoiceReportDocumentController),typeof(InvoiceController),typeof(PayController),typeof(RefundController),typeof(Features.Orders.OrdersReportController),typeof(ShipmentDetailController),
		    typeof(ProductReportController),typeof(MapOrderController), typeof(ViewFilterController),
		    typeof(MapProductController),typeof(MapCustomerController),typeof(MapEmployeeController),typeof(MapOpportunitiesController),
		    typeof(MapsSalesPeriodViewController),typeof(ProtectReportActionItemsViewController),typeof(OpportunitiesListViewController),
		    typeof(ShowReportController),typeof(QuoteAnalysisListViewController),typeof(OpportunitiesFilterListViewController)
	    ];

    public override void Setup(XafApplication application) {
	    base.Setup(application);
	    application.ObjectSpaceCreated += Application_ObjectSpaceCreated;
    }

	private void Application_ObjectSpaceCreated(object sender, ObjectSpaceCreatedEventArgs e) {
		if(e.ObjectSpace is NonPersistentObjectSpace nonPersistentObjectSpace) {
            nonPersistentObjectSpace.ObjectByKeyGetting += nonPersistentObjectSpace_ObjectByKeyGetting;
        }
		if (e.ObjectSpace is not CompositeObjectSpace { Owner: not CompositeObjectSpace } compositeObjectSpace) return;
		compositeObjectSpace.PopulateAdditionalObjectSpaces((XafApplication)sender);
	}

	public override void ExtendModelInterfaces(ModelInterfaceExtenders extenders){
		base.ExtendModelInterfaces(extenders);
		extenders.Add<IModelOptions,IModelOptionsHomeOffice>();
	}

	public override void AddGeneratorUpdaters(ModelNodesGeneratorUpdaters updaters) {
	    base.AddGeneratorUpdaters(updaters);
	    foreach (var updater in new IModelNodesGeneratorUpdater[]{new CloneViewUpdater(),  new DataAccessModeUpdater(),new NavigationItemsModelUpdater(),new DashboardViewsModelUpdater()}){
		    updaters.Add(updater);    
	    }
	    
    }

    private void nonPersistentObjectSpace_ObjectByKeyGetting(object sender, ObjectByKeyGettingEventArgs e) {
        if (e.ObjectType!=typeof(Welcome)) return;
        e.Object = ((IObjectSpace)sender).CreateObject<Welcome>();
    }
}

