using AbiokaApi.Infrastructure.Common.ApplicationSettings;
using AbiokaApi.Infrastructure.Common.Helper;
using AbiokaApi.Infrastructure.Common.IoC;

namespace AbiokaApi.Infrastructure.Common
{
    public class Bootstrapper
    {
        public static void Initialise() {
            DependencyContainer.Container
                .Register<IConnectionStringRepository, WebConfigConnectionStringRepository>()
                .Register<IContextHolder, ContextHolder>();
        }
    }
}
