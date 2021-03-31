using AutoMapper;
using Commerce.Domain.DTOs;
using Commerce.Domain.Entities;
using Commerce.Domain.Interfaces.Repositories;
using Commerce.Domain.Interfaces.Services;
using Commerce.Services;
using FluentValidation;
using FluentValidation.Results;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Commerce.Test.Services
{
    public class UserServicesTests
    {
        private readonly IUserServices userServices;

        private readonly Mock<IUserRepository> mockUserRepository = new Mock<IUserRepository>();
        private readonly Mock<IMapper> mockMapper = new Mock<IMapper>();
        private readonly Mock<IValidator<SignUpDto>> mockSignUpValidator = new Mock<IValidator<SignUpDto>>();

        User mockUser;

        public UserServicesTests() 
        {
            mockUser = new User()
            {
                CreationDate = DateTime.Now,
                DisplayName = "displayName",
                EmailAddress = "emailAddress",
                Id = 1,
                Password = "password",
                Username = "username"
            };

            mockUserRepository = new Mock<IUserRepository>();
            mockMapper = new Mock<IMapper>();
            mockSignUpValidator = new Mock<IValidator<SignUpDto>>();

            this.userServices = new UserServices(mockUserRepository.Object,
                                                mockMapper.Object,
                                                mockSignUpValidator.Object);


        }

        [Fact]
        public void GetUsers_ShouldReturnListOfUsers()
        {
            var getUserAsyncResult = new GetUserAsyncResult
            {
                CreationDate = mockUser.CreationDate,
                DisplayName = mockUser.DisplayName,
                EmailAddress = mockUser.EmailAddress,
                Username = mockUser.Username
            };

            var listUser = new List<User>()
            {
                mockUser
            };

            var listResult = new List<GetUserAsyncResult>
            {
                getUserAsyncResult
            };

            mockMapper.Setup(m => m.Map<IEnumerable<GetUserAsyncResult>>(It.IsAny<IEnumerable<User>>()))
                .Returns(listResult);

            mockUserRepository.Setup(m => m.GetUsers(It.IsAny<GetUsersDto>())).
                Returns(listUser);

            var result = userServices.GetUsers(new GetUsersDto());

            Assert.Equal(listResult, result);
        }

        [Fact]
        public async Task SignUpAsync_ShouldReturnSuccess()
        {
            var dto = new SignUpDto
            {
                DisplayName = mockUser.DisplayName,
                EmailAddress = mockUser.EmailAddress,
                Password = mockUser.Password,
                Username = mockUser.Username
            };

            mockSignUpValidator.Setup(v => v.Validate(It.IsAny<SignUpDto>()))
                .Returns(new FluentValidation.Results.ValidationResult());
            mockMapper.Setup(m => m.Map<User>(It.IsAny<SignUpDto>()))
                .Returns(mockUser);

            mockUserRepository.Setup(u => u.AddAsync(It.IsAny<User>()))
                .Callback<User>((user) =>
                {
                    Assert.Equal(dto.DisplayName, user.DisplayName);
                    Assert.Equal(dto.EmailAddress, user.EmailAddress);
                    Assert.Equal(dto.Username, user.Username);
                    Assert.NotEqual(dto.Password, user.Password);
                });

            await userServices.SignUpAsync(dto);
        }

        [Fact]
        public async Task SignUpAsync_ShouldReturnValidationException()
        {
            var dto = new SignUpDto
            {
                DisplayName = mockUser.DisplayName,
                EmailAddress = mockUser.EmailAddress,
                Password = mockUser.Password,
                Username = mockUser.Username
            };

            var validationResult = new ValidationResult(new List<ValidationFailure>()
                {
                    new ValidationFailure("EmailAddress", "Email address already in use")
                });

            mockSignUpValidator.Setup(v => v.Validate(It.IsAny<SignUpDto>()))
                .Returns(validationResult);

            await Assert.ThrowsAsync<ValidationException>(() => userServices.SignUpAsync(dto));
        }
    }
}
