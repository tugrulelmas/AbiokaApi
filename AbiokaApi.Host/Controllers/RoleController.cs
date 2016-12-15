using AbiokaApi.ApplicationService.Abstractions;
using AbiokaApi.Domain;
using System.Web.Http;

namespace AbiokaApi.Host.Controllers
{
    [RoutePrefix("api/Role")]
    public class RoleController : BaseCrudController<Role>
    {
        public RoleController(IRoleService service)
            : base(service) {
        }
    }
}
