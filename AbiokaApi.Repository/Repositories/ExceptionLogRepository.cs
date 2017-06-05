using AbiokaApi.Domain.Repositories;
using AbiokaApi.Infrastructure.Common.Exceptions;
using AbiokaApi.Infrastructure.Common.IoC;

namespace AbiokaApi.Repository.Repositories
{
    public class ExceptionLogRepository : Repository<ExceptionLog>, IExceptionLogRepository
    {
        public override void Add(ExceptionLog exceptionLog) {
            using (var unitOfWork = DependencyContainer.Container.Resolve<IDisposableUnitOfWork>()) {
                Add(unitOfWork.Session, exceptionLog);

                unitOfWork.Commit();
            }
        }
    }
}
