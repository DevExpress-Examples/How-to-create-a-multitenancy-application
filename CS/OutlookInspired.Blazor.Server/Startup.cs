using System.IO.Compression;
using Aqua.EnumerableExtensions;
using DevExpress.ExpressApp.ApplicationBuilder;
using DevExpress.ExpressApp.Blazor.ApplicationBuilder;
using DevExpress.ExpressApp.Blazor.Services;
using DevExpress.ExpressApp.MultiTenancy;
using DevExpress.ExpressApp.Security;
using DevExpress.Persistent.BaseImpl.EF;
using DevExpress.Persistent.BaseImpl.EF.PermissionPolicy;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components.Server.Circuits;
using Microsoft.EntityFrameworkCore;
using OutlookInspired.Blazor.Server.Services;
using OutlookInspired.Blazor.Server.Services.Internal;
using OutlookInspired.Module;
using OutlookInspired.Module.Features.Maps;

namespace OutlookInspired.Blazor.Server;

public class Startup(IConfiguration configuration){
    public IConfiguration Configuration { get; } = configuration;

    public void ConfigureServices(IServiceCollection services) {
        services.AddSingleton(typeof(Microsoft.AspNetCore.SignalR.HubConnectionHandler<>), typeof(ProxyHubConnectionHandler<>));
        services.AddRazorPages();
        services.AddServerSideBlazor();
        services.AddHttpContextAccessor();
        services.AddScoped<CircuitHandler, CircuitHandlerProxy>();
        services.AddXaf(Configuration, builder => {
            builder.UseApplication<OutlookInspiredBlazorApplication>();
            builder.Services.AddDevExpressServerSideBlazorPdfViewer();
            builder.Modules
                .AddConditionalAppearance()
                .AddFileAttachments()
                .AddOffice(options => options.RichTextMailMergeDataType = typeof(RichTextMailMergeData))
                .AddReports(options => {
                    options.EnableInplaceReports = false;
                    options.ReportDataType = typeof(ReportDataV2);
                    options.ReportStoreMode = DevExpress.ExpressApp.ReportsV2.ReportStoreModes.XML;
                })
                .AddScheduler()
                .AddValidation()
                .AddViewVariants()
                .Add<OutlookInspiredModule>()
                .Add<OutlookInspiredBlazorModule>();
            builder.ObjectSpaceProviders
                .AddSecuredEFCore(options => options.PreFetchReferenceProperties())
                .WithDbContext<Module.BusinessObjects.OutlookInspiredEFCoreDbContext>((serviceProvider, options) => {
                    var connectionString = serviceProvider.GetRequiredService<IConnectionStringProvider>().GetConnectionString();
                    ExtractDb(connectionString);
                    options.UseSqlite(connectionString);
                    options.UseChangeTrackingProxies();
                    options.UseObjectSpaceLinkProxies();
                    options.UseLazyLoadingProxies();
                })
                .AddNonPersistent();
            builder.Security
                .UseIntegratedMode(options => {
                    options.RoleType = typeof(PermissionPolicyRole);
                    options.UserType = typeof(Module.BusinessObjects.ApplicationUser);
                    options.UserLoginInfoType = typeof(Module.BusinessObjects.ApplicationUserLoginInfo);
                    options.Events.OnSecurityStrategyCreated += securityStrategy => ((SecurityStrategy)securityStrategy).PermissionsReloadMode = PermissionsReloadMode.CacheOnFirstAccess;
                })
                .AddPasswordAuthentication(options => {
                    options.IsSupportChangePassword = true;
                });
            builder.AddMultiTenancy()
                .WithHostDbContext((serviceProvider, options) => {
#if EASYTEST
                    string connectionString = configuration.GetConnectionString("EasyTestConnectionString");
#else
                    string connectionString = Configuration.GetConnectionString("ConnectionString");
#endif
                    options.UseSqlite(connectionString);
                    options.UseChangeTrackingProxies();
                    options.UseLazyLoadingProxies();
                })
                .WithMultiTenancyModelDifferenceStore(e => {
#if !RELEASE
                    e.UseTenantSpecificModel = false;
#endif
                })
                .WithTenantResolver<TenantByEmailResolver>();
            builder.AddBuildStep(application => {
                application.ApplicationName = "OutlookInspired";
                application.CheckCompatibilityType = DevExpress.ExpressApp.CheckCompatibilityType.DatabaseSchema;
                application.DatabaseVersionMismatch += (_, e) => {
                    e.Updater.Update();
                    e.Handled = true;
                };
                application.LastLogonParametersRead += (_, e) => {
                    if (e.LogonObject is not AuthenticationStandardLogonParameters logonParameters || !logonParameters.UserName.IsNullOrEmpty()) return;
                    logonParameters.UserName = "Admin";
                };
            });
        });
        services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options => options.LoginPath = "/LoginPage");
        services.AddSingleton<IMapApiKeyProvider, MapApiKeyProvider>();
        
    }

    private static void ExtractDb(string connectionString){
        var dataPath = FindFolderInPathUpwards("Data");
        if (!File.Exists($"{dataPath}\\OutlookInspired.db")){
            ZipFile.ExtractToDirectory($"{dataPath}\\OutlookInspired.zip",dataPath);
        }
        var dbPath = $"{dataPath}\\{Path.GetFileName(connectionString)}";
        if (!File.Exists(dbPath)){
            File.Copy($"{dataPath}\\OutlookInspired.db",dbPath);
        }
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


    public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
        if(env.IsDevelopment()) {
            app.UseDeveloperExceptionPage();
        }
        else {
            app.UseExceptionHandler("/Error");
            app.UseHsts();
        }
        app.UseHttpsRedirection();
        app.UseRequestLocalization();
        app.UseStaticFiles();
        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseXaf();
        app.UseEndpoints(endpoints => {
            endpoints.MapXafEndpoints();
            endpoints.MapBlazorHub();
            endpoints.MapFallbackToPage("/_Host");
            endpoints.MapControllers();
        });
    }
}



