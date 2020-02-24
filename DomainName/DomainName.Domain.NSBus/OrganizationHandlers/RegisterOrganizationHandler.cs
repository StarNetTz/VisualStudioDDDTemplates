using DomainName.Domain.Organization;
using DomainName.PL.Commands;
using NServiceBus;
using System.Threading.Tasks;

namespace DomainName.Domain.NSBus
{
    public class RegisterOrganizationHandler : AggregateHandlerBase, IHandleMessages<RegisterOrganization>
    {
        readonly IOrganizationInteractor Svc;

        public RegisterOrganizationHandler(IOrganizationInteractor svc)
        {
            Svc = svc;
        }

        public async Task Handle(RegisterOrganization message, IMessageHandlerContext context)
        {
            await TryHandle(message, context, Svc);
        }
    }
}