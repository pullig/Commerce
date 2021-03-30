using Commerce.Domain.Entitties;
using System.Collections.Generic;

namespace Commerce.Domain.Interfaces.Services
{
    public interface IUserService
    {
        public IEnumerable<User> GetUsers();
    }
}
