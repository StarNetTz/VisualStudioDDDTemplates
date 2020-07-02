using NUnit.Framework;
using System.Threading.Tasks;

namespace $safeprojectname$
{
    class OrganizationSmartSearchQueryIntegrationTests : QueryIntegrationTestBase
    {
        [Test]
        public async Task CanExecute()
        {
            var qry = new OrganizationSmartSearchQuery(DocumentStore);
            var res = await qry.Execute(new SmartSearchQueryRequest { Qry = "*", CurrentPage = 0, PageSize = 1 });
        }
    }
}