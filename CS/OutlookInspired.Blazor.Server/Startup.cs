using DevExpress.ExpressApp.ApplicationBuilder;
using DevExpress.ExpressApp.Blazor.ApplicationBuilder;
using DevExpress.ExpressApp.Blazor.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components.Server.Circuits;
using OutlookInspired.Blazor.Server.Services;
using OutlookInspired.Blazor.Server.Services.Internal;
using OutlookInspired.Module.Features.Maps;
using OutlookInspired.Module.Services;

namespace OutlookInspired.Blazor.Server;

public class Startup(IConfiguration configuration){
    public IConfiguration Configuration { get; } = configuration;

    public void ConfigureServices(IServiceCollection services) {
        services.AddSingleton<IMapColorService,MapColorService>();
        services.AddSingleton(typeof(Microsoft.AspNetCore.SignalR.HubConnectionHandler<>), typeof(ProxyHubConnectionHandler<>));
        services.AddRazorPages();
        services.AddServerSideBlazor();
        services.AddHttpContextAccessor();
        services.AddScoped<CircuitHandler, CircuitHandlerProxy>();
        services.AddXaf(Configuration, builder => builder.UseApplication<OutlookInspiredBlazorApplication>().AddModules()
            .AddObjectSpaceProviders().AddSecurity().AddMultiTenancy(Configuration).AddBuildStep());
        services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options => options.LoginPath = "/LoginPage");
        services.AddSingleton<IMapApiKeyProvider, MapApiKeyProvider>();
        
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



