using AbiokaApi.ApplicationService.Abstractions;
using AbiokaApi.ApplicationService.DTOs;
using System.Web.Http;

namespace AbiokaApi.Host.Controllers
{
    [RoutePrefix("api/Role")]
    public class RoleController : BaseCrudController<RoleDTO>
    {
        public RoleController(ICrudService<RoleDTO> service)
            : base(service) {
        }
    }
}
