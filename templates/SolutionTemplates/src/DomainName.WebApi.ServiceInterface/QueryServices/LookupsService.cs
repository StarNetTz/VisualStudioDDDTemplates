using $ext_projectname$.ReadModel;
using $ext_projectname$.WebApi.ServiceModel;
using ServiceStack;
using System.Threading.Tasks;

namespace $safeprojectname$
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