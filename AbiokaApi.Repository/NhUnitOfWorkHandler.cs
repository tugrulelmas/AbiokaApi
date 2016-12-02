using AbiokaApi.Infrastructure.Common.Dynamic;

namespace AbiokaApi.Repository
{
    public class NhUnitOfWorkHandler : IDynamicHandler
    {
        private readonly IUnitOfWork unitOfWork;

        public short Order => 10;

        /// <summary>
        /// Initializes a new instance of the <see cref="NhUnitOfWorkHandler"/> class.
        /// </summary>
        /// <param name="sessionFactory">The session factory.</param>
        public NhUnitOfWorkHandler(IUnitOfWork unitOfWork) {
            this.unitOfWork = unitOfWork;
        }

        public void BeforeSend(IRequestContext requestContext) {
            if (!unitOfWork.IsInTransaction)
            {
                unitOfWork.BeginTransaction();
            }
        }

        public void AfterSend(IResponseContext responseContext) {
            if (unitOfWork.IsInTransaction)
            {
                unitOfWork.Commit();
            }
        }

        public void OnException(IExceptionContext exceptionContext) {
            if (unitOfWork.IsInTransaction)
            {
                unitOfWork.Rollback();
            }
        }
    }
}
