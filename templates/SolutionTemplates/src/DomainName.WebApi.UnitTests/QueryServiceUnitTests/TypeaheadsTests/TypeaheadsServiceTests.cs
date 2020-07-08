using $ext_projectname$.ReadModel;
using $ext_projectname$.WebApi.ServiceInterface;
using $ext_projectname$.WebApi.ServiceModel;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using ServiceStack.Configuration;
using System.Threading.Tasks;

namespace $safeprojectname$
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