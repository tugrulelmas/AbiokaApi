using AbiokaApi.ApplicationService.Abstractions;
using AbiokaApi.Domain;
using AbiokaApi.Infrastructure.Common.Domain;

namespace AbiokaApi.ApplicationService.Implementations
{
    public class RoleService : CrudService<Role>, IRoleService
    {
        public RoleService(IRepository<Role> repository) 
            : base(repository) {
        }
    }
}
