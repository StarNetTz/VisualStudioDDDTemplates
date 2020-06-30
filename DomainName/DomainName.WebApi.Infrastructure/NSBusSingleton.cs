using DomainName.WebApi.ServiceInterface;
using Microsoft.Extensions.Configuration;
using NServiceBus;
using System.Threading.Tasks;

namespace DomainName.WebApi.Infrastructure
{
    public class NSBus : IMessageBus
    {
        public async Task Send(object message)
            => await NSBusSingleton.DomainNameAppEndpointInstance.Send(message);
    }

    class NSBusSingleton
    {
        public static IEndpointInstance DomainNameAppEndpointInstance;

        static NSBusSingleton()
        {
            DomainNameAppEndpointInstance = Endpoint.Start(CreateEndpointConfiguration()).GetAwaiter().GetResult();
        }

        static EndpointConfiguration CreateEndpointConfiguration()
        {
            var config = new ConfigurationBuilder().AddJsonFile("appsettings.json", true, true).Build();
            var endpointConfiguration = new EndpointConfiguration(config["NSBus:EndpointName"]);

            var transport = endpointConfiguration.UseTransport<RabbitMQTransport>();
            transport.UseConventionalRoutingTopology();

            transport.ConnectionString(config["RabbitMQ:ConnectionString"]);

            var routing = transport.Routing();
            routing.RouteToEndpoint(
                assembly: typeof(PL.Commands.RegisterOrganization).Assembly,
                destination: config["NSBus:DomainNameAppEndpointName"]);

            var conventions = endpointConfiguration.Conventions();
            conventions.DefiningCommandsAs(
                type =>
                    type.Namespace == "DomainName.PL.Commands"
                );

            endpointConfiguration.SendOnly();
            endpointConfiguration.EnableInstallers();
            return endpointConfiguration;
        }
    }
}
