using DomainName.ReadModel;
using DomainName.WebApi.ServiceInterface;
using DomainName.WebApi.ServiceModel;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using ServiceStack.Configuration;
using System.Threading.Tasks;

namespace DomainName.WebApi.UnitTests
{
    class LookupsServiceTests : QueryServiceTestBase<LookupsService>
    {
        public override IContainerAdapter CreateContainerAdapter()
            => new MicrosoftDIAdapter(MSContainer());

        IServiceCollection MSContainer()
        {
            var c = new ServiceCollection();
            c.AddSingleton(typeof(IQueryById<Lookup>), typeof(LookupQueryById));
            return c;
        }

        [Test]
        public async Task can_execute_request()
        {
            var req = new GetLookup();
            var response = await Service.Any(req) as Lookup;
            Assert.That(response, Is.Not.Null);
        }
    }
}