using System;
using ServiceStack;
using DomainName.WebApi.ServiceModel;

namespace DomainName.WebApi.ServiceInterface
{
    public class MyServices : Service
    {
        public object Any(Hello request)
        {
            return new HelloResponse { Result = $"Hello, {request.Name}!" };
        }
    }
}
