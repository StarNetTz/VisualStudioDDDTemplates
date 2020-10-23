using System.Threading.Tasks;

namespace $safeprojectname$
{
    public interface IQueryById
    {
        Task<T> GetById<T>(string id);
    }
}