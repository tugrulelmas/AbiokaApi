using AbiokaApi.Infrastructure.Common.Authentication;
using AbiokaApi.Infrastructure.Common.Dynamic;
using AbiokaApi.Infrastructure.Common.Helper;
using System.Net.Http;
using System.Web.Http;

namespace AbiokaApi.Infrastructure.Framework.Authentication
{
    public class AuthenticationHandler : IDynamicHandler
    {
        private readonly IAbiokaToken abiokaToken;
        private readonly ICurrentContext currentContext;

        public AuthenticationHandler(IAbiokaToken abiokaToken, ICurrentContext currentContext) {
            this.abiokaToken = abiokaToken;
            this.currentContext = currentContext;
        }

        public short Order => 0;

        public void AfterSend(IResponseContext responseContext) {
        }

        private HttpRequestMessage Request;

        public void BeforeSend(IRequestContext requestContext) {
            SetContextActionType(requestContext.Request);
            currentContext.IP = requestContext.Request.GetClientIpString();

            var actionDescriptor = requestContext.Request.GetActionDescriptor();
            var actionAttributes = actionDescriptor.GetCustomAttributes<AllowAnonymousAttribute>(true);
            var controllerAttributes = actionDescriptor.ControllerDescriptor.GetCustomAttributes<AllowAnonymousAttribute>(true);
            if ((actionAttributes != null && actionAttributes.Count > 0) || (controllerAttributes != null && controllerAttributes.Count > 0)) {
                currentContext.Principal = null;
                return;
            }
            Request = requestContext.Request;

            HttpRequestMessage request = requestContext.Request;

            if (request.Headers.Authorization == null || string.IsNullOrWhiteSpace(request.Headers.Authorization.Parameter)) {
                throw AuthenticationException.MissingCredential;
            }

            if (request.Headers.Authorization.Scheme != "Bearer") {
                throw AuthenticationException.InvalidCredential;
            }

            var payload = abiokaToken.Decode(request.Headers.Authorization.Parameter);

            var user = new CustomPrincipal(payload.id.ToString()) {
                Token = request.Headers.Authorization.Parameter,
                UserName = payload.id.ToString(),
                Email = payload.email,
                Id = payload.id,
                TokenExpirationDate = Util.UnixTimeStampToDateTime(payload.exp),
                Roles = payload.roles,
                Language = payload.language
            };
            currentContext.Principal = user;
        }

        public void OnException(IExceptionContext exceptionContext) {
        }

        private void SetContextActionType(HttpRequestMessage request) {
            if (request.Method.Method == "DELETE") {
                currentContext.ActionType = ActionType.Delete;
            }
            else if (request.Method.Method == "PUT") {
                currentContext.ActionType = ActionType.Update;
            }
            else if (request.Method.Method == "POST") {
                currentContext.ActionType = ActionType.Add;
            }
            else {
                currentContext.ActionType = ActionType.List;
            }
        }
    }
}
