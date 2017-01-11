using AbiokaApi.Domain.Events;
using AbiokaApi.Domain.Repositories;
using AbiokaApi.Infrastructure.Common.Domain;

namespace AbiokaApi.Repository.EventHandlers
{
    public class RoleAddedToUserHandler : IEventHandler<RoleAddedToUser>
    {
        private readonly IRoleRepository roleRepository;

        public RoleAddedToUserHandler(IRoleRepository roleRepository) {
            this.roleRepository = roleRepository;
        }

        public void Handle(RoleAddedToUser eventInstance) {
            roleRepository.AddToUser(eventInstance.RoleId, eventInstance.UserId);
        }
    }
}
