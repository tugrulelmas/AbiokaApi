﻿using AbiokaApi.ApplicationService.Abstractions;
using AbiokaApi.ApplicationService.Implementations;
using AbiokaApi.Infrastructure.Common.IoC;

namespace AbiokaApi.ApplicationService
{
    public class Bootstrapper
    {
        public static void Initialise() {
            Repository.Bootstrapper.Initialise();
            DependencyContainer.Container.Register<IInvitationService, InvitationService>();
        }
    }
}
