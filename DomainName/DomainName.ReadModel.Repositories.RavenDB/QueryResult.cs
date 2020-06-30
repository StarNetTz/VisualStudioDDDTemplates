using Raven.Client.Documents.Session;
using System.Collections.Generic;

namespace DomainName.ReadModel.Repositories.RavenDB
{
    public class QueryResult<T>
    {
        public List<T> Data { get; set; }

        public QueryStatistics Statistics { get; set; }

        public QueryResult()
        {
            Data = new List<T>();
        }
    }
}