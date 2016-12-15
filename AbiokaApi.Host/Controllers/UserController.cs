using AbiokaApi.ApplicationService.Abstractions;
using AbiokaApi.ApplicationService.Messaging;
using AbiokaApi.Domain;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AbiokaApi.Host.Controllers
{
    [RoutePrefix("api/User")]
    public class UserController : BaseReadController<User>
    {
        private readonly IUserService userService;

        public UserController(IUserService userService)
            : base(userService) {
            this.userService = userService;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("Login")]
        public HttpResponseMessage Login([FromBody]LoginRequest request) {
            var token = userService.Login(request);

            var response = Request.CreateResponse(HttpStatusCode.OK, token);
            return response;
        }

        [HttpPost]
        [Route("")]
        public HttpResponseMessage Add([FromBody]AddUserRequest request) {
            var user = userService.Add(request);

            return Request.CreateResponse(HttpStatusCode.Created, user);
        }

        [HttpPut]
        [Route("")]
        public HttpResponseMessage Update([FromBody]User user) {
            userService.Update(user);

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [HttpDelete]
        [Route("{id}")]
        public HttpResponseMessage Delete([FromUri]Guid id) {
            userService.Delete(id);

            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}
