using DomainName.ReadModel;
using DomainName.WebApi.ServiceModel;
using ServiceStack;
using System.Threading.Tasks;

namespace DomainName.WebApi.ServiceInterface
{
    public class LookupsService : Service
    {
        readonly IQueryById QueryById;

        public LookupsService(IQueryById queryById)
        {
            QueryById = queryById;
        }

        public async Task<object> Any(GetLookup req)
            => await QueryById.GetById<Lookup>(req.Id);
    }
}