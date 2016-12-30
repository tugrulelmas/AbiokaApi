using AbiokaApi.ApplicationService.Abstractions;
using AbiokaApi.ApplicationService.Messaging;
using AbiokaApi.Domain;
using System.Collections.Generic;

namespace AbiokaApi.ApplicationService.Implementations
{
    public class InstallationService : IInstallationService
    {
        private readonly IUserService userService;
        private readonly ICrudService<Role> roleService;

        public InstallationService(IUserService userService, ICrudService<Role> roleService) {
            this.userService = userService;
            this.roleService = roleService;
        }

        public void CreateApplicationData(CreateApplicationDataRequest createApplicationDataRequest) {
            var role = new Role {
                Name = "Admin"
            };
            roleService.Add(role);

            var adduserRequest = new AddUserRequest {
                Email = createApplicationDataRequest.Email,
                Password = createApplicationDataRequest.Password,
                Roles = new List<Role> { role }
            };

            userService.Add(adduserRequest);
        }

        public bool IsInstallationRequired() => userService.Count() == 0;
    }
}
