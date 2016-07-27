using AbiokaApi.Repository.DatabaseObjects;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Tool.hbm2ddl;

namespace AbiokaApi.Repository
{
    internal class SessionFactory
    {
        public static ISessionFactory CreateNhSessionFactory() {
            var sessionFactory = Fluently.Configure()
          .Database(MsSqlConfiguration.MsSql2012.ConnectionString("Data Source=.\\SQLEXPRESS;User Id=sa;Password=sapass;Initial Catalog=TestAbioka;"))
          .ExposeConfiguration(config => new SchemaUpdate(config).Execute(false, true))
          .Mappings(m => m.FluentMappings.AddFromAssemblyOf<DBEntity>())
          .BuildSessionFactory();
            return sessionFactory;
        }
    }
}
