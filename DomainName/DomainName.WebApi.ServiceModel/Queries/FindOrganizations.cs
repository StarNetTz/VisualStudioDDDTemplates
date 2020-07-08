using DomainName.ReadModel;
using ServiceStack;

namespace DomainName.WebApi.ServiceModel
{
    [Route("/organizations", Verbs = "GET")]
    public class FindOrganizations : IReturn<PaginatedResult<Organization>>
    {
        public string Id { get; set; }
        public string Qry { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
    }
}
