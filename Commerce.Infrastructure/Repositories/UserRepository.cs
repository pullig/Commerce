using Commerce.Domain.Entitties;
using Commerce.Domain.Interfaces.Repositories;
using Commerce.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commerce.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly CommerceContext context;

        public UserRepository(CommerceContext context)
        {
            this.context = context;
        }

        public IEnumerable<User> GetUsers()
        {
            return context.Users;
        }
    }
}
