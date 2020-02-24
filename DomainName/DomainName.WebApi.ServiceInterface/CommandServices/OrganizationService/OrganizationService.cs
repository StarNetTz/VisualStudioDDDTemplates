using DomainName.WebApi.ServiceModel;
using ServiceStack;
using ServiceStack.FluentValidation;
using System;
using System.Threading.Tasks;

namespace DomainName.WebApi.ServiceInterface
{
    public class OrganizationService : DomainCommandService
    {
        public async Task<object> Any(RegisterOrganization request)
        {
            return await TryProcessRequest<PL.Commands.RegisterOrganization>(request);
        }
    }

    public class RegisterOrganizationValidator : AbstractValidator<RegisterOrganization>
    {
        public RegisterOrganizationValidator()
        {
            RuleFor(c => c.Id).NotEmpty().Matches("Organizations-\\w");
            RuleFor(c => c.Name).NotEmpty().Length(2, 150);
            RuleFor(c => c.Address).NotEmpty().SetValidator(new AddressValidator());
        }
    }
}