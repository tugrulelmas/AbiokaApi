using AbiokaApi.ApplicationService.Messaging;
using AbiokaApi.Domain;
using System;
using System.Collections.Generic;

namespace AbiokaApi.ApplicationService.Abstractions
{
    public interface IUserService : IReadService<User>
    {
        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns></returns>
        IEnumerable<User> GetAll();

        /// <summary>
        /// Logins the specified login request.
        /// </summary>
        /// <param name="loginRequest">The login request.</param>
        /// <returns></returns>
        string Login(LoginRequest loginRequest);

        /// <summary>
        /// Adds the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        User Add(AddUserRequest request);

        /// <summary>
        /// Updates the specified entiy.
        /// </summary>
        /// <param name="entiy">The entiy.</param>
        void Update(User entiy);

        /// <summary>
        /// Deletes the specified user.
        /// </summary>
        /// <param name="id">The identifier.</param>
        void Delete(Guid id);
    }
}
