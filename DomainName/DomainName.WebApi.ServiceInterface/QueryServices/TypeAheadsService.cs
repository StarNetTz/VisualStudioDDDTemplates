using DomainName.ReadModel;
using DomainName.WebApi.ServiceModel;
using ServiceStack;
using System.Threading.Tasks;

namespace DomainName.WebApi.ServiceInterface.QueryServices
{
    public class TypeAheadQueryService : Service
    {
        readonly ITypeaheadSmartSearchQuery Query;

        public TypeAheadQueryService(ITypeaheadSmartSearchQuery query)
        {
            Query = query;
        }

        public async Task<object> Any(FilterTypeahead req)
        {
            return await Query.Execute(req.ConvertTo<SmartShearchQueryRequest>());
        }
    }
}