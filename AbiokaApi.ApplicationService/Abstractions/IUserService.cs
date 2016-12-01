using AbiokaApi.Domain;
using System.Collections.Generic;

namespace AbiokaApi.ApplicationService.Abstractions
{
    public interface IUserService : IService
    {
        IEnumerable<User> GetAll();
    }
}
