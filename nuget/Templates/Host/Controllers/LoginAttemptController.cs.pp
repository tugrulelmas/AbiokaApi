using AbiokaApi.ApplicationService.Abstractions;
using AbiokaApi.ApplicationService.DTOs;
using System.Web.Http;

namespace $rootnamespace$.Controllers
{
    [RoutePrefix("api/LoginAttempt")]
    public class LoginAttemptController : BaseReadController<LoginAttemptDTO>
    {
        public LoginAttemptController(IReadService<LoginAttemptDTO> service)
            : base(service) {
        }
    }
}
