using EventStore.ClientAPI;
using Microsoft.Extensions.Configuration;
using Npgsql;
using NpgsqlTypes;
using NServiceBus;
using NServiceBus.Logging;
using Starnet.Aggregates.ES;
using $ext_projectname$.Domain.Infrastructure;
using System;
using System.Threading.Tasks;

namespace $safeprojectname$
{
    class Host
    {
        static readonly ILog log = LogManager.GetLogger<Host>();

        IEndpointInstance endpoint;
        IConfiguration Configuration;

        public string EndpointName { get; set; }

        public Host()
        {
            Configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json", true, true).Build();
            EndpointName = Configuration["NSBus:EndpointName"];
        }

        public async Task Start()
        {
            try
            {
                var endpointConfiguration = CreateEndpointConfiguration(Configuration);
                endpoint = await Endpoint.Start(endpointConfiguration);
                Console.WriteLine("Use 'docker-compose down' to stop containers.");
            }
            catch (Exception ex)
            {
                FailFast("Failed to start.", ex);
            }
        }

       

        EndpointConfiguration CreateEndpointConfiguration(IConfiguration config)
        {
            var endpointConfiguration = new EndpointConfiguration(config["NSBus:EndpointName"]);
            //endpointConfiguration.PurgeOnStartup(true);
            RegisterComponents(config, endpointConfiguration);
            InitializeTransport(config, endpointConfiguration);
            InitializePostgressPersistence(config, endpointConfiguration);
            SetupConventions(endpointConfiguration);
            SetupHeartBeatAndMetrics(config, endpointConfiguration);
            endpointConfiguration.EnableInstallers();
            return endpointConfiguration;
        }

        void RegisterComponents(IConfiguration config, EndpointConfiguration endpointConfiguration)
        {
            endpointConfiguration.RegisterComponents(reg =>
            {
                reg.ConfigureComponent<ESAggregateRepository>(CreateEventStoreAggregateRepository, DependencyLifecycle.SingleInstance);
                reg.ConfigureComponent<IConfiguration>(() => config, DependencyLifecycle.SingleInstance);
                reg.ConfigureComponent<TimeProvider>(DependencyLifecycle.InstancePerCall);
                RegisterAggregateInteractors(reg);
            });
        }

        ESAggregateRepository CreateEventStoreAggregateRepository()
        {
            var uri = new Uri(Configuration["EventStore:Uri"]);
            var Connection = EventStoreConnection.Create(uri);
            Connection.ConnectAsync().Wait();
            var Repository = new ESAggregateRepository(Connection);
            return Repository;
        }

        static void RegisterAggregateInteractors(NServiceBus.ObjectBuilder.IConfigureComponents reg)
        {
            foreach (var type in AggregateInteractorsExtractor.GetInteractors())
                reg.ConfigureComponent(type, DependencyLifecycle.InstancePerCall);
        }

        static void InitializeTransport(IConfiguration config, EndpointConfiguration endpointConfiguration)
        {
            var transport = endpointConfiguration.UseTransport<RabbitMQTransport>();
            transport.UseConventionalRoutingTopology();
            transport.ConnectionString(config["RabbitMQ:ConnectionString"]);
        }

        static void InitializePostgressPersistence(IConfiguration config, EndpointConfiguration endpointConfiguration)
        {
            var persistence = endpointConfiguration.UsePersistence<SqlPersistence>();
            persistence.TablePrefix("AppName");

            var dialect = persistence.SqlDialect<SqlDialect.PostgreSql>();
            dialect.JsonBParameterModifier(
                modifier: parameter =>
                {
                    var npgsqlParameter = (NpgsqlParameter)parameter;
                    npgsqlParameter.NpgsqlDbType = NpgsqlDbType.Jsonb;
                });
            persistence.ConnectionBuilder(
                connectionBuilder: () =>
                {
                    return new NpgsqlConnection(config["Postgress:ConnectionString"]);
                });
            var subscriptions = persistence.SubscriptionSettings();
            subscriptions.CacheFor(TimeSpan.FromMinutes(1));
        }

        static void SetupConventions(EndpointConfiguration endpointConfiguration)
        {
            var conventions = endpointConfiguration.Conventions();

            conventions.DefiningCommandsAs(
                type =>
                {
                    return (
                    (type.Namespace == "DomainName.PL.Commands")
                    );
                });

            conventions.DefiningEventsAs(
                type =>
                {
                    return (
                    (type.Namespace == "DomainName.PL.Events")
                    );
                });
        }

        static void SetupHeartBeatAndMetrics(IConfiguration config, EndpointConfiguration endpointConfiguration)
        {
            endpointConfiguration.SendHeartbeatTo(
                serviceControlQueue: "Particular.ServiceControl",
                frequency: TimeSpan.FromSeconds(15),
                timeToLive: TimeSpan.FromSeconds(30));

            const string SERVICE_CONTROL_METRICS_ADDRESS = "Particular.Monitoring";

            var metrics = endpointConfiguration.EnableMetrics();

            metrics.SendMetricDataToServiceControl(
                serviceControlMetricsAddress: SERVICE_CONTROL_METRICS_ADDRESS,
                interval: TimeSpan.FromSeconds(10),
                instanceId: config["NSBus:EndpointName"]
                );
        }

        public async Task Stop()
        {
            try
            {
                await endpoint?.Stop();
            }
            catch (Exception ex)
            {
                FailFast("Failed to stop correctly.", ex);
            }
        }

        async Task OnCriticalError(ICriticalErrorContext context)
        {
            try
            {
                await context.Stop();
            }
            finally
            {
                FailFast($"Critical error, shutting down: {context.Error}", context.Exception);
            }
        }

        void FailFast(string message, Exception exception)
        {
            try
            {
                log.Fatal(message, exception);
            }
            finally
            {
                Environment.FailFast(message, exception);
            }
        }
    }
}
