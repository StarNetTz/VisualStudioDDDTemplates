namespace DomainName.PL.Commands
{
    public class RegisterOrganization : Command
    {
        public string Name { get; set; }
        public Address Address { get; set; }
    }

    public class CorrectOrganizationName : Command
    {
        public string Name { get; set; }
    }
}