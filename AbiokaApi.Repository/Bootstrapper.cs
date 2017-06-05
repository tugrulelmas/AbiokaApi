using AbiokaApi.Infrastructure.Common.Domain;
using AbiokaApi.Infrastructure.Common.Dynamic;
using AbiokaApi.Infrastructure.Common.IoC;

namespace AbiokaApi.Repository
{
    public class Bootstrapper
    {
        public static void Initialise() {
            Infrastructure.Common.Bootstrapper.Initialise();

            DependencyContainer.Container
                .UsingFactoryMethod(SessionFactory.CreateNhSessionFactory, true)
                .RegisterWithDefaultInterfaces(typeof(IRepository<>), typeof(Repository<>))
                .Register<IDynamicHandler, NhUnitOfWorkHandler>(LifeStyle.PerWebRequest)
                .Register<IUnitOfWork, UnitOfWork>(LifeStyle.PerWebRequest, true)
                .Register<IDisposableUnitOfWork, DisposableUnitOfWork>(LifeStyle.Transient, true);
        }
    }
}
