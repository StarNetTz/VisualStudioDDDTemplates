using System.Threading.Tasks;

namespace DomainName.WebApi.ServiceInterface
{
    public interface IMessageBus
    {
        Task Send(object message);
    }
}
