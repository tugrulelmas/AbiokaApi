using AbiokaApi.ApplicationService.Authentication;
using AbiokaApi.ApplicationService.Messaging;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Linq;
using AbiokaApi.Infrastructure.Common.Exceptions;
using AbiokaApi.Infrastructure.Common.Authentication;

namespace AbiokaApi.Host.Controllers
{
    [AllowAnonymous]
    [RoutePrefix("api/Auth")]
    public class AuthController : BaseApiController
    {
        private readonly IEnumerable<IAuthService> authServices;

        public AuthController(IEnumerable<IAuthService> authServices) {
            this.authServices = authServices;
        }

        [HttpPost]
        [Route("login")]
        public async Task<HttpResponseMessage> Login([FromBody]AuthRequest request) {
            var authService = authServices.Where(a => a.Provider == request.provider).FirstOrDefault();
            if (authService == null)
                throw new DenialException($"Invalid provider: {request.provider}");

            var token = await authService.LoginAsync(request);

            var response = Request.CreateResponse(HttpStatusCode.OK, token);
            return response;
        }

        [HttpPost]
        [Route("refreshToken")]
        public async Task<HttpResponseMessage> Login([FromUri]string refreshToken, [FromUri]AuthProvider provider) {
            var authService = authServices.Where(a => a.Provider == provider).FirstOrDefault();
            if (authService == null)
                throw new DenialException($"Invalid provider: {provider}");

            var token = await authService.RefreshTokenAsync(refreshToken);

            var response = Request.CreateResponse(HttpStatusCode.OK, token);
            return response;
        }
    }
}
