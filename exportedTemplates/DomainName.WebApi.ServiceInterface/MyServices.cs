using System;
using ServiceStack;
using DomainName.WebApi.ServiceModel;

namespace $safeprojectname$
{
    public class MyServices : Service
    {
        public object Any(Hello request)
        {
            return new HelloResponse { Result = $"Hello, {request.Name}!" };
        }
    }
}
