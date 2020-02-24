using DomainName.ReadModel;
using ServiceStack;

namespace DomainName.WebApi.ServiceModel
{
    [Route("/typeaheads")]
    public class FilterTypeahead : IReturn<PaginatedResult<TypeaheadItem>>
    {
        public string Collection { get; set; }
        public string Qry { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
    }
}
