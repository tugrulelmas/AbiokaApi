using AbiokaApi.Infrastructure.Common.Domain;
using AbiokaApi.Repository.DatabaseObjects;
using AbiokaApi.Repository.Mappings;
using NHibernate;
using NHibernate.Linq;
using System.Collections.Generic;
using System.Linq;

namespace AbiokaApi.Repository
{
    internal class Repository<T, TDBEntity> : IRepository<T> 
        where T : IEntity
        where TDBEntity : DBEntity
    {
        protected ISession Session { get { return UnitOfWork.Current.Session; } }

        public void Add(T entity) {
            var dbObject = DBObjectMapper.FromDomainObject(entity);
            Session.Save(dbObject);
            entity = (T)dbObject.ToDomainObject();
        }

        public void Delete(T entity) {
            Session.Delete(entity);
        }

        public T FindById(object id) {
            var dbEntity = Session.Get<TDBEntity>(id);

            if (dbEntity == null)
                return default(T);

            var result = dbEntity.ToDomainObject();
            return (T)result;
        }

        public IEnumerable<T> GetAll() {
            return Session.Query<T>().AsEnumerable();
        }

        public void Update(T entity) {
            Session.Update(entity);
        }
    }
}
