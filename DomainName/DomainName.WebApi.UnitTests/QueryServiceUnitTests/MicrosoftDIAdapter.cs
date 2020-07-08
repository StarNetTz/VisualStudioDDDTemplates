using Microsoft.Extensions.DependencyInjection;
using ServiceStack;
using ServiceStack.Configuration;

namespace DomainName.WebApi.UnitTests
{
    public class MicrosoftDIAdapter : IContainerAdapter
    {
        readonly ServiceProvider ServiceProvider;

        public MicrosoftDIAdapter(IServiceCollection container)
            => ServiceProvider = container.BuildServiceProvider();

        public T Resolve<T>()
            => ServiceProvider.Resolve<T>();

        public T TryResolve<T>()
        {
            var registration = ServiceProvider.TryResolve<T>();
            return registration == null ? default : registration;
        }
    }
}
