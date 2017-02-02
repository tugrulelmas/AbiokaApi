using AbiokaApi.ApplicationService.Abstractions;
using AbiokaApi.ApplicationService.Messaging;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace $rootnamespace$.Controllers
{
    [AllowAnonymous]
    [RoutePrefix("api/Installation")]
    public class InstallationController : BaseApiController
    {
        private readonly IInstallationService installationService;

        public InstallationController(IInstallationService installationService) {
            this.installationService = installationService;
        }
        
        [HttpGet]
        [Route("Required")]
        public HttpResponseMessage Required() {
            var result = installationService.IsInstallationRequired();
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpPost]
        public HttpResponseMessage Create([FromBody]CreateApplicationDataRequest createApplicationDataRequest) {
            installationService.CreateApplicationData(createApplicationDataRequest);

            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}
