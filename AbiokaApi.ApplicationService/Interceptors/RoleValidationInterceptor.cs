using AbiokaApi.Infrastructure.Common.Authentication;
using AbiokaApi.Infrastructure.Common.Exceptions;
using AbiokaApi.Infrastructure.Common.Helper;
using AbiokaApi.Infrastructure.Common.IoC;
using System.Linq;

namespace AbiokaApi.ApplicationService.Interceptors
{
    internal class RoleValidationInterceptor : IServiceInterceptor
    {
        private readonly ICurrentContext currentContext;

        public RoleValidationInterceptor(ICurrentContext currentContext) {
            this.currentContext = currentContext;
        }

        public int Order => 0;

        public void BeforeProceed(IInvocationContext context) {
            var attributes = context.Method.GetCustomAttributes(typeof(AllowedRole), true);
            if (attributes == null || attributes.Count() == 0)
                return;


            var allowedRoles = (AllowedRole)attributes.First();
            if (currentContext.Current.Principal.Roles.Any(r => allowedRoles.Roles.Contains(r)))
                return;

            throw new DenialException("AccessDenied");
        }
    }
}
