using NHibernate;

namespace AbiokaApi.Repository
{
    internal class DisposableUnitOfWork : IDisposableUnitOfWork
    {
        private readonly ISessionFactory sessionFactory;
        private ITransaction transaction;
        private ISession session;

        public DisposableUnitOfWork(ISessionFactory sessionFactory) {
            this.sessionFactory = sessionFactory;
            session = sessionFactory.OpenSession();
            transaction = session.BeginTransaction();
        }

        public IDisposableUnitOfWork OpenSession() {
            session = sessionFactory.OpenSession();
            transaction = session.BeginTransaction();
            return this;
        }

        public ISession Session => session;

        public void Dispose() {
            if(transaction != null && transaction.IsActive) {
                transaction.Rollback();
            }

            session.Dispose();
        }

        public void Commit() {
            try {
                transaction.Commit();
            }
            catch {
                transaction.Rollback();
                throw;
            }
            finally {
                session.Close();
            }
        }
    }
}
