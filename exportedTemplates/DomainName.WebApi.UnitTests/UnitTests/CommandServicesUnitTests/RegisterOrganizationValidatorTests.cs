using NUnit.Framework;
using $ext_projectname$.WebApi.ServiceInterface;
using $ext_projectname$.WebApi.ServiceModel;

namespace $safeprojectname$.WebApi.Tests
{
    public class RegisterOrganizationValidatorTests : ValidatorTestBase<RegisterOrganization>
    {
        public RegisterOrganizationValidatorTests() : base(new RegisterOrganizationValidator()) { }

        [TestCase("Id", "NotEmpty")]
        [TestCase("Name", "NotEmpty")]
        [TestCase("Address", "NotEmpty")]
        public void property_is_required(string property, string errorCode)
        {
            var obj = new RegisterOrganization();
            AssertRuleBroken(obj, property, errorCode);
        }

        [Test]
        public void name_cannot_be_less_than_2_characters_long()
        {
            var obj = new RegisterOrganization { Name = "1" };
            AssertRuleBroken(obj, "Name", "Length");
        }

        [Test]
        public void name_cannot_be_more_than_150_characters_long()
        {
            var obj = new RegisterOrganization { Name = CreateStringOfLength(151) };
            AssertRuleBroken(obj, "Name", "Length");
        }
    }
}