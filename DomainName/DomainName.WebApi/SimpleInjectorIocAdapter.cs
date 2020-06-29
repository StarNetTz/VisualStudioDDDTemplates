using ServiceStack.Configuration;

namespace DomainName.WebApi
{
    public class SimpleInjectorIocAdapter : IContainerAdapter
    {
        readonly SimpleInjector.Container Container;

        public SimpleInjectorIocAdapter(SimpleInjector.Container container)
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
