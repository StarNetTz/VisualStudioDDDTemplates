using System.Threading.Tasks;

namespace DomainName.ReadModel
{
    public interface IQueryById<T>
    {
        Task<T> GetById(string id);
    }
}