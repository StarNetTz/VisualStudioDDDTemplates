using DomainName.ReadModel.Projections;
using DomainName.ReadModel.Repositories.RavenDB;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NLog.Extensions.Logging;
using NServiceBus.Extensions.Logging;
using Starnet.Projections;
using Starnet.Projections.ES;
using Starnet.Projections.RavenDb;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace DomainName.ReadModel.App
{
    class Program
    {
        async static Task Main(string[] args)
        {
            NLog.LogManager.LoadConfiguration("nlog.config");
            NServiceBus.Logging.LogManager.UseFactory(new ExtensionsLoggerFactory(new NLogLoggerFactory()));
            await CreateHostBuilder(args).Build().RunAsync();
        }

        static IHostBuilder CreateHostBuilder(string[] args)
            => Host.CreateDefaultBuilder()
                .ConfigureServices((hostContext, services) =>
                {
                    var docStore = new RavenDocumentStoreFactory().CreateAndInitializeDocumentStore(RavenConfig.FromConfiguration(hostContext.Configuration));
                    services.AddSingleton(docStore);
                    services.AddSingleton<INoSqlStore, DefensiveRavenDbProjectionsStore>();
                    services.AddSingleton<ISqlStore, DefensiveRavenDbProjectionsStore>();
                    services.AddTransient(typeof(IQueryById<>), typeof(QueryById<>));
                    services.AddTransient<ICheckpointReader, RavenDbCheckpointReader>();
                    services.AddTransient<ICheckpointWriter, RavenDbCheckpointWriter>();
                    services.AddTransient<IHandlerFactory, DIHandlerFactory>();
                    services.AddTransient<ISubscriptionFactory, ESSubscriptionFactory>();
                    services.AddTransient<IProjectionsFactory, ProjectionsFactory>();
                    services.AddTransient<IJSProjectionsFactory, JSProjectionsFactory>();

                    RegisterProjectionHandlers(services);

                    services.AddHostedService<ServiceInstance>();
                })
                .ConfigureHostConfiguration(configHost =>
                {
                    configHost.SetBasePath(Directory.GetCurrentDirectory());
                    configHost.AddJsonFile("appsettings.json", optional: false);
                    configHost.AddEnvironmentVariables(prefix: "STARNET_");
                    configHost.AddCommandLine(args);
                });

        static void RegisterProjectionHandlers(IServiceCollection services)
        {
            var a = Assembly.GetAssembly(typeof(OrganizationProjectionHandler));
            var results = from type in a.GetTypes()
                          where typeof(IHandler).IsAssignableFrom(type)
                          select type;
            foreach (var t in results)
                services.AddTransient(t);
        }
    }
}