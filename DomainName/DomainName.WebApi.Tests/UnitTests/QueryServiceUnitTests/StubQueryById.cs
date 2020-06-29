using DomainName.ReadModel;
using System.Threading.Tasks;

namespace DomainName.WebApi.Tests
{
    public class StubQueryById<T> : IQueryById<T>
    {
        public Task<T> GetById(string id)
            => Task.FromResult(default(T));
    }
}