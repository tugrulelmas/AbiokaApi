using AbiokaApi.Infrastructure.Common.Domain;
using NHibernate;
using NHibernate.Linq;
using System.Collections.Generic;
using System.Linq;

namespace AbiokaApi.Repository
{
    public class Repository<T> : IRepository<T> where T : IEntity
    {
        protected ISession Session { get { return UnitOfWork.Current.Session; } }

        public void Add(T entity) {
            Session.Save(entity);
        }

        public void Delete(T entity) {
            Session.Delete(entity);
        }

        public T FindById(object id) {
            return Session.Get<T>(id);
        }

        public IEnumerable<T> GetAll() {
            return Session.Query<T>().AsEnumerable();
        }

        public void Update(T entity) {
            Session.Update(entity);
        }
    }
}
