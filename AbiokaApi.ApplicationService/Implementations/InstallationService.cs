using AbiokaApi.ApplicationService.Abstractions;
using AbiokaApi.ApplicationService.Messaging;
using AbiokaApi.Domain;
using AbiokaApi.Domain.Repositories;
using AbiokaApi.Infrastructure.Common.Authentication;
using AbiokaApi.Infrastructure.Common.Domain;
using System;
using System.Collections.Generic;

namespace AbiokaApi.ApplicationService.Implementations
{
    public class InstallationService : IInstallationService
    {
        private readonly IUserSecurityRepository userSecurityRepository;
        private readonly IUserRepository userRepository;
        private readonly IRoleRepository roleRepository;
        private readonly IRepository<Menu> menuRepository;

        public InstallationService(IUserSecurityRepository userSecurityRepository, IUserRepository userRepository, IRoleRepository roleRepository, IRepository<Menu> menuRepository) {
            this.userSecurityRepository = userSecurityRepository;
            this.userRepository = userRepository;
            this.roleRepository = roleRepository;
            this.menuRepository = menuRepository;
        }

        public void CreateApplicationData(CreateApplicationDataRequest createApplicationDataRequest) {
            var adminRole = new Role(Guid.Empty, "Admin");
            roleRepository.Add(adminRole);
            var userRole = new Role(Guid.Empty, "User");
            roleRepository.Add(userRole);

            var userSecurity = new UserSecurity(
                Guid.Empty,
                createApplicationDataRequest.Email,
                AuthProvider.Local,
                Guid.NewGuid().ToString(),
                Guid.NewGuid().ToString(),
                Guid.NewGuid().ToString(),
                string.Empty,
                createApplicationDataRequest.Password,
                "en",
                "admin",
                "admin",
                null,
                Gender.Male,
                false,
                new List<Role> { adminRole }
            );

            userSecurityRepository.Add(userSecurity);

            CreateMenus(adminRole, userRole);
        }

        private void CreateMenus(Role adminRole, Role userRole) {
            var dashboardMenu = new Menu(Guid.Empty, "Dashboard", "/", 5, null, userRole, null);
            menuRepository.Add(dashboardMenu);
            var loginLogsMenu = new Menu(Guid.Empty, "LoginLogs", "loginAttempts", 80, null, userRole, null);
            menuRepository.Add(loginLogsMenu);
            var adminMenu = new Menu(Guid.Empty, "Admin", "/", 10, null, adminRole, null);
            menuRepository.Add(adminMenu);
            var menusMenu = new Menu(Guid.Empty, "Menus", "menus", 10, adminMenu, adminRole, null);
            menuRepository.Add(menusMenu);
            var usersMenu = new Menu(Guid.Empty, "Users", "users", 1, adminMenu, adminRole, null);
            menuRepository.Add(usersMenu);
            var rolesMenu = new Menu(Guid.Empty, "Roles", "roles", 4, adminMenu, adminRole, null);
            menuRepository.Add(rolesMenu);
        }

        public bool IsInstallationRequired() => userRepository.Count() == 0;
    }
}