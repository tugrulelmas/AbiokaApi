using AbiokaApi.Infrastructure.Common.Authentication;
using AbiokaApi.Infrastructure.Common.Dynamic;
using AbiokaApi.Infrastructure.Common.IoC;
using AbiokaApi.Infrastructure.Framework.Authentication;
using AbiokaApi.Infrastructure.Framework.Handlers;
using AbiokaApi.Infrastructure.Framework.IoC;

namespace AbiokaApi.Infrastructure.Framework
{
    public class Bootstrapper
    {
        public static void Initialise() {
            DependencyContainer.Container
                .Register<ServiceInterceptor>(LifeStyle.Transient)
                .Register<IAbiokaToken, AbiokaToken>(isFallback: true)
                .Register<IDynamicHandler, AuthenticationHandler>(LifeStyle.PerWebRequest)
                .Register<IDynamicHandler, ExceptionHandler>();
        }
    }
}
