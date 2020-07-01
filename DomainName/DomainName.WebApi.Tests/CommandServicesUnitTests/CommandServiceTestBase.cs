using DomainName.WebApi.ServiceInterface;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using ServiceStack;
using ServiceStack.Auth;
using ServiceStack.Testing;
using ServiceStack.Validation;

namespace DomainName.WebApi.UnitTests
{
    public class CommandServiceTestBase<T> where T : Service
    {
        protected T Service;
        protected ServiceStackHost AppHost;

        [OneTimeSetUp]
        public void ConfigureAppHost()
        {
            IConfiguration configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json", true, true).Build();
            Licensing.RegisterLicense(configuration["ServiceStack:Licence"]);

            AppHost = new BasicAppHost(typeof(OrganizationService).Assembly)
            {
                TestMode = true,
                ConfigureContainer = container =>
                {
                    container.RegisterAutoWiredAs<StubMessageBus, IMessageBus>();
                    container.RegisterAutoWiredAs<StubTimeProvider, ITimeProvider>();
                    container.Register<IAuthSession>(c => new AuthUserSession
                    {
                        Email = "mensad@mail.com"
                    });
                    container.RegisterValidators(typeof(AddressValidator).Assembly);
                }
            };
            AppHost.Plugins.Add(new ValidationFeature());
            AppHost.Init();
            Service = AppHost.Container.Resolve<T>();
            Service.Request = new MockHttpRequest();
        }

        [OneTimeTearDown]
        public void OneTimeTearDown() => AppHost.Dispose();
    }
}