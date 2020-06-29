using ServiceStack;
using DomainName.WebApi.ServiceModel;

namespace DomainName.WebApi.ServiceInterface
{
    public class MyServices : Service
    {
        public object Any(Hello request)
            => new HelloResponse { Result = $"Hello, {request.Name}!" };
    }
}
