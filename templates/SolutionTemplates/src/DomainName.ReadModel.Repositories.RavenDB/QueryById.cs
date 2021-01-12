using Raven.Client.Documents;
using System.Threading.Tasks;

namespace $safeprojectname$
{
    public class QueryById : IQueryById
    {
        protected readonly IDocumentStore DocumentStore;

        public QueryById(IDocumentStore documentStore)
            => DocumentStore = documentStore;

        public async Task<T> GetById<T>(string id)
        {
            using (var ses = DocumentStore.OpenAsyncSession())
                return await ses.LoadAsync<T>(id);
        }
    }
}
