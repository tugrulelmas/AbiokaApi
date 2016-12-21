using AbiokaApi.ApplicationService.Abstractions;
using AbiokaApi.Domain;
using System.Web.Http;

namespace AbiokaApi.Host.Controllers
{
    [RoutePrefix("api/LoginAttempt")]
    public class LoginAttemptController : BaseReadController<LoginAttempt>
    {
        public LoginAttemptController(IReadService<LoginAttempt> service)
            : base(service) {
        }
    }
}
