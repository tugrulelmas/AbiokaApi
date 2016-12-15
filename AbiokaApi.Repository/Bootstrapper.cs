using AbiokaApi.Domain;
using AbiokaApi.Infrastructure.Common.Domain;
using AbiokaApi.Infrastructure.Common.Dynamic;
using AbiokaApi.Infrastructure.Common.IoC;
using AbiokaApi.Repository.DatabaseObjects;

namespace AbiokaApi.Repository
{
    public class Bootstrapper
    {
        public static void Initialise() {
            Infrastructure.Common.Bootstrapper.Initialise();

            DependencyContainer.Container
                .UsingFactoryMethod(SessionFactory.CreateNhSessionFactory)
                .RegisterWithDefaultInterfaces(typeof(IRepository<>), typeof(Repository<,>))
                .Register<IRepository<Role>, Repository<Role, RoleDB>>()
                .Register<IDynamicHandler, NhUnitOfWorkHandler>(LifeStyle.PerWebRequest)
                .Register<IUnitOfWork, UnitOfWork>(LifeStyle.PerWebRequest);
        }
    }
}
