using AbiokaApi.ApplicationService.Abstractions;
using AbiokaApi.ApplicationService.DTOs;
using AbiokaApi.ApplicationService.Messaging;
using AbiokaApi.Domain;
using AbiokaApi.Domain.Repositories;
using AbiokaApi.Infrastructure.Common.Authentication;
using AbiokaApi.Infrastructure.Common.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AbiokaApi.ApplicationService.Implementations
{
    public class UserService : ReadService<User, UserDTO>, IUserService
    {
        private readonly IUserSecurityRepository userSecurityRepository;
        private readonly IRoleRepository roleRepository;
        private readonly IAbiokaToken abiokaToken;
        private readonly IEventDispatcher eventDispatcher;

        public UserService(IUserRepository repository, IUserSecurityRepository userSecurityRepository, IRoleRepository roleRepository, IAbiokaToken abiokaToken, IEventDispatcher eventDispatcher)
            : base(repository) {
            this.userSecurityRepository = userSecurityRepository;
            this.roleRepository = roleRepository;
            this.abiokaToken = abiokaToken;
            this.eventDispatcher = eventDispatcher;
        }

        public string Login(LoginRequest loginRequest) {
            var user = userSecurityRepository.GetByEmail(loginRequest.Email);
            user.CreateToken(abiokaToken);
            userSecurityRepository.Update(user);
            return user.Token;
        }

        public string RefreshToken(string refreshToken) {
            var user = userSecurityRepository.GetByRefreshToken(refreshToken);
            if (user == null)
                throw AuthenticationException.InvalidCredential;

            user.CreateToken(abiokaToken);
            userSecurityRepository.Update(user);
            return user.Token;
        }

        public AddUserResponse Add(AddUserRequest request) {
            var roles = DTOMapper.ToDomainObjects<Role>(request.Roles);
            var userSecurity = new UserSecurity (
                Guid.Empty,
                request.Email,
                AuthProvider.Local,
                Guid.NewGuid().ToString(),
                Guid.NewGuid().ToString(),
                string.Empty,
                request.Password,
                false,
                roles
            );

            userSecurityRepository.Add(userSecurity);

            return new AddUserResponse {
                Email = userSecurity.Email,
                Id = userSecurity.Id,
                Roles = request.Roles
            };
        }

        public AddUserResponse Register(AddUserRequest request) {
            var userRole = roleRepository.GetByName("User");
            request.Roles = new List<RoleDTO> { new RoleDTO { Id = userRole.Id, Name = userRole.Name } };

            return Add(request);
        }

        public void Update(UserDTO entity) {
            var dbUser = GetEntity(entity.Id);
            var roles = DTOMapper.ToDomainObjects<Role>(entity.Roles);
            dbUser.SetRoles(roles);

            eventDispatcher.Dispatch(dbUser.Events.ToArray());
        }

        public void Delete(Guid id) {
            var entity = GetEntity(id);
            repository.Delete(entity);
        }

        public int Count() => ((IUserRepository)repository).Count();

        public string ChangePassword(ChangePasswordRequest request) {
            var user = userSecurityRepository.FindById(request.UserId);
            user.ChangePassword(request.OldPassword, request.NewPassword);
            user.CreateToken(abiokaToken);
            userSecurityRepository.Update(user);

            return user.Token;
        }
    }
}
