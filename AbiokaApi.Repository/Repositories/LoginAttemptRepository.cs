using AbiokaApi.Domain;
using AbiokaApi.Domain.Repositories;
using AbiokaApi.Infrastructure.Common.IoC;
using AbiokaApi.Repository.DatabaseObjects;

namespace AbiokaApi.Repository.Repositories
{
    public class LoginAttemptRepository : Repository<LoginAttempt, LoginAttemptDB>, ILoginAttemptRepository
    {
        public override void Add(LoginAttempt entity) {
            using (var unitOfWork = DependencyContainer.Container.Resolve<IDisposableUnitOfWork>()) {
                Add(unitOfWork.Session, entity);

                unitOfWork.Commit();
            }
        }

    }
}
