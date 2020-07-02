using $ext_projectname$.ReadModel;
using $ext_projectname$.WebApi.ServiceModel;
using ServiceStack;
using System.Threading.Tasks;

namespace $safeprojectname$
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