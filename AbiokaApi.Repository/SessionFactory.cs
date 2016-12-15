using AbiokaApi.Infrastructure.Common.ApplicationSettings;
using AbiokaApi.Infrastructure.Common.IoC;
using AbiokaApi.Repository.Mappings;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Tool.hbm2ddl;

namespace AbiokaApi.Repository
{
    internal class SessionFactory
    {
        public static ISessionFactory CreateNhSessionFactory() {
            var connectionStringRepository = DependencyContainer.Container.Resolve<IConnectionStringRepository>();
            var sessionFactory = Fluently.Configure()
          .Database(MsSqlConfiguration.MsSql2012.ConnectionString(connectionStringRepository.ReadConnectionString("abioka")))
          .ExposeConfiguration(config => new SchemaUpdate(config).Execute(false, true))
          .Mappings(m => m.FluentMappings.AddFromAssemblyOf<UserMap>())
          .BuildSessionFactory();
            return sessionFactory;
        }
    }
}
