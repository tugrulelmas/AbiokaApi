using AbiokaApi.Infrastructure.Common.IoC;
using AbiokaApi.Infrastructure.Framework.IoC;
using System.Reflection;
using System.Linq;
using System.Web.Http;

namespace $rootnamespace$
{
    public class Bootstrapper
    {
        public static void Initialise() {
            DependencyContainer.SetContainer(new CastleContainer());
            Infrastructure.Framework.Bootstrapper.Initialise();
            ApplicationService.Bootstrapper.Initialise();

            var controllerTypes =
                from t in Assembly.GetExecutingAssembly().GetTypes()
                where typeof(ApiController).IsAssignableFrom(t)
                select t;

            foreach (var t in controllerTypes)
            {
                DependencyContainer.Container.Register(t, LifeStyle.Transient);
            }
        }
    }
}