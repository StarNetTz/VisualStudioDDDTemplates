using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Funq;
using ServiceStack;
using $ext_projectname$.WebApi.ServiceInterface;
using $ext_projectname$.ReadModel;
using $ext_projectname$.ReadModel.Repositories.RavenDB;
using $ext_projectname$.WebApi.Infrastructure;
using ServiceStack.Validation;
using ServiceStack.Auth;
using Microsoft.Extensions.Hosting;
using Raven.Client.Documents;

namespace $safeprojectname$
{
    public class Startup : ModularStartup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public new void ConfigureServices(IServiceCollection services)
        {
            var raven = CreateRavenDbDocumentStore();
            services.AddSingleton(raven);
            services.AddTransient<ITimeProvider, TimeProvider>();
            services.AddTransient<ITypeaheadSmartSearchQuery, TypeaheadSmartSearchQuery>();
            services.AddTransient<IMessageBus, NSBus>();
            services.AddTransient<IOrganizationSmartSearchQuery, OrganizationSmartSearchQuery>();
            services.AddTransient(typeof(IQueryById<>), typeof(QueryById<>));
        }

            IDocumentStore CreateRavenDbDocumentStore()
                => new RavenDocumentStoreFactory().CreateAndInitializeDocumentStore(RavenConfig.FromConfiguration(Configuration));

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseServiceStack(new AppHost(Configuration)
            {
                AppSettings = new NetCoreAppSettings(Configuration)
            });
        }
    }

    public class AppHost : AppHostBase
    {
        public AppHost(IConfiguration configuration) : base("$safeprojectname$", typeof(OrganizationService).Assembly)
        {
            Configuration = configuration;
            Licensing.RegisterLicense(Configuration["ServiceStack:Licence"]);
        }

        // Configure your AppHost with the necessary configuration and dependencies your App needs
        public override void Configure(Container container)
        {
            ServiceStack.Text.JsConfig.TreatEnumAsInteger = true;
            ServiceStack.Text.JsConfig.AssumeUtc = true;
            ServiceStack.Text.JsConfig.AlwaysUseUtc = true;
            ServiceStack.Text.JsConfig.DateHandler = ServiceStack.Text.DateHandler.ISO8601;

            SetConfig(new HostConfig
            {
                DefaultRedirectPath = "/metadata",
                DebugMode = AppSettings.Get(nameof(HostConfig.DebugMode), false)
            });

            Plugins.Add(new ValidationFeature());
            Plugins.Add(new CorsFeature(allowCredentials: true, allowedHeaders: "Content-Type, Authorization", allowOriginWhitelist: GetOriginWhiteList()));
            Plugins.Add(new AuthFeature(() => new AuthUserSession(),
                new IAuthProvider[] {
                new JwtAuthProvider(AppSettings) {
                    AuthKeyBase64 = Configuration["Jwt:Key"],
                    RequireSecureConnection = false, //dev configuration
                    EncryptPayload = false, //dev configuration
                    HashAlgorithm = "HS256"
                }
            }));
        }

            string[] GetOriginWhiteList()
                => Configuration["CORS:Whitelist"].Split(';');
    }
}