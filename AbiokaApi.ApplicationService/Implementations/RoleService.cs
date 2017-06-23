using AbiokaApi.ApplicationService.Abstractions;
using AbiokaApi.ApplicationService.DTOs;
using AbiokaApi.Domain;
using AbiokaApi.Domain.Repositories;
using AbiokaApi.Infrastructure.Common.Authentication;
using AbiokaApi.Infrastructure.Common.Domain;

namespace AbiokaApi.ApplicationService.Implementations
{
    public class RoleService : CrudService<Role, RoleDTO>, IRoleService
    {
        public RoleService(IRoleRepository repository, IDTOMapper dtoMapper) 
            : base(repository, dtoMapper) {
        }

        [AllowedRole("Admin")]
        public override void Add(RoleDTO entity) {
            base.Add(entity);
        }

        [AllowedRole("Admin")]
        public override void Delete(object id) {
            base.Delete(id);
        }

        [AllowedRole("Admin")]
        public override void Update(RoleDTO entity) {
            base.Update(entity);
        }

        [AllowedRole("Admin")]
        public override IPage<RoleDTO> GetWithPage(int page, int limit, string order) {
            return base.GetWithPage(page, limit, order);
        }
    }
}
