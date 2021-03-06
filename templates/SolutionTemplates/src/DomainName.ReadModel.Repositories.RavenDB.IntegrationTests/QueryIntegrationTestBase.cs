﻿using $ext_projectname$.ReadModel.Repositories.RavenDB;
using Microsoft.Extensions.Configuration;
using Raven.Client.Documents;

namespace $safeprojectname$
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