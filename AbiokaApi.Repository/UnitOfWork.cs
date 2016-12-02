using AbiokaApi.Infrastructure.Common.Helper;
using NHibernate;
using System;

namespace AbiokaApi.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private static readonly string contextName = "_AbiokaUnitOfWorkContext_";
        private readonly ISessionFactory sessionFactory;
        private static IContextHolder _contextHolder;
        private ITransaction transaction;
        private ISession session;

        public bool IsInTransaction => transaction != null && transaction.IsActive;

        public ISession Session => session;

        public static UnitOfWork Current {
            get {
                object obj = _contextHolder.GetData(contextName);
                if (obj == null) {
                    throw new ApplicationException("Abioka unit of work context is empty");
                }
                return (UnitOfWork)obj;
            }
        }

        public UnitOfWork(ISessionFactory sessionFactory, IContextHolder contextHolder) {
            this.sessionFactory = sessionFactory;
            _contextHolder = contextHolder;
            contextHolder.SetData(contextName, this);
        }

        public void BeginTransaction() {
            session = sessionFactory.OpenSession();
            transaction = session.BeginTransaction();
        }

        public void Commit() {
            try
            {
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
            finally
            {
                session.Close();
            }
        }

        public void Rollback() {
            transaction.Rollback();
        }
    }
}
