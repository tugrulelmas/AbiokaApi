using AbiokaApi.Domain.Repositories;
using AbiokaApi.Infrastructure.Common.Dynamic;
using AbiokaApi.Infrastructure.Common.IoC;
using AbiokaApi.Repository.Repositories;

namespace AbiokaApi.Repository
{
    public class Bootstrapper
    {
        public static void Initialise() {
            Infrastructure.Common.Bootstrapper.Initialise();

            DependencyContainer.Container
                .UsingFactoryMethod(SessionFactory.CreateNhSessionFactory)
                //.RegisterWithAllInterfaces(typeof(IRepository<>))
                .Register<IInvitationContactRepository, InvitationContactRepository>()
                .Register<IUserRepository, UserRepository>()
                .Register<IUserSecurityRepository, UserSecurityRepository>()
                .Register<IDynamicHandler, NhUnitOfWorkHandler>(LifeStyle.PerWebRequest)
                .Register<IUnitOfWork, UnitOfWork>(LifeStyle.PerWebRequest);
        }
    }
}
