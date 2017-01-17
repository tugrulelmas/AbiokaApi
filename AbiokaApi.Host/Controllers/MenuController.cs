using AbiokaApi.ApplicationService.Abstractions;
using AbiokaApi.ApplicationService.DTOs;
using System.Web.Http;

namespace AbiokaApi.Host.Controllers
{
    [RoutePrefix("api/Menu")]
    [AllowAnonymous]
    public class MenuController : BaseCrudController<MenuDTO>
    {
        private readonly IMenuService menuService;

        public MenuController(IMenuService menuService)
            : base(menuService) {
            this.menuService = menuService;
        }
    }
}
