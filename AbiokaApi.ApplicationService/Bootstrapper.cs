using AbiokaApi.ApplicationService.Abstractions;
using AbiokaApi.ApplicationService.DTOs;
using AbiokaApi.ApplicationService.Implementations;
using AbiokaApi.ApplicationService.Interceptors;
using AbiokaApi.ApplicationService.Validation;
using AbiokaApi.Domain;
using AbiokaApi.Infrastructure.Common.IoC;

namespace AbiokaApi.ApplicationService
{
    public class Bootstrapper
    {
        public static void Initialise() {
            Repository.Bootstrapper.Initialise();
            DependencyContainer.Container
                .RegisterServices<IService>()
                .RegisterService<ICrudService<RoleDTO>, CrudService<Role, RoleDTO>>()
                .RegisterService<IReadService<LoginAttemptDTO>, ReadService<LoginAttempt, LoginAttemptDTO>>()
                .RegisterWithBase(typeof(ICustomValidator<>), typeof(CustomValidator<>))
                .Register<IServiceInterceptor, RoleValidationInterceptor>()
                .Register<IServiceInterceptor, DataValidationInterceptor>();
        }
    }
}
