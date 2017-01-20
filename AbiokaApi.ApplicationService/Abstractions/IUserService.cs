using AbiokaApi.ApplicationService.DTOs;
using AbiokaApi.ApplicationService.Messaging;
using AbiokaApi.Infrastructure.Common.Authentication;
using System;

namespace AbiokaApi.ApplicationService.Abstractions
{
    public interface IUserService : IReadService<UserDTO>
    {
        /// <summary>
        /// Adds the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        AddUserResponse Add(AddUserRequest request);

        /// <summary>
        /// Registers the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        AddUserResponse Register(RegisterUserRequest request);

        /// <summary>
        /// Updates the specified entiy.
        /// </summary>
        /// <param name="entiy">The entiy.</param>
        [AllowedRole("Admin")]
        void Update(UserDTO entiy);

        /// <summary>
        /// Deletes the specified user.
        /// </summary>
        /// <param name="id">The identifier.</param>
        void Delete(Guid id);

        /// <summary>
        /// Count of users.
        /// </summary>
        /// <returns></returns>
        int Count();

        /// <summary>
        /// Changes the password.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>New token</returns>
        string ChangePassword(ChangePasswordRequest request);

        /// <summary>
        /// Changes the language.
        /// </summary>
        /// <param name="language">The language.</param>
        void ChangeLanguage(string language);
    }
}
