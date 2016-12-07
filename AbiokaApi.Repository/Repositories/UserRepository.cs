using AbiokaApi.Domain;
using AbiokaApi.Domain.Repositories;
using AbiokaApi.Repository.DatabaseObjects;

namespace AbiokaApi.Repository.Repositories
{
    public class UserRepository : Repository<User, UserDB>, IUserRepository
    {
    }
}
