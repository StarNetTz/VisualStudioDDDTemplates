using DomainName.PL;
namespace DomainName.ReadModel
{
    public class Organization
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public Address Address { get; set; }
        public string VATId { get; set; }
    }
}
