using AbiokaApi.ApplicationService.Abstractions;
using AbiokaApi.ApplicationService.Messaging;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AbiokaApi.Host.Controllers
{
    [RoutePrefix("api/Invitation")]
    public class InvitationController : BaseApiController {
        private readonly IInvitationService invitationService;

        public InvitationController(IInvitationService invitationService) {
            this.invitationService = invitationService;
        }

        [HttpGet]
        [Route("Contact")]
        public HttpResponseMessage Contract() {
            var saveInvitaionContactRequest = new SaveInvitaionContactRequest();
            var serviceResult = invitationService.SaveInvitaionContact(saveInvitaionContactRequest);

            var response = Request.CreateResponse(HttpStatusCode.Created, serviceResult.Id);
            return response;
        }
    }
}
