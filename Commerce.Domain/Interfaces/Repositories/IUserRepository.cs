using Commerce.Domain.DTOs;
using Commerce.Domain.Entities;
using System.Collections.Generic;

namespace Commerce.Domain.Interfaces.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        /// <summary>
        /// Get users based on filters passed
        /// </summary>
        /// <param name="dto">
        /// Filters to search for am user, if no filters are passed will return all users
        /// </param>
        /// <returns>List of users</returns>
        public IEnumerable<User> GetUsers(GetUsersRequest dto);
        /// <summary>
        /// Verify if the username is already in use
        /// </summary>
        /// <param name="username">Username</param>
        /// <returns>True if exists an user with the username</returns>
        public bool VerifyIfUsernameExists(string username);
        /// <summary>
        /// Verify if the email address is already in use
        /// </summary>
        /// <param name="emailAddress">Email address</param>
        /// <returns>True if exists an user with the email address</returns>
        public bool VerifyIfEmailAddressExists(string emailAddress);
        /// <summary>
        /// Search for an user with the username and password specified
        /// </summary>
        /// <param name="dto">Username and password</param>
        /// <returns>User</returns>
        public User GetUserByUsernameAndPassword(SignInRequest dto);
    }
}
