using Commerce.Domain.Entitties;
using System.Collections.Generic;

namespace Commerce.Domain.Interfaces.Repositories
{
    public interface IUserRepository
    {
        public IEnumerable<User> GetUsers();
    }
}
