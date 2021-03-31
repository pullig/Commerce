using Commerce.Domain.DTOs;
using Commerce.Domain.Entities;
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
        public IEnumerable<GetUserAsyncResult> GetUsers(GetUsersDto dto);

        /// <summary>
        /// Add an user to the 
        /// </summary>
        /// <param name="signupDto"></param>
        /// <returns></returns>
        public Task SignUpAsync(SignUpDto signupDto);
    }
}
