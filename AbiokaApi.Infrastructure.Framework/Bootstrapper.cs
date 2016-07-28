using AbiokaApi.Infrastructure.Common.IoC;
using AbiokaApi.Infrastructure.Framework.IoC;

namespace AbiokaApi.Infrastructure.Framework
{
   public class Bootstrapper
    {
        public static void Initialise() {
            DependencyContainer.Container.Register<DynamicInterceptor>(LifeStyle.Transient);
        }
    }
}
