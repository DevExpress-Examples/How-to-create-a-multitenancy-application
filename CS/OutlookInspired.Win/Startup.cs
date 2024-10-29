using System.IO.Compression;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using DevExpress.Blazor;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.ApplicationBuilder;
using DevExpress.ExpressApp.Win;
using DevExpress.ExpressApp.Security.ClientServer;
using DevExpress.Persistent.BaseImpl.EF.PermissionPolicy;
using DevExpress.ExpressApp.Design;
using DevExpress.ExpressApp.MultiTenancy;
using DevExpress.ExpressApp.ReportsV2.Win;
using DevExpress.ExpressApp.Scheduler.Win;
using DevExpress.ExpressApp.Security;
using DevExpress.ExpressApp.Win.ApplicationBuilder;
using DevExpress.ExpressApp.Win.Utils;
using DevExpress.Persistent.BaseImpl.EF;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OutlookInspired.Module.BusinessObjects;
using OutlookInspired.Module.Features.Maps;

namespace OutlookInspired.Win;
public class ApplicationBuilder : IDesignTimeApplicationFactory {
    public static WinApplication BuildApplication(string connectionString){
        var builder = WinApplication.CreateBuilder();
        builder.UseApplication<OutlookInspiredWindowsFormsApplication>();
        builder.Modules
            .AddCharts()
            .AddConditionalAppearance()
            .AddDashboards(options => {
                options.DashboardDataType = typeof(DashboardData);
                options.DesignerFormStyle = DevExpress.XtraBars.Ribbon.RibbonFormStyle.Ribbon;
            })
            .AddFileAttachments()
            .AddNotifications()
            .AddOffice(options => options.RichTextMailMergeDataType = typeof(RichTextMailMergeData))
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
        builder.ObjectSpaceProviders
            .AddEFCore(options => options.PreFetchReferenceProperties())
            .WithDbContext<OutlookInspiredEFCoreDbContext>((application, options) => {
                options.UseMiddleTier(application.Security);
                options.UseChangeTrackingProxies();
                options.UseObjectSpaceLinkProxies();
                ExtractDb(application);
            }, ServiceLifetime.Transient)
            .AddNonPersistent()
            .Context.Security
            .UseMiddleTierMode(options => {
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

        builder.AddMultiTenancy()
            .WithHostDbContext((serviceProvider, options) => {
                options.UseMiddleTier(serviceProvider.GetRequiredService<ISecurityStrategyBase>());
                options.UseChangeTrackingProxies();
            }, true)
            .WithMultiTenancyModelDifferenceStore(mds => {
#if !RELEASE
                mds.UseTenantSpecificModel = false;
#endif
            })
            .WithTenantResolver<TenantByEmailResolver>();
        var services = builder.Services;
        services.AddSingleton<IMapApiKeyProvider, MapApiKeyProvider>();
        services.AddDevExpressBlazor(options => {
            options.BootstrapVersion = BootstrapVersion.v5;
            options.SizeMode = SizeMode.Large;
        });
        services.AddWindowsFormsBlazorWebView();

        builder.AddBuildStep(application => {
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
        return builder.Build();
    }

    static string FindFolderInPathUpwards(string folderName){
        var current = new DirectoryInfo(Directory.GetCurrentDirectory());
        var directory = current;
        while (directory.Parent != null){
            if (directory.GetDirectories(folderName).Any()){
                return Path.GetRelativePath(current.FullName, Path.Combine(directory.FullName, folderName));
            }
            directory = directory.Parent;
        }
        throw new DirectoryNotFoundException($"Folder '{folderName}' not found up the tree from '{current.FullName}'");
    }

    private static void ExtractDb(XafApplication application){
        var dataPath = FindFolderInPathUpwards("Data");
        var connectionString = GetTenantConnectionString(application, dataPath);
        if (!File.Exists($"{dataPath}\\OutlookInspired.db")){
            ZipFile.ExtractToDirectory($"{dataPath}\\OutlookInspired.zip",dataPath);
        }
        var dbPath = $"{dataPath}\\{Path.GetFileName(connectionString)}";
        if (!File.Exists(dbPath)){
            File.Copy($"{dataPath}\\OutlookInspired.db",dbPath);
        }
    }

    static string GetTenantConnectionString( XafApplication application, string dataPath){
        var tenantProvider = application.ServiceProvider.GetRequiredService<ITenantProvider>();
        using var connection = new SqliteConnection($"Data source={Path.GetFullPath($"{dataPath}\\{Path.GetFileName(application.ConnectionString)}")}");
        var query = "SELECT ConnectionString FROM Tenant WHERE ID = @Id";
        using var command = new SqliteCommand(query, connection);
        command.Parameters.AddWithValue("@Id", tenantProvider.TenantId);
        connection.Open();
        using var reader = command.ExecuteReader();
        if (reader.Read()){
            return reader.GetString(0);
        } 
        throw new InvalidOperationException("Tenant not found. Make sure you have already login as super admin at least once.");
    }

    XafApplication IDesignTimeApplicationFactory.Create() {
        MiddleTierClientSecurityBase.DesignModeUserType = typeof(ApplicationUser);
        MiddleTierClientSecurityBase.DesignModeRoleType = typeof(PermissionPolicyRole);
        return BuildApplication(XafApplication.DesignTimeConnectionString);
    }
}
