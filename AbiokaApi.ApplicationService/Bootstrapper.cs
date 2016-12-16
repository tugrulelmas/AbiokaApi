﻿using AbiokaApi.ApplicationService.Abstractions;
using AbiokaApi.ApplicationService.Interceptors;
using AbiokaApi.Infrastructure.Common.IoC;

namespace AbiokaApi.ApplicationService
{
    public class Bootstrapper
    {
        public static void Initialise() {
            Repository.Bootstrapper.Initialise();
            DependencyContainer.Container
                .RegisterServices<IService>()
                .Register<IServiceInterceptor, RoleValidationInterceptor>(LifeStyle.PerWebRequest);
        }
    }
}
