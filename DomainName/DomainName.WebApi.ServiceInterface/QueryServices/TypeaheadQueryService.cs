using DomainName.ReadModel;
using DomainName.WebApi.ServiceModel;
using ServiceStack;
using System.Threading.Tasks;

namespace DomainName.WebApi.ServiceInterface.QueryServices
{
    public class TypeaheadQueryService : Service
    {
        readonly ITypeaheadSmartSearchQuery Query;

        public TypeaheadQueryService(ITypeaheadSmartSearchQuery query)
            => Query = query;

        public async Task<object> Any(FilterTypeahead req)
            => await Query.Execute(req.ConvertTo<SmartSearchQueryRequest>());
    }
}