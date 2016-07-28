using AbiokaApi.Domain;
using AbiokaApi.Domain.Repositories;
using AbiokaApi.Infrastructure.Common.ApplicationSettings;
using AbiokaApi.Infrastructure.Common.Dynamic;
using AbiokaApi.Infrastructure.Common.IoC;
using AbiokaApi.Repository.Repositories;

namespace AbiokaApi.Repository
{
    public class Bootstrapper
    {
        public static void Initialise() {
            var a = new InvitationContact();
            DependencyContainer.Container
                .Register<IConnectionStringRepository, WebConfigConnectionStringRepository>()
                .UsingFactoryMethod(SessionFactory.CreateNhSessionFactory)
                //.RegisterWithAllInterfaces(typeof(IRepository<>))
                .Register<IInvitationContactRepository, InvitationContactRepository>()
                .Register<IDynamicHandler, NhUnitOfWorkHandler>(LifeStyle.Transient)
                .Register<IUnitOfWork, UnitOfWork>(LifeStyle.Transient);
        }
    }
}
