using System.Threading.Tasks;

namespace DomainName.ReadModel
{
    public interface IOrganizationSmartSearchQuery
    {
        Task<PaginatedResult<Organization>> Execute(ISmartSearchQueryRequest qry);
    }
}
