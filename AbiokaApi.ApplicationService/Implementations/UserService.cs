using AbiokaApi.ApplicationService.Abstractions;
using AbiokaApi.Domain;
using AbiokaApi.Domain.Repositories;
using System.Collections.Generic;

namespace AbiokaApi.ApplicationService.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserRepository repository;

        public UserService(IUserRepository repository) {
            this.repository = repository;
        }

        public IEnumerable<User> GetAll() => repository.GetAll();
    }
}
