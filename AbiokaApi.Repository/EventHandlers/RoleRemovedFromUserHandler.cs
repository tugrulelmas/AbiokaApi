using AbiokaApi.Domain.Events;
using AbiokaApi.Domain.Repositories;
using AbiokaApi.Infrastructure.Common.Domain;

namespace AbiokaApi.Repository.EventHandlers
{
    public class RoleRemovedFromUserHandler : IEventHandler<RoleRemovedFromUser>
    {
        private readonly IRoleRepository roleRepository;

        public RoleRemovedFromUserHandler(IRoleRepository roleRepository) {
            this.roleRepository = roleRepository;
        }

        public void Handle(RoleRemovedFromUser eventInstance) {
            roleRepository.RemoveFromUser(eventInstance.RoleId, eventInstance.UserId);
        }
    }
}
