using AbiokaApi.ApplicationService.Abstractions;
using AbiokaApi.ApplicationService.DTOs;
using System.Web.Http;

namespace $rootnamespace$.Controllers
{
    [RoutePrefix("api/Role")]
    public class RoleController : BaseCrudController<RoleDTO>
    {
        public RoleController(ICrudService<RoleDTO> service)
            : base(service) {
        }
    }
}
