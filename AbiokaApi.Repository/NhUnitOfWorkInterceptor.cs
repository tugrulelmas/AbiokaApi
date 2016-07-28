using System;
using AbiokaApi.Infrastructure.Common.Dynamic;
using NHibernate;

namespace AbiokaApi.Repository
{
    public class NhUnitOfWorkHandler : IDynamicHandler
    {
        private readonly ISessionFactory _sessionFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="NhUnitOfWorkHandler"/> class.
        /// </summary>
        /// <param name="sessionFactory">The session factory.</param>
        public NhUnitOfWorkHandler(ISessionFactory sessionFactory) {
            _sessionFactory = sessionFactory;
        }

        public void BeforeSend(IRequestContext requestContext) {
            if (UnitOfWork.Current == null)
            {
                UnitOfWork.Current = new UnitOfWork(_sessionFactory);
                UnitOfWork.Current.BeginTransaction();
            }
        }

        public void AfterSend(IResponseContext responseContext) {
            if (UnitOfWork.Current != null)
            {
                UnitOfWork.Current.Commit();
            }
        }

        public void OnException(IExceptionContext exceptionContext) {
            if (UnitOfWork.Current != null)
            {
                UnitOfWork.Current.Rollback();
            }
        }
    }
}
