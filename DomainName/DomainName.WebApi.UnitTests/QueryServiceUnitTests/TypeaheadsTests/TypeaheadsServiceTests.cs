using DomainName.ReadModel;
using DomainName.WebApi.ServiceInterface;
using DomainName.WebApi.ServiceModel;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using ServiceStack.Configuration;
using System.Threading.Tasks;

namespace DomainName.WebApi.UnitTests
{
    class TypeaheadsServiceTests : QueryServiceTestBase<TypeaheadQueryService>
    {
        public override IContainerAdapter CreateContainerAdapter()
            => new MicrosoftDIAdapter(MSContainer());

        IServiceCollection MSContainer()
        {
            var c = new ServiceCollection();
            c.AddSingleton<ITypeaheadSmartSearchQuery, StubTypeaheadSmartSearchQuery>();
            return c;
        }

        [Test]
        public async Task can_execute_request()
        {
            var req = new FilterTypeahead();
            var response = await Service.Any(req) as PaginatedResult<TypeaheadItem>;
            Assert.That(response, Is.Not.Null);
        }
    }
}