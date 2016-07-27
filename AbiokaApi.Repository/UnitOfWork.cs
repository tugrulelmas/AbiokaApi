using NHibernate;
using System;

namespace AbiokaApi.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ISessionFactory _sessionFactory;
        private ITransaction _transaction;

        public ISession Session { get; private set; }

        public static UnitOfWork Current {
            get { return _current; }
            set { _current = value; }
        }

        [ThreadStatic]
        private static UnitOfWork _current;

        public UnitOfWork(ISessionFactory sessionFactory) {
            _sessionFactory = sessionFactory;
        }

        public void BeginTransaction() {
            Session = _sessionFactory.OpenSession();
            _transaction = Session.BeginTransaction();
        }

        public void Commit() {
            try
            {
                _transaction.Commit();
            }
            catch
            {
                _transaction.Rollback();
                throw;
            }
            finally
            {
                Session.Close();
            }
        }

        public void Rollback() {
            _transaction.Rollback();
        }
    }
}
