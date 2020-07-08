using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using ServiceStack;
using System.Threading.Tasks;

namespace DomainName.WebApi
{
    public class Program
    {
        public async static Task Main(string[] args)
        {
            NLog.LogManager.LoadConfiguration("nlog.config");
            await BuildWebHost(args).Build().RunAsync();
        }

        public static IWebHostBuilder BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseModularStartup<Startup>();
    }
}
