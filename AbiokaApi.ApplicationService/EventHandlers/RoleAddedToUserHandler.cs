using AbiokaApi.Domain.Events;
using AbiokaApi.Domain.Repositories;
using AbiokaApi.Infrastructure.Common.Domain;

namespace AbiokaApi.ApplicationService.EventHandlers
{
    public class RoleAddedToUserHandler : IEventHandler<RoleAddedToUser>
    {
        private readonly IRoleRepository roleRepository;

        public RoleAddedToUserHandler(IRoleRepository roleRepository) {
            this.roleRepository = roleRepository;
        }

        public int Order => 5;

        public void Handle(RoleAddedToUser eventInstance) {
            roleRepository.AddToUser(eventInstance.RoleId, eventInstance.User.Id);
        }
    }
}
