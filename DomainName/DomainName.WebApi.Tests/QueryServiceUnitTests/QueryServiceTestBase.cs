using $ext_projectname$.WebApi.ServiceInterface;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using ServiceStack;
using ServiceStack.Auth;
using ServiceStack.Configuration;
using ServiceStack.Testing;
using ServiceStack.Validation;

namespace $safeprojectname$
{
    public abstract class QueryServiceTestBase<T> where T : Service
    {
        protected T Service;
        protected ServiceStackHost AppHost;

        public abstract IContainerAdapter CreateContainerAdapter();

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
                    container.Register<IAuthSession>(c => new AuthUserSession
                    {
                        Email = "mensad@mail.com"
                    });
                    container.RegisterValidators(typeof(AddressValidator).Assembly);
                }
            };
            AppHost.Container.Adapter = CreateContainerAdapter();
            AppHost.Plugins.Add(new ValidationFeature());
            AppHost.Init();
            Service = AppHost.Container.Resolve<T>();
            Service.Request = new MockHttpRequest();
        }

        [OneTimeTearDown]
        public void OneTimeTearDown() => AppHost.Dispose();
    }
}
