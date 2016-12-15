using AbiokaApi.Infrastructure.Common.Authentication;
using AbiokaApi.Infrastructure.Common.Dynamic;
using AbiokaApi.Infrastructure.Common.Exceptions;
using AbiokaApi.Infrastructure.Common.Helper;
using System.Linq;
using System.Net.Http;

namespace AbiokaApi.Infrastructure.Framework.Handlers
{
    public class RoleValidationHandler : IDynamicHandler
    {
        private readonly ICurrentContext curruntContext;

        public RoleValidationHandler(ICurrentContext curruntContext) {
            this.curruntContext = curruntContext;
        }

        public short Order => 1;

        public void AfterSend(IResponseContext responseContext) {
        }

        public void BeforeSend(IRequestContext requestContext) {
            var actionDescriptor = requestContext.Request.GetActionDescriptor();
            var actionAttributes = actionDescriptor.GetCustomAttributes<AllowedRoleAttributte>(true);
            var controllerAttributes = actionDescriptor.ControllerDescriptor.GetCustomAttributes<AllowedRoleAttributte>(true);

            if ((actionAttributes == null || actionAttributes.Count == 0) && (controllerAttributes == null || controllerAttributes.Count == 0))
                return;

            if (actionAttributes != null && actionAttributes.Count > 0) {
                var allowedRoles = actionAttributes.First();
                if (curruntContext.Principal.Roles.Any(r => allowedRoles.Roles.Contains(r)))
                    return;
            }

            if ((controllerAttributes != null && controllerAttributes.Count > 0)) {
                var allowedRoles = controllerAttributes.First();
                if (curruntContext.Principal.Roles.Any(r => allowedRoles.Roles.Contains(r)))
                    return;
            }


            throw new DenialException("AccessDenied");
        }

        public void OnException(IExceptionContext exceptionContext) {
        }
    }
}
