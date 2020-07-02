using NUnit.Framework;
using $ext_projectname$.WebApi.ServiceInterface;
using $ext_projectname$.WebApi.ServiceModel;
using System.Threading.Tasks;

namespace $safeprojectname$
{
    public class RegisterOrganizationValidatorTests : ValidatorTestBase<RegisterOrganization>
    {
        public RegisterOrganizationValidatorTests() : base(new RegisterOrganizationValidator()) { }

        [TestCase("Id", "NotEmpty")]
        [TestCase("Name", "NotEmpty")]
        [TestCase("Address", "NotEmpty")]
        public async Task property_is_required(string property, string errorCode)
        {
            var obj = new RegisterOrganization();
            await AssertRuleBroken(obj, property, errorCode);
        }

        [Test]
        public async Task name_cannot_be_less_than_2_characters_long()
        {
            var obj = new RegisterOrganization { Name = "1" };
            await AssertRuleBroken(obj, "Name", "Length");
        }

        [Test]
        public async Task name_cannot_be_more_than_150_characters_long()
        {
            var obj = new RegisterOrganization { Name = CreateStringOfLength(151) };
            await AssertRuleBroken(obj, "Name", "Length");
        }
    }
}