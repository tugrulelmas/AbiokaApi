using AbiokaApi.Infrastructure.Common.Dynamic;

namespace AbiokaApi.Repository
{
    public class NhUnitOfWorkHandler : IDynamicHandler
    {
        private readonly IUnitOfWork unitOfWork;

        /// <summary>
        /// Initializes a new instance of the <see cref="NhUnitOfWorkHandler"/> class.
        /// </summary>
        /// <param name="sessionFactory">The session factory.</param>
        public NhUnitOfWorkHandler(IUnitOfWork unitOfWork) {
            this.unitOfWork = unitOfWork;
        }

        public void BeforeSend(IRequestContext requestContext) {
            if (UnitOfWork.Current.Session == null)
            {
                UnitOfWork.Current.BeginTransaction();
            }
        }

        public void AfterSend(IResponseContext responseContext) {
            if (UnitOfWork.Current.Session != null)
            {
                UnitOfWork.Current.Commit();
            }
        }

        public void OnException(IExceptionContext exceptionContext) {
            if (UnitOfWork.Current.Session != null)
            {
                UnitOfWork.Current.Rollback();
            }
        }
    }
}
