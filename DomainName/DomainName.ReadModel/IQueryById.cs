using System.Threading.Tasks;

namespace DomainName.ReadModel
{
    public interface IQueryById
    {
        Task<T> GetById<T>(string id);
    }
}