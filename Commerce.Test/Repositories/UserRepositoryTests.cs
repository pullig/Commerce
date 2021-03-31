using Commerce.Domain.DTOs;
using Commerce.Domain.Entities;
using Commerce.Domain.Enums;
using Commerce.Domain.Interfaces.Repositories;
using Commerce.Infrastructure.Repositories;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Commerce.Test.Repositories
{
    public class UserRepositoryTests
    {
        MockCommerceContext context;

        public UserRepositoryTests() 
        {
            
        }

        [Fact]
        public void GetUsers_ShouldReturnListOfAllUsers()
        {
            var userRepository = InitializeRepository();
            var users = context.CommerceContext().Users;

            var result = userRepository.GetUsers(new GetUsersRequest());

            context.DropCommerceContext();
            Assert.Equal(users.ToList(), result.OrderBy(r => r.Id));
        }

        [Fact]
        public void GetUsers_ShouldReturnListOfUsersFilteredByUserName()
        {
            var dto = new GetUsersRequest
            {
                Username = "username",
                OrderBy = UserOrderBy.UsernameAscending
            };

            var userRepository = InitializeRepository();
            var users = context.CommerceContext().Users
                            .Where(u => u.Username.Contains(dto.Username))
                            .OrderBy(u => u.Username);

            var result = userRepository.GetUsers(dto);

            context.DropCommerceContext();
            Assert.Equal(users, result);
        }

        [Fact]
        public void GetUsers_ShouldReturnListOfUsersFilteredByDisplayname()
        {
            var dto = new GetUsersRequest
            {
                DisplayName = "DisplayName",
                OrderBy = UserOrderBy.UsernameDescending
            };

            var userRepository = InitializeRepository();
            var users = context.CommerceContext().Users
                            .Where(u => u.DisplayName.Contains(dto.DisplayName))
                            .OrderByDescending(u => u.Username);

            var result = userRepository.GetUsers(dto);

            context.DropCommerceContext();
            Assert.Equal(users, result);
        }

        [Fact]
        public void GetUsers_ShouldReturnListOfUsersFilteredByEmailAddress()
        {
            var dto = new GetUsersRequest
            {
                EmailAddress = "memail3@email.com",
                OrderBy = UserOrderBy.DisplayNameAscending
            };

            var userRepository = InitializeRepository();
            var users = context.CommerceContext().Users
                            .Where(u => u.EmailAddress.Contains(dto.EmailAddress))
                            .OrderBy(u => u.DisplayName);

            var result = userRepository.GetUsers(dto);

            context.DropCommerceContext();
            Assert.Equal(users, result);
        }

        [Fact]
        public void GetUsers_ShouldReturnListOfUsersFilteredByStartDate()
        {
            var dto = new GetUsersRequest
            {
                StartDate = DateTime.Now,
                OrderBy = UserOrderBy.DisplayNameDescending
            };

            var userRepository = InitializeRepository();
            var users = context.CommerceContext().Users
                            .Where(u => u.CreationDate >= dto.StartDate)
                            .OrderByDescending(u => u.DisplayName);

            var result = userRepository.GetUsers(dto);

            context.DropCommerceContext();
            Assert.Equal(users, result);
        }

        [Fact]
        public void GetUsers_ShouldReturnListOfUsersFilteredByEndDate()
        {
            var dto = new GetUsersRequest
            {
                EndDate = DateTime.Now.AddDays(-15),
                OrderBy = UserOrderBy.EmailAddressAscending
            };

            var userRepository = InitializeRepository();
            var users = context.CommerceContext().Users
                            .Where(u => u.Username.Contains(dto.Username) &&
                                            u.DisplayName.Contains(dto.DisplayName))
                            .OrderBy(u => u.EmailAddress);

            var result = userRepository.GetUsers(dto);

            context.DropCommerceContext();
            Assert.Equal(users, result);
        }

        [Fact]
        public void GetUsers_ShouldReturnListOfUsersFilteredByAll()
        {
            var dto = new GetUsersRequest
            {
                Username = "ausername",
                DisplayName = "aDisplayName",
                StartDate = DateTime.Now.AddDays(-50),
                EndDate = DateTime.Now.AddDays(1),
                EmailAddress = "aemail@email.com",
                OrderBy = UserOrderBy.EmailAddressDescending
            };

            var userRepository = InitializeRepository();
            var users = context.CommerceContext().Users
                            .Where(u => u.Username.Contains(dto.Username) &&
                                            u.DisplayName.Contains(dto.DisplayName) &&
                                            u.EmailAddress.Contains(dto.EmailAddress) &&
                                            u.CreationDate >= dto.StartDate &&
                                            u.CreationDate <= dto.EndDate)
                            .OrderByDescending(u => u.EmailAddress);

            var result = userRepository.GetUsers(dto);

            context.DropCommerceContext();
            Assert.Equal(users, result);
            
        }

        [Fact]
        public void VerifyIfUsernameExists_IsTrue()
        {
            var userRepository = InitializeRepository();

            var result = userRepository.VerifyIfUsernameExists("ausername");

            context.DropCommerceContext();
            Assert.True(result);
        }

        [Fact]
        public void VerifyIfUsernameExists_IsFalse()
        {
            var userRepository = InitializeRepository();

            var result = userRepository.VerifyIfUsernameExists("notexists");

            context.DropCommerceContext();
            Assert.False(result);
        }

        [Fact]
        public void VerifyIfEmailAddressExists_IsTrue()
        {
            var userRepository = InitializeRepository();

            var result = userRepository.VerifyIfEmailAddressExists("aemail@email.com");

            context.DropCommerceContext();
            Assert.True(result);
        }

        [Fact]
        public void VerifyIfEmailAddressExists_IsFalse()
        {
            var userRepository = InitializeRepository();

            var result = userRepository.VerifyIfEmailAddressExists("notexists");

            context.DropCommerceContext();
            Assert.False(result);
        }

        [Fact]
        public async Task AddAsync_ShouldAddUser()
        {
            var userRepository = InitializeRepository();

            var user = new User
            {
                CreationDate = DateTime.Now,
                DisplayName = "newDisplayName",
                EmailAddress = "newEmailAddress",
                Password = "password",
                Username = "AddAsyncTestUser"
            };

            var result = await userRepository.AddAsync(user);

            var addedUser = context.CommerceContext().Users.FirstOrDefault(u => u.Username.Equals(user.Username));

            context.DropCommerceContext();
            Assert.NotNull(addedUser);
            Assert.NotEqual(0, addedUser.Id);
            Assert.Equal(user.Username, addedUser.Username);
            Assert.Equal(user.DisplayName, addedUser.DisplayName);
            Assert.Equal(user.EmailAddress, addedUser.EmailAddress);
            Assert.Equal(user.CreationDate, addedUser.CreationDate);
            Assert.Equal(user.Password, addedUser.Password);
        }

        private IUserRepository InitializeRepository()
        {
            context = new MockCommerceContext();
            return new UserRepository(context.CommerceContext());
        }
    }
}
