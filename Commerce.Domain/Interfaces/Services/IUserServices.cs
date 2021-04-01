using Commerce.Domain.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Commerce.Domain.Interfaces.Services
{
    public interface IUserServices
    {
        /// <summary>
        /// Get users based on filters passed
        /// </summary>
        /// <param name="dto">
        /// Filters to search for am user, if no filters are passed will return all users
        /// </param>
        /// <returns>List of users</returns>
        public IEnumerable<GetUserResult> GetUsers(GetUsersRequest dto);

        /// <summary>
        /// Add an user to the 
        /// </summary>
        /// <param name="signupDto"></param>
        /// <returns></returns>
        public Task SignUpAsync(SignUpRequest signupDto);
        /// <summary>
        /// Sign the user in the service if they exist on the database
        /// </summary>
        /// <param name="dto">Username and password</param>
        /// <returns>User and token</returns>
        public Task<SignInResult> SignInAsync(SignInRequest dto);
    }
}
