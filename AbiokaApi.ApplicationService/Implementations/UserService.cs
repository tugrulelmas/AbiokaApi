using AbiokaApi.ApplicationService.Abstractions;
using AbiokaApi.ApplicationService.Messaging;
using AbiokaApi.Domain;
using AbiokaApi.Domain.Repositories;
using AbiokaApi.Infrastructure.Common.Authentication;
using AbiokaApi.Infrastructure.Common.Domain;
using AbiokaApi.Infrastructure.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Net;

namespace AbiokaApi.ApplicationService.Implementations
{
    public class UserService : CrudService<User>, IUserService
    {
        private readonly IUserRepository repository;
        private readonly IUserSecurityRepository userSecurityRepository;
        private readonly IAbiokaToken abiokaToken;

        public UserService(IUserRepository repository, IUserSecurityRepository userSecurityRepository, IAbiokaToken abiokaToken)
            : base(repository) {
            this.repository = repository;
            this.userSecurityRepository = userSecurityRepository;
            this.abiokaToken = abiokaToken;
        }

        public IEnumerable<User> GetAll() => repository.GetAll();

        public string Login(LoginRequest loginRequest) {
            var user = userSecurityRepository.GetByEmail(loginRequest.Email);

            if (user == null) {
                throw new DenialException(HttpStatusCode.NotFound, "kullanıcı bulunamadı");
            }

            var hashedPassword = user.GetHashedPassword(loginRequest.Password);
            if (user.Password != hashedPassword) {
                throw new DenialException("hatalı şifre");
            }

            if (user.IsDeleted) {
                throw new DenialException("Kullanıcı aktif değil");
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

        public override void Add(User entiy) {
            throw new NotSupportedException();
        }

        public User Add(AddUserRequest request) {
            var tmpUser = userSecurityRepository.GetByEmail(request.Email);
            if (tmpUser != null)
                throw new DenialException("UserIsAlreadyRegistered");

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

        public override void Update(User entiy) {
            var dbUser = GetEntity(entiy.Id);
            dbUser.IsAdmin = entiy.IsAdmin;
            repository.Update(dbUser);
        }
    }
}
