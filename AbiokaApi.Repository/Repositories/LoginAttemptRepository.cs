using AbiokaApi.Domain;
using AbiokaApi.Domain.Repositories;
using AbiokaApi.Infrastructure.Common.Domain;
using AbiokaApi.Infrastructure.Common.Helper;
using AbiokaApi.Infrastructure.Common.IoC;
using AbiokaApi.Repository.DatabaseObjects;

namespace AbiokaApi.Repository.Repositories
{
    public class LoginAttemptRepository : Repository<LoginAttempt, LoginAttemptDB>, ILoginAttemptRepository
    {
        private readonly ICurrentContext currentContext;

        public LoginAttemptRepository(ICurrentContext currentContext) {
            this.currentContext = currentContext;
        }

        public override void Add(LoginAttempt entity) {
            using (var unitOfWork = DependencyContainer.Container.Resolve<IDisposableUnitOfWork>()) {
                Add(unitOfWork.Session, entity);

                unitOfWork.Commit();
            }
        }

        public override IPage<LoginAttempt> GetPage(PageRequest pageRequest) {
            if (currentContext.Current.Principal.IsInRole("Admin")) {
                return base.GetPage(pageRequest);
            }

            return GetPage(pageRequest, la => la.User.Id == currentContext.Current.Principal.Id);
        }
    }
}
