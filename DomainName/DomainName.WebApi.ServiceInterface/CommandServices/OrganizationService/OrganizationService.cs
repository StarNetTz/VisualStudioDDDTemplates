using $ext_projectname$.WebApi.ServiceModel;
using System.Threading.Tasks;

namespace $safeprojectname$
{
    public class OrganizationService : DomainCommandService
    {
        public async Task<object> Any(RegisterOrganization request)
            => await TryProcessRequest<PL.Commands.RegisterOrganization>(request);
    }
}