using DomainName.ReadModel;
using DomainName.WebApi.ServiceModel;
using ServiceStack;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DomainName.WebApi.ServiceInterface
{
    public class OrganizationQueryService : Service
    {
        readonly IOrganizationSmartSearchQuery Query;
        readonly IQueryById QueryById;

        public OrganizationQueryService(IOrganizationSmartSearchQuery query, IQueryById queryById)
        {
            Query = query;
            QueryById = queryById;
        }

        public async Task<object> Any(FindOrganizations req)
        {
            if (!string.IsNullOrEmpty(req.Id))
                return await GetById(req);
            return await PerformSmartSearch(req);
        }

        async Task<object> GetById(FindOrganizations req)
        {
            var c = await QueryById.GetById<Organization>(req.Id);
            return c == null ? new PaginatedResult<Organization>() : new PaginatedResult<Organization>() { PageSize = 1, TotalItems = 1, CurrentPage = 0, TotalPages = 1, Data = new List<Organization>() { c } };
        }

        async Task<object> PerformSmartSearch(FindOrganizations req)
        {
            var smartSearchRequest = req.ConvertTo<SmartSearchQueryRequest>();
            var res = await Query.Execute(smartSearchRequest);
            return res;
        }
    }
}