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

        [Route("")]
        [HttpGet]
        public HttpResponseMessage Get([FromUri]int page, [FromUri]int limit, [FromUri]string order) {
            var result = userService.GetWithPage(page, limit, order);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("Login")]
        public HttpResponseMessage Login([FromBody]LoginRequest request) {
            var token = userService.Login(request);

            var response = Request.CreateResponse(HttpStatusCode.OK, token);
            return response;
        }
        
        [HttpDelete]
        [Route("{id}")]
        public HttpResponseMessage Delete([FromUri]Guid id) {
            userService.Delete(id);

            return Request.CreateResponse(HttpStatusCode.OK);
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
    }
}
