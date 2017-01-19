using AbiokaApi.ApplicationService.Abstractions;
using AbiokaApi.ApplicationService.DTOs;
using AbiokaApi.ApplicationService.Messaging;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AbiokaApi.Host.Controllers
{
    [RoutePrefix("api/User")]
    public class UserController : BaseReadController<UserDTO>
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

        [AllowAnonymous]
        [HttpPost]
        [Route("register")]
        public HttpResponseMessage Register([FromBody]RegisterUserRequest request) {
            var user = userService.Register(request);

            return Request.CreateResponse(HttpStatusCode.Created, user);
        }

        [HttpPut]
        [Route("")]
        public HttpResponseMessage Update([FromBody]UserDTO user) {
            userService.Update(user);

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [HttpDelete]
        [Route("{id}")]
        public HttpResponseMessage Delete([FromUri]Guid id) {
            userService.Delete(id);

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("RefreshToken/{refreshToken}")]
        public HttpResponseMessage RefreshToken([FromUri]string refreshToken) {
            var token = userService.RefreshToken(refreshToken);

            return Request.CreateResponse(HttpStatusCode.OK, token);
        }

        [HttpPut]
        [Route("{id}/ChangePassword")]
        public HttpResponseMessage ChangePassword([FromUri]Guid id, [FromBody]ChangePasswordRequest request) {
            var newToken = userService.ChangePassword(request);

            return Request.CreateResponse(HttpStatusCode.OK, newToken);
        }

        [HttpPut]
        [Route("{id}/ChangeLanguage")]
        public HttpResponseMessage ChangeLanguage([FromUri]Guid id, [FromUri]string language) {
            userService.ChangeLanguage(language);

            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}
