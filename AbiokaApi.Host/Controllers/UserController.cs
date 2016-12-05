using AbiokaApi.ApplicationService.Abstractions;
using AbiokaApi.ApplicationService.Messaging;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AbiokaApi.Host.Controllers
{
    [RoutePrefix("api/User")]
    public class UserController : BaseApiController
    {
        private readonly IUserService userService;

        public UserController(IUserService userService) {
            this.userService = userService;
        }

        [HttpGet]
        [Route("")]
        public HttpResponseMessage Get() {
            var users = userService.GetAll();

            var response = Request.CreateResponse(HttpStatusCode.OK, users);
            return response;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("")]
        public HttpResponseMessage Login([FromBody]LoginRequest request) {
            var token = userService.Login(request);

            var response = Request.CreateResponse(HttpStatusCode.OK, token);
            return response;
        }
    }
}
