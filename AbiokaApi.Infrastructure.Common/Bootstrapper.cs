using AbiokaApi.Infrastructure.Common.ApplicationSettings;
using AbiokaApi.Infrastructure.Common.Domain;
using AbiokaApi.Infrastructure.Common.Exceptions.Adapters;
using AbiokaApi.Infrastructure.Common.Helper;
using AbiokaApi.Infrastructure.Common.IoC;

namespace AbiokaApi.Infrastructure.Common
{
    public class Bootstrapper
    {
        public static void Initialise() {
            DependencyContainer.Container
                .Register<IConfigurationManager, WebConfigManager>(isFallback: true)
                .Register<IContextHolder, ContextHolder>(isFallback: true)
                .Register<IExceptionAdapterFactory, ExceptionAdapterFactory>()
                .Register<ICurrentContext, CurrentContext>(LifeStyle.PerWebRequest, true)
                .Register<IEventDispatcher, EventDispatcher>(isFallback: true);
        }
    }
}
