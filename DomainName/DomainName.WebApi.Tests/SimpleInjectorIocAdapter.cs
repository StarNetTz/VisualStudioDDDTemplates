using ServiceStack.Configuration;
using SimpleInjector;

namespace DomainName.WebApi.Tests
{
    public class SimpleInjectorIocAdapter : IContainerAdapter
    {
        readonly Container Container;

        public SimpleInjectorIocAdapter(Container container)
        {
            Container = container;
        }

        public T Resolve<T>()
            => (T)Container.GetInstance(typeof(T));

        public T TryResolve<T>()
        {
            var registration = Container.GetRegistration(typeof(T));
            return registration == null ? default : (T)registration.GetInstance();
        }
    }
}