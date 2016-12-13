using AbiokaApi.ApplicationService.Abstractions;
using AbiokaApi.ApplicationService.Messaging;
using AbiokaApi.Domain;
using AbiokaApi.Domain.Repositories;
using AbiokaApi.Infrastructure.Common.Authentication;
using AbiokaApi.Infrastructure.Common.Exceptions;
using System;
using System.Collections.Generic;
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

        public IEnumerable<User> GetAll() => repository.GetAll();

        public string Login(LoginRequest loginRequest) {
            var user = userSecurityRepository.GetByEmail(loginRequest.Email);

            if (user == null) {
                throw new DenialException(HttpStatusCode.NotFound, "UserNotFound");
            }

            var hashedPassword = user.GetHashedPassword(loginRequest.Password);
            if (user.Password != hashedPassword) {
                throw new DenialException("WrongPassword");
            }

            if (user.IsDeleted) {
                throw new DenialException("UserIsNotActive");
            }

            var localToken = Guid.NewGuid().ToString();
            var userInfo = new UserClaim {
                Email = loginRequest.Email,
                Id = user.Id,
                Provider = AuthProvider.Local,
                ProviderToken = localToken
            };
            user.ProviderToken = localToken;

            var token = abiokaToken.Encode(userInfo);
            user.Token = token;

            userSecurityRepository.Update(user);

            return token;
        }

        public User Add(AddUserRequest request) {
            var tmpUser = userSecurityRepository.GetByEmail(request.Email);
            if (tmpUser != null)
                throw new DenialException("UserIsAlreadyRegistered", request.Email);

            var userSecurity = new UserSecurity {
                Email = request.Email,
                IsAdmin = request.IsAdmin,
                AuthProvider = AuthProvider.Local,
                ProviderToken = Guid.NewGuid().ToString()
            };
            userSecurity.Password = userSecurity.GetHashedPassword(request.Password);

            userSecurityRepository.Add(userSecurity);

            return userSecurity;
        }

        public void Update(User entiy) {
            var dbUser = GetEntity(entiy.Id);
            dbUser.IsAdmin = entiy.IsAdmin;
            repository.Update(dbUser);
        }

        public void Delete(Guid id) {
            var entity = GetEntity(id);
            repository.Delete(entity);
        }
    }
}
