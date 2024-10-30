using System.Text;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.ApplicationBuilder;
using DevExpress.ExpressApp.MultiTenancy;
using DevExpress.ExpressApp.Security;
using DevExpress.ExpressApp.Security.Authentication.ClientServer;
using DevExpress.Persistent.BaseImpl.EF.PermissionPolicy;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using OutlookInspired.WebApi.JWT;

namespace OutlookInspired.MiddleTier;

public class Startup(IConfiguration configuration){
    public IConfiguration Configuration { get; } = configuration;

    // This method gets called by the runtime. Use this method to add services to the container.
    // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
    public void ConfigureServices(IServiceCollection services) {
        services.AddScoped<IAuthenticationTokenProvider, JwtTokenProviderService>();

        services.AddXafMiddleTier(Configuration, builder => {

            builder.Modules
                .AddReports(options => options.ReportDataType = typeof(DevExpress.Persistent.BaseImpl.EF.ReportDataV2))
                .Add<Module.OutlookInspiredModule>();


            builder.AddMultiTenancy()
                .WithHostDbContext((serviceProvider, options) => {
                    string connectionString = Configuration.GetConnectionString("ConnectionString");
#if EASYTEST
                    string connectionString = Configuration.GetConnectionString("EasyTestConnectionString");
#endif
                    options.UseSqlite(connectionString);
                    options.UseChangeTrackingProxies();
                    options.UseLazyLoadingProxies();
                })
                .WithMultiTenancyModelDifferenceStore(options => {
#if !RELEASE
                    options.UseTenantSpecificModel = false;
#endif
                })
#if !RELEASE
                .WithTenantDatabaseUpdater()
#endif
                .WithTenantResolver<TenantByEmailResolver>();


            builder.ObjectSpaceProviders
                .AddSecuredEFCore()
                    .WithDbContext<Module.BusinessObjects.OutlookInspiredEFCoreDbContext>((serviceProvider, options) => {
                        var connectionString = serviceProvider.GetRequiredService<IConnectionStringProvider>().GetConnectionString();
                        options.UseSqlite(connectionString);
                        options.UseChangeTrackingProxies();
                        options.UseObjectSpaceLinkProxies();
                        options.UseLazyLoadingProxies();
                    })
                .AddNonPersistent();

            builder.Security
                .UseIntegratedMode(options => {
                    options.Lockout.Enabled = true;

                    options.RoleType = typeof(PermissionPolicyRole);
                    // ApplicationUser descends from PermissionPolicyUser and supports the OAuth authentication. For more information, refer to the following topic: https://docs.devexpress.com/eXpressAppFramework/402197
                    // If your application uses PermissionPolicyUser or a custom user type, set the UserType property as follows:
                    options.UserType = typeof(Module.BusinessObjects.ApplicationUser);
                    // ApplicationUserLoginInfo is only necessary for applications that use the ApplicationUser user type.
                    // If you use PermissionPolicyUser or a custom user type, comment out the following line:
                    options.UserLoginInfoType = typeof(Module.BusinessObjects.ApplicationUserLoginInfo);
                    options.Events.OnSecurityStrategyCreated += securityStrategy => {
                        //((SecurityStrategy)securityStrategy).AnonymousAllowedTypes.Add(typeof(DevExpress.Persistent.BaseImpl.EF.ModelDifference));
                        //((SecurityStrategy)securityStrategy).AnonymousAllowedTypes.Add(typeof(DevExpress.Persistent.BaseImpl.EF.ModelDifferenceAspect));
                        ((SecurityStrategy)securityStrategy).PermissionsReloadMode = PermissionsReloadMode.CacheOnFirstAccess;
                    };
                })
                .AddPasswordAuthentication(options => {
                    options.IsSupportChangePassword = true;
                });

            builder.AddBuildStep(application => {
                application.ApplicationName = "SetupApplication.OutlookInspired";
                application.CheckCompatibilityType = CheckCompatibilityType.DatabaseSchema;
                //application.CreateCustomModelDifferenceStore += += (s, e) => {
                    //    e.Store = new ModelDifferenceDbStore((XafApplication)sender!, typeof(ModelDifference), true, "Win");
                    //    e.Handled = true;
                //};
// #if DEBUG
                if( application.CheckCompatibilityType == CheckCompatibilityType.DatabaseSchema) {
                    application.DatabaseUpdateMode = DatabaseUpdateMode.UpdateDatabaseAlways;
                    application.DatabaseVersionMismatch += (s, e) => {
                        e.Updater.Update();
                        e.Handled = true;
                    };
                }
// #endif
            });
        });

        services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options => {
                options.TokenValidationParameters = new TokenValidationParameters() {
                    ValidateIssuerSigningKey = true,
                    //ValidIssuer = Configuration["Authentication:Jwt:Issuer"],
                    //ValidAudience = Configuration["Authentication:Jwt:Audience"],
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Authentication:Jwt:IssuerSigningKey"]!))
                };
            });

        services.AddAuthorization(options => {
            options.DefaultPolicy = new AuthorizationPolicyBuilder(
                JwtBearerDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser()
                    .RequireXafAuthentication()
                    .Build();
        });

        services.AddSwaggerGen(c => {
            c.EnableAnnotations();
            c.SwaggerDoc("v1", new OpenApiInfo {
                Title = "OutlookInspired API",
                Version = "v1",
                Description = @"MiddleTier"
            });
            c.AddSecurityDefinition("JWT", new OpenApiSecurityScheme() {
                Type = SecuritySchemeType.Http,
                Name = "Bearer",
                Scheme = "bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header
            });
            c.AddSecurityRequirement(new OpenApiSecurityRequirement() {
                {
                    new OpenApiSecurityScheme() {
                        Reference = new OpenApiReference() {
                            Type = ReferenceType.SecurityScheme,
                            Id = "JWT"
                        }
                    },
                    Array.Empty<string>()
                },
            });
        });
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
        if(env.IsDevelopment()) {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "OutlookInspired Api v1");
            });
        }
        else {
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. To change this for production scenarios, see: https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }
        app.UseHttpsRedirection();
        app.UseRequestLocalization();
        app.UseStaticFiles();
        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseXafMiddleTier();
        app.UseEndpoints(endpoints => {
            endpoints.MapControllers();
        });
    }
}
