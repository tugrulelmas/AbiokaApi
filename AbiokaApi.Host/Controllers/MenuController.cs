using AbiokaApi.ApplicationService.Abstractions;
using AbiokaApi.ApplicationService.DTOs;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AbiokaApi.Host.Controllers
{
    [RoutePrefix("api/Menu")]
    public class MenuController : BaseCrudController<MenuDTO>
    {
        private readonly IMenuService menuService;

        public MenuController(IMenuService menuService)
            : base(menuService) {
            this.menuService = menuService;
        }


        [Route("filter")]
        [HttpGet]
        public virtual HttpResponseMessage Get([FromUri]string text) {
            var result = menuService.Filter(text);

            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

    }
}
