using NUnit.Framework;
using System.Threading.Tasks;

namespace DomainName.Domain.Tests
{
    class CorrectOrganizationNameTests : _ServiceSpec
    {
        [Test]
        public async Task Can_Execute()
        {
            var aggId = "Organizations-1";
            Given(EventsFactory.CreateOrganizationRegisteredEvent(aggId));
            When(CommandsFactory.CreateCorrectOrganizationNameCommand(aggId));
            await Expect(EventsFactory.CreateOrganizationNameCorrectedEvent(aggId));
        }

        [Test]
        public async Task Throws_Error_On_Organization_Non_Existant()
        {
            var aggId = "Organizations-1";
            Given();
            When(CommandsFactory.CreateCorrectOrganizationNameCommand(aggId));
            await ExpectError("OrganizationDoesNotExist");
        }

        [Test]
        public async Task Correction_Is_Idempotent()
        {
            var aggregateId = "Organizations-1";
            Given(
                EventsFactory.CreateOrganizationRegisteredEvent(aggregateId),
                EventsFactory.CreateOrganizationNameCorrectedEvent(aggregateId));

            When(CommandsFactory.CreateCorrectOrganizationNameCommand(aggregateId));

            await Expect();
        }
    }
}