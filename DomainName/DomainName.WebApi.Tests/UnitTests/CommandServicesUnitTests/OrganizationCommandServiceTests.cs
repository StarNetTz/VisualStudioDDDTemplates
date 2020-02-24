using NUnit.Framework;
using ServiceStack;
using DomainName.WebApi.ServiceInterface;
using DomainName.WebApi.ServiceModel;
using System.Threading.Tasks;

namespace DomainName.WebApi.Tests
{
    public class OrganizationCommandServiceTests : CommandServiceTestBase<OrganizationService>
    {
        [Test]
        public async Task can_execute_register_company()
        {
            var response = await Service.Any(new RegisterOrganization { 
                Id = "Organizations-1", 
                Name = "My company", 
                Address = new Address { 
                    City = "Cardiff", 
                    Country = "UK", 
                    State = "Essex",
                    Street = "Baker 223",
                    PostalCode = "9876" }
            }) as ResponseStatus;
            Assert.That(response.Errors, Is.Null);
        }
    }
}