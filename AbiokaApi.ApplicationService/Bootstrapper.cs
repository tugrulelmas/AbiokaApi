using AbiokaApi.ApplicationService.Abstractions;
using AbiokaApi.ApplicationService.DTOs;
using AbiokaApi.ApplicationService.EventHandlers;
using AbiokaApi.ApplicationService.Implementations;
using AbiokaApi.ApplicationService.Interceptors;
using AbiokaApi.ApplicationService.Messaging;
using AbiokaApi.ApplicationService.Validation;
using AbiokaApi.Domain;
using AbiokaApi.Infrastructure.Common.Domain;
using AbiokaApi.Infrastructure.Common.IoC;

namespace AbiokaApi.ApplicationService
{
    public class Bootstrapper
    {
        public static void Initialise() {
            Repository.Bootstrapper.Initialise();
            DependencyContainer.Container
                .RegisterServices<IService, IService>()
                .RegisterService<ICrudService<RoleDTO>, CrudService<Role, RoleDTO>>()
                .RegisterService<IReadService<LoginAttemptDTO>, ReadService<LoginAttempt, LoginAttemptDTO>>()
                .RegisterWithBase(typeof(ICustomValidator<>), typeof(CustomValidator<>))
                .Register<ICustomValidator<RegisterUserRequest>, AddUserRequestValidator>()
                .RegisterWithBase(typeof(IEventHandler<>), typeof(RoleAddedToUserHandler))
                .Register<IServiceInterceptor, RoleValidationInterceptor>()
                .Register<IServiceInterceptor, DataValidationInterceptor>()
                .Register<IHttpClient, CustomHttpClient>();
        }
    }
}
