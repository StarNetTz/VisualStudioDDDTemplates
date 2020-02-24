using System;
using System.Threading.Tasks;

namespace DomainName.WebApi.ServiceInterface
{
    public interface IIdentityProvider
    {
        Task<string> GetId(string aggregateName);
    }
}
