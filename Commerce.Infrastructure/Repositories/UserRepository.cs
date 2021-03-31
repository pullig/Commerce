using Commerce.Domain.DTOs;
using Commerce.Domain.Entities;
using Commerce.Domain.Enums;
using Commerce.Domain.Interfaces.Repositories;
using Commerce.Infrastructure.Context;
using System.Collections.Generic;
using System.Linq;

namespace Commerce.Infrastructure.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {

        public UserRepository(CommerceContext context) : base(context)
        {

        }

        public IEnumerable<User> GetUsers(GetUsersDto dto)
        {
            IEnumerable<User> result = context.Users.AsEnumerable();

            if (!string.IsNullOrWhiteSpace(dto.Username))
            {
                result = result.Where(u => u.Username.Contains(dto.Username));
            }

            if (!string.IsNullOrWhiteSpace(dto.DisplayName))
            {
                result = result.Where(u => u.DisplayName.Contains(dto.DisplayName));
            }

            if (!string.IsNullOrWhiteSpace(dto.EmailAddress))
            {
                result = result.Where(u => u.EmailAddress.Contains(dto.EmailAddress));
            }

            if (dto.StartDate.HasValue)
            {
                result = result.Where(u => u.CreationDate.Date >= dto.StartDate);
            }

            if (dto.EndDate.HasValue)
            {
                result = result.Where(u => u.CreationDate.Date <= dto.EndDate);
            }

            switch (dto.OrderBy)
            {
                default:
                case UserOrderBy.UsernameAscending:
                    return result.OrderBy(o => o.Username);
                case UserOrderBy.DisplayNameAscending:
                    return result.OrderBy(o => o.DisplayName);
                case UserOrderBy.EmailAddressAscending:
                    return result.OrderBy(o => o.EmailAddress);
                case UserOrderBy.UsernameDescending:
                    return result.OrderByDescending(o => o.Username);
                case UserOrderBy.DisplayNameDescending:
                    return result.OrderByDescending(o => o.DisplayName);
                case UserOrderBy.EmailAddressDescending:
                    return result.OrderByDescending(o => o.EmailAddress);
            }
        }

        public bool VerifyIfUsernameExists(string username)
        {
            return context.Users.Any(u => u.Username.ToLower().Equals(username.ToLower()));
        }

        public bool VerifyIfEmailAddressExists(string emailAddress)
        {
            return context.Users.Any(u => u.EmailAddress.ToLower().Equals(emailAddress.ToLower()));
        }
    }
}
