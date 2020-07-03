using $ext_projectname$.ReadModel;
using $ext_projectname$.WebApi.ServiceInterface.QueryServices;
using $ext_projectname$.WebApi.ServiceModel;
using NUnit.Framework;
using ServiceStack.Configuration;
using SimpleInjector;
using System.Threading.Tasks;

namespace $safeprojectname$
{
    class LookupsServiceTests : QueryServiceTestBase<LookupsService>
    {
        public override IContainerAdapter CreateContainerAdapter()
            => new SimpleInjectorIocAdapter(SIContainer());

        Container SIContainer()
        {
            var c = new Container();
            c.Register(typeof(IQueryById<Lookup>), typeof(LookupQueryById));
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