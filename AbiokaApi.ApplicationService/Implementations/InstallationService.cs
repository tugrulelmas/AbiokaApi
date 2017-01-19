using AbiokaApi.ApplicationService.Abstractions;
using AbiokaApi.ApplicationService.DTOs;
using AbiokaApi.ApplicationService.Messaging;
using System;
using System.Collections.Generic;

namespace AbiokaApi.ApplicationService.Implementations
{
    public class InstallationService : IInstallationService
    {
        private readonly IUserService userService;
        private readonly ICrudService<RoleDTO> roleService;

        public InstallationService(IUserService userService, ICrudService<RoleDTO> roleService) {
            this.userService = userService;
            this.roleService = roleService;
        }

        public void CreateApplicationData(CreateApplicationDataRequest createApplicationDataRequest) {
            var adminRole = new RoleDTO { Id = Guid.Empty, Name = "Admin" };
            roleService.Add(adminRole);
            var userRole = new RoleDTO { Id = Guid.Empty, Name = "User" };
            roleService.Add(userRole);

            var registerUserRequest = new RegisterUserRequest {
                Email = createApplicationDataRequest.Email,
                Password = createApplicationDataRequest.Password,
                Roles = new List<RoleDTO> { adminRole }
            };

            userService.Register(registerUserRequest);
        }

        public bool IsInstallationRequired() => userService.Count() == 0;
    }
}
