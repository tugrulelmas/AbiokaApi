using AbiokaApi.Infrastructure.Common.Domain;
using AbiokaApi.Infrastructure.Common.IoC;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbiokaApi.Repository
{
    public class Bootstrapper
    {
        public static void Initialise() {
            // DependencyContainer.Container.Register<NhUnitOfWorkInterceptor>()
            DependencyContainer.Container.UsingFactoryMethod(SessionFactory.CreateNhSessionFactory)
                .RegisterWithAllInterfaces(typeof(IRepository<>))
                .RegisterPerWebRequest<IUnitOfWork, UnitOfWork>();

            /*
            container.Register(
                Component.For<NhUnitOfWorkInterceptor>().LifeStyle.Transient,
                Classes.FromThisAssembly().BasedOn(typeof(IService)).WithServiceAllInterfaces().Configure(c => c.LifeStyle.Singleton.Interceptors<NhUnitOfWorkInterceptor>()),
                );

            container.Kernel.Resolver.AddSubResolver(new CollectionResolver(container.Kernel, true));
            */
        }
    }
}
