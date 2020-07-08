using $ext_projectname$.ReadModel;
using $ext_projectname$.WebApi.ServiceInterface;
using $ext_projectname$.WebApi.ServiceModel;
using NUnit.Framework;
using ServiceStack.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace $safeprojectname$
{
    public class OrganizationQueryServiceTests : QueryServiceTestBase<OrganizationQueryService>
    {
        public override IContainerAdapter CreateContainerAdapter()
            => new MicrosoftDIAdapter(MSContainer());

        IServiceCollection MSContainer()
        {
            var container = new ServiceCollection();
            container.AddSingleton<IOrganizationSmartSearchQuery, StubOrganizationSmartSearchQuery>();
            container.AddSingleton(typeof(IQueryById<>), typeof(StubQueryById<>));
            return container;
        }

        [Test]
        public async Task can_smart_search()
        {
            var response = await Service.Any(new FindOrganizations { CurrentPage = 0, PageSize = 10, Qry = "*" }) as PaginatedResult<Organization>;
            Assert.That(response, Is.Null);
        }

        [Test]
        public async Task can_get_by_id()
        {
            var response = await Service.Any(new FindOrganizations { Id = "Organizations-1" }) as PaginatedResult<Organization>;
            Assert.That(response.Data, Is.Null);
        }
    }
}