using DomainName.ReadModel.Projections;
using DomainName.ReadModel.Projections.ES;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Starnet.Projections;
using Starnet.Projections.ES;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace DomainName.ReadModel.App
{
    internal class ServiceInstance : IHostedService
    {
        readonly ILogger<ServiceInstance> Logger;
        readonly IConfiguration Configuration;
        readonly IProjectionsFactory ProjectionsFactory;
        readonly IJSProjectionsFactory JSProjectionsFactory;

        public ServiceInstance(ILogger<ServiceInstance> logger, IConfiguration configuration, IProjectionsFactory projectionsFactory, IJSProjectionsFactory jSProjectionsFactory)
        {
            Logger = logger;
            Configuration = configuration;
            ProjectionsFactory = projectionsFactory;
            JSProjectionsFactory = jSProjectionsFactory;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await CreateEventStoreProjections();
            await RunProjections();
        }

            async Task CreateEventStoreProjections()
            {
                foreach (var v in EventStoreProjectionDefinitions.CreateEventStoreProjectionSources())
                    JSProjectionsFactory.AddProjection(v.Key, v.Value);
                await JSProjectionsFactory.CreateProjections();
            }

            async Task RunProjections()
            {
                var projections = await ProjectionsFactory.Create(Assembly.GetAssembly(typeof(OrganizationProjection)));
                var projectionsToRun = Configuration["ActiveProjections"];
                if (projectionsToRun == "All")
                    await RunAllProjections(projections);
                else
                    await RunConfiguredProjections(projections, projectionsToRun.Split(";"));
            }

                async Task RunAllProjections(IList<IProjection> projections)
                {
                    foreach (var p in projections)
                    {
                        Logger.LogInformation($"Starting {p.Name} on stream {p.Subscription.StreamName}.");
                        await p.Start();
                    }
                }

                async Task RunConfiguredProjections(IList<IProjection> projections, string[] projectionsToRunList)
                {
                    foreach (var p in projections)
                    {
                        if (!projectionsToRunList.Contains(p.Name))
                            continue;
                        Logger.LogInformation($"Starting projection {p.Name} on stream {p.Subscription.StreamName}.");
                        await p.Start();
                    }
                }

        public Task StopAsync(CancellationToken cancellationToken)
            => Task.CompletedTask;
    }
}