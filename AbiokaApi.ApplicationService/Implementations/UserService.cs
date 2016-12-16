using AbiokaApi.ApplicationService.Abstractions;
using AbiokaApi.ApplicationService.Messaging;
using AbiokaApi.Domain;
using AbiokaApi.Domain.Repositories;
using AbiokaApi.Infrastructure.Common.Authentication;
using AbiokaApi.Infrastructure.Common.Exceptions;
using System;
using System.Linq;
using System.Net;

namespace AbiokaApi.ApplicationService.Implementations
{
    public class UserService : ReadService<User>, IUserService
    {
        private readonly IUserSecurityRepository userSecurityRepository;
        private readonly IAbiokaToken abiokaToken;

        public UserService(IUserRepository repository, IUserSecurityRepository userSecurityRepository, IAbiokaToken abiokaToken)
            : base(repository) {
            this.userSecurityRepository = userSecurityRepository;
            this.abiokaToken = abiokaToken;
        }

        public string Login(LoginRequest loginRequest) {
            var user = userSecurityRepository.GetByEmail(loginRequest.Email);

            var localToken = Guid.NewGuid().ToString();
            var userInfo = new UserClaim {
                Email = loginRequest.Email,
                Id = user.Id,
                Provider = AuthProvider.Local,
                ProviderToken = localToken,
                Roles = user.Roles?.Select(r => r.Name).ToArray()
            };
            user.ProviderToken = localToken;

            var token = abiokaToken.Encode(userInfo);
            user.Token = token;

            userSecurityRepository.Update(user);

            return token;
        }

        public User Add(AddUserRequest request) {
            var userSecurity = new UserSecurity {
                Email = request.Email,
                Roles = request.Roles,
                AuthProvider = AuthProvider.Local,
                ProviderToken = Guid.NewGuid().ToString()
            };
            userSecurity.Password = userSecurity.GetHashedPassword(request.Password);

            userSecurityRepository.Add(userSecurity);

            return userSecurity;
        }

        public void Update(User entiy) {
            var dbUser = GetEntity(entiy.Id);
            dbUser.Roles = entiy.Roles;
            repository.Update(entiy);
        }

        public void Delete(Guid id) {
            var entity = GetEntity(id);
            repository.Delete(entity);
        }
    }
}
