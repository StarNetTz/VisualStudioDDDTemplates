using DomainName.ReadModel.Repositories.RavenDB;
using Microsoft.Extensions.Configuration;
using Raven.Client.Documents;

namespace DomainName.ReadModel.Repositories.RavenDB.IntegrationTests
{
    public class QueryIntegrationTestBase
    {
        internal readonly IDocumentStore DocumentStore;

        public QueryIntegrationTestBase()
        {
            var conf = new ConfigurationBuilder().AddJsonFile("appsettings.json", true, true).Build();
            var rconf = RavenConfig.FromConfiguration(conf);
            DocumentStore = new RavenDocumentStoreFactory().CreateDocumentStore(rconf);
        }
    }
}