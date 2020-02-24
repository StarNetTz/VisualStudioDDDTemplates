using NUnit.Framework;
using System.Threading.Tasks;

namespace DomainName.Domain.Tests.OrganizationTests
{
    class RegisterOrganizationTests : _ServiceSpec
    {

        [Test]
        public async Task Can_Execute()
        {
            string aggId = "Organizations-1";
            Given();
            When(CommandsFactory.CreateRegisterOrganizationCommand(aggId));
            await Expect(EventsFactory.CreateOrganizationRegisteredEvent(aggId));
        }

        [Test]
        public async Task Is_Idempotent()
        {
            string aggId = "Organizations-1";
            var cmd = CommandsFactory.CreateRegisterOrganizationCommand(aggId);
            var evt = EventsFactory.CreateOrganizationRegisteredEvent(aggId);

            Given(evt);
            When(cmd);
            await Expect();
        }

        [Test]
        public async Task NonIdempotent_Throws_OrganizationAlreadyExists()
        {
            string aggId = "Organizations-1";
            var cmd = CommandsFactory.CreateRegisterOrganizationCommand(aggId);
            cmd.Address.Country = "Some other";
            var evt = EventsFactory.CreateOrganizationRegisteredEvent(aggId);

            Given(evt);
            When(cmd);
            await ExpectError("OrganizationAlreadyExists");
        }
    }
}