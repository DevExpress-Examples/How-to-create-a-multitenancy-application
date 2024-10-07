using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using DevExpress.Blazor;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.ApplicationBuilder;
using DevExpress.ExpressApp.MultiTenancy;
using DevExpress.ExpressApp.ReportsV2.Win;
using DevExpress.ExpressApp.Scheduler.Win;
using DevExpress.ExpressApp.Security;
using DevExpress.ExpressApp.Win;
using DevExpress.ExpressApp.Win.ApplicationBuilder;
using DevExpress.ExpressApp.Win.Utils;
using DevExpress.Persistent.BaseImpl.EF;
using DevExpress.Persistent.BaseImpl.EF.PermissionPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OutlookInspired.Module.BusinessObjects;
using OutlookInspired.Module.Services.Internal;

namespace OutlookInspired.Win.Services{
    public static class ApplicationBuilder{
        public static IWinApplicationBuilder Configure(this IWinApplicationBuilder builder,string connectionString){
            builder.UseApplication<OutlookInspiredWindowsFormsApplication>();
            builder.AddModules();
            builder.UseMiddleTierModeSecurity();
            builder.AddMiddleTierMultiTenancy();
            
            builder.Services.AddDevExpressBlazor(options => {
                options.BootstrapVersion = BootstrapVersion.v5;
                options.SizeMode = SizeMode.Large;
            });
            builder.Services.AddWindowsFormsBlazorWebView();
            builder.AddBuildSteps(connectionString);
            return builder;
        }
        public static void AddBuildSteps(this IWinApplicationBuilder builder, string connectionString) 
            => builder.AddBuildStep(application => {
                application.DatabaseUpdateMode = DatabaseUpdateMode.Never;
                ((WinApplication)application).SplashScreen = new DXSplashScreen(
                    typeof(XafDemoSplashScreen), new DefaultOverlayFormOptions());
                application.ApplicationName = "OutlookInspired";
                SchedulerListEditor.DailyPrintStyleCalendarHeaderVisible = false;
                WinReportServiceController.UseNewWizard = true;
                application.LastLogonParametersReading += (_, e) => {
                    if (!string.IsNullOrWhiteSpace(e.SettingsStorage.LoadOption("", "UserName"))) return;
                    e.SettingsStorage.SaveOption("", "UserName", "Admin");
                };
                application.ConnectionString = connectionString;
            });

        private static void UseMiddleTierModeSecurity(this IWinApplicationBuilder builder){
            builder.AddMiddleTierObjectSpaceProviders().Context.Security.UseMiddleTierMode(options => {
                options.WaitForMiddleTierServerReady();
                options.BaseAddress = new Uri("https://localhost:44319/");
                options.Events.OnHttpClientCreated = client => client.DefaultRequestHeaders.Add("Accept", "application/json");
                options.Events.OnCustomAuthenticate = (_, _, args) => {
                    args.Handled = true;
                    var msg = args.HttpClient.PostAsJsonAsync("api/Authentication/Authenticate",
                        (AuthenticationStandardLogonParameters)args.LogonParameters).GetAwaiter().GetResult();
                    var token = (string)msg.Content.ReadFromJsonAsync(typeof(string)).GetAwaiter().GetResult();
                    if (msg.StatusCode == HttpStatusCode.Unauthorized){
                        XafExceptions.Authentication.ThrowAuthenticationFailedFromResponse(token);
                    }
                    msg.EnsureSuccessStatusCode();
                    args.HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
                };
            })
            .UsePasswordAuthentication();
            builder.Get().PostConfigure<SecurityOptions>(options => {
                options.RoleType = typeof(PermissionPolicyRole);
                options.UserType = typeof(ApplicationUser);
            });
        }

        public static void UseIntegratedModeSecurity(this IWinApplicationBuilder builder, string connectionString) 
            => builder.AddMultiTenancy(connectionString)
                .AddSecuredObjectSpaceProviders().Context.Security
                .UseIntegratedMode(options => {
                    options.RoleType = typeof(PermissionPolicyRole);
                    options.UserType = typeof(ApplicationUser);
                    options.UserLoginInfoType = typeof(ApplicationUserLoginInfo);
                    options.Events.OnSecurityStrategyCreated += securityStrategy =>
                        ((SecurityStrategy)securityStrategy).PermissionsReloadMode = PermissionsReloadMode.NoCache;
                })
                .UsePasswordAuthentication();

        public static IObjectSpaceProviderBuilder<IWinApplicationBuilder> AddSecuredObjectSpaceProviders(this IWinApplicationBuilder builder)
            => builder.ObjectSpaceProviders.AddSecuredEFCore(ConfigureObjectSpaceProvider())
                .ObjectSpaceProviderBuilder(application => application.ServiceProvider.AttachDatabase((application.ServiceProvider.GetRequiredService<IConnectionStringProvider>()).GetConnectionString()));

        private static Action<EFCoreObjectSpaceProviderOptionsBuilder> ConfigureObjectSpaceProvider()
            => options => options.PreFetchReferenceProperties();

        public static IObjectSpaceProviderBuilder<IWinApplicationBuilder> AddObjectSpaceProviders(this IWinApplicationBuilder builder)
            => builder.ObjectSpaceProviders.AddEFCore(ConfigureObjectSpaceProvider()).ObjectSpaceProviderBuilder();
        
        public static IObjectSpaceProviderBuilder<IWinApplicationBuilder> AddMiddleTierObjectSpaceProviders(this IWinApplicationBuilder builder) 
            => builder.ObjectSpaceProviders
                .AddEFCore(options => options.PreFetchReferenceProperties())
                .WithDbContext<OutlookInspiredEFCoreDbContext>((application, options) => {
                    options.UseMiddleTier(application.Security);
                    options.UseChangeTrackingProxies();
                    options.UseObjectSpaceLinkProxies();
                    var connectionString = application.ServiceProvider.GetRequiredService<ITenantProvider>().GetTenantConnectionString(application.ConnectionString);
                    application.ServiceProvider.AttachDatabase(connectionString);
                }, ServiceLifetime.Transient)
                .AddNonPersistent();

        public static IObjectSpaceProviderBuilder<IWinApplicationBuilder> ObjectSpaceProviderBuilder(this DbContextBuilder<IWinApplicationBuilder> builder,Action<XafApplication> configure=null) 
            => builder.WithDbContext<OutlookInspiredEFCoreDbContext>((application, options) => {
                configure?.Invoke(application);
                options.UseSqlite(application.ServiceProvider.GetRequiredService<IConnectionStringProvider>().GetConnectionString());
                options.UseChangeTrackingProxies();
                options.UseObjectSpaceLinkProxies();
                options.UseLazyLoadingProxies();
            }, ServiceLifetime.Transient)
                .AddNonPersistent();

        public static IWinApplicationBuilder AddMiddleTierMultiTenancy(this IWinApplicationBuilder builder) {
            builder.AddMultiTenancy()
                .WithHostDbContext((serviceProvider, options) => {
                    options.UseMiddleTier(serviceProvider.GetRequiredService<ISecurityStrategyBase>());
                    options.UseChangeTrackingProxies();
                },true)
                .WithMultiTenancyModelDifferenceStore(mds => {
#if !RELEASE
                    mds.UseTenantSpecificModel = false;
#endif
                })
                .WithTenantResolver<TenantByEmailResolver>();
            return builder;
        }

        public static IWinApplicationBuilder AddMultiTenancy(this IWinApplicationBuilder builder, string serviceConnectionString) {
            builder.AddMultiTenancy()
                .WithHostDbContext((_, options) => {
                    options.UseSqlite(serviceConnectionString);
                    options.UseChangeTrackingProxies();
                    options.UseLazyLoadingProxies();
                })
                .WithMultiTenancyModelDifferenceStore(mds => {
#if !RELEASE
                    mds.UseTenantSpecificModel = false;
#endif
                })
                .WithTenantResolver<TenantByEmailResolver>();
            return builder;
        }

        public static IModuleBuilder<IWinApplicationBuilder> AddModules(this IWinApplicationBuilder builder) 
            => builder.Modules
                .AddCharts()
                .AddConditionalAppearance()
                .AddDashboards(options => {
                    options.DashboardDataType = typeof(DashboardData);
                    options.DesignerFormStyle = DevExpress.XtraBars.Ribbon.RibbonFormStyle.Ribbon;
                })
                .AddFileAttachments()
                .AddNotifications()
                .AddOffice(options => options.RichTextMailMergeDataType=typeof(RichTextMailMergeData))
                .AddPivotChart(options => options.ShowAdditionalNavigation = true)
                .AddPivotGrid()
                .AddReports(options => {
                    options.EnableInplaceReports = true;
                    options.ReportDataType = typeof(ReportDataV2);
                    options.ReportStoreMode = DevExpress.ExpressApp.ReportsV2.ReportStoreModes.XML;
                    options.ShowAdditionalNavigation = false;
                })
                .AddScheduler()
                .AddTreeListEditors()
                .AddValidation(options => options.AllowValidationDetailsAccess = false)
                .AddViewVariants()
                .Add<OutlookInspiredWinModule>();

        
    }
}