using AbiokaApi.Domain;
using AbiokaApi.Infrastructure.Common.Domain;
using AbiokaApi.Infrastructure.Common.Dynamic;
using AbiokaApi.Infrastructure.Common.IoC;
using AbiokaApi.Repository.EventHandlers;
using AbiokaApi.Repository.Repositories;

namespace AbiokaApi.Repository
{
    public class Bootstrapper
    {
        public static void Initialise() {
            Infrastructure.Common.Bootstrapper.Initialise();

            DependencyContainer.Container
                .UsingFactoryMethod(SessionFactory.CreateNhSessionFactory)
                .RegisterWithDefaultInterfaces(typeof(IRepository<>), typeof(Repository<>))
                .RegisterWithBase(typeof(IEventHandler<>), typeof(RoleAddedToUserHandler))
                .Register<IRepository<LoginAttempt>, LoginAttemptRepository>()
                .Register<IDynamicHandler, NhUnitOfWorkHandler>(LifeStyle.PerWebRequest)
                .Register<IUnitOfWork, UnitOfWork>(LifeStyle.PerWebRequest)
                .Register<IDisposableUnitOfWork, DisposableUnitOfWork>(LifeStyle.Transient);
        }
    }
}
