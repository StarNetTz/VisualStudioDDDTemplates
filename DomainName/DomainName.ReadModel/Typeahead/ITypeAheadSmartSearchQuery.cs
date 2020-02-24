using System.Threading.Tasks;

namespace DomainName.ReadModel
{
    public interface ITypeaheadSmartSearchQuery
    {
        Task<PaginatedResult<TypeaheadItem>> Execute(ISmartSearchQueryRequest qry);
    }
}
