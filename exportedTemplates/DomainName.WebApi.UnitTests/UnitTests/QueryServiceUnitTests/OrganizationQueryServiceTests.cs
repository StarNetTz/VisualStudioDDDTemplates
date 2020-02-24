using DomainName.ReadModel;
using DomainName.WebApi.ServiceInterface.QueryServices;
using DomainName.WebApi.ServiceModel;
using NUnit.Framework;
using ServiceStack;
using ServiceStack.Testing;
using SimpleInjector;
using System.Threading.Tasks;

namespace DomainName.WebApi.Tests
{
    public class OrganizationQueryServiceTests
    {
        private readonly ServiceStackHost AppHost;

        public OrganizationQueryServiceTests()
        {
            AppHost = new BasicAppHost().Init();
            AppHost.Container.Adapter = new SimpleInjectorIocAdapter(SetupSimpleInjectorContainer());
        }

        static Container SetupSimpleInjectorContainer()
        {
            Container simpleContainer = new Container();
            simpleContainer.Register<IOrganizationSmartSearchQuery, StubOrganizationSmartSearchQuery>();
            simpleContainer.Register<OrganizationQueryService>();
            simpleContainer.Register(typeof(IQueryById<>), typeof(StubQueryById<>));
            return simpleContainer;
        }

        [OneTimeTearDown]
        public void OneTimeTearDown() => AppHost.Dispose();

        [Test]
        public async Task can_smart_search()
        {
            var service = AppHost.Container.Resolve<OrganizationQueryService>();
            var response = await service.Any(new FindOrganizations { CurrentPage = 0, PageSize = 10, Qry = "*" }) as PaginatedResult<Organization>;
            Assert.That(response, Is.Null);
        }

        [Test]
        public async Task can_get_by_id()
        {
            var service = AppHost.Container.Resolve<OrganizationQueryService>();
            var response = await service.Any(new FindOrganizations { Id = "Organizations-1" }) as PaginatedResult<Organization>;
            Assert.That(response.Data, Is.Null);
        }

        class StubOrganizationSmartSearchQuery : IOrganizationSmartSearchQuery
        {
            public Task<PaginatedResult<Organization>> Execute(ISmartSearchQueryRequest qry)
            {
                return Task.FromResult<PaginatedResult<Organization>>(null);
            }
        }
    }
}