using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using NServiceBus;
using NServiceBus.Extensions.Logging;
using NServiceBus.Logging;
using System.IO;
using System.Threading.Tasks;

namespace DomainName.App
{
    partial class Program
    {
        async static Task Main(string[] args)
        {
            NLog.LogManager.LoadConfiguration("nlog.config");
            LogManager.UseFactory(new ExtensionsLoggerFactory(new NLogLoggerFactory())); 
            await CreateHostBuilder(args).Build().RunAsync();
        }

            static IHostBuilder CreateHostBuilder(string[] args)
                => Host.CreateDefaultBuilder()
                    .ConfigureHostConfiguration(configHost =>
                    {
                        configHost.SetBasePath(Directory.GetCurrentDirectory());
                        configHost.AddJsonFile("appsettings.json", optional: false);
                        configHost.AddEnvironmentVariables(prefix: "STARNET_");
                        configHost.AddCommandLine(args);
                    }).ConfigureLogging(logging =>
                    {
                        logging.ClearProviders();
                        logging.AddNLog();
                    }).UseNServiceBus(hostBuilderContext =>
                        new EndpointConfigurationFactory().Create(hostBuilderContext.Configuration)
                    );
    }
}
