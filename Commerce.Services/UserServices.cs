using AutoMapper;
using Commerce.Domain.DTOs;
using Commerce.Domain.Entities;
using Commerce.Domain.Interfaces.Clients;
using Commerce.Domain.Interfaces.Repositories;
using Commerce.Domain.Interfaces.Services;
using Commerce.Services.Extensions;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Commerce.Services
{
    public class UserServices : IUserServices
    {
        private readonly IUserRepository userRepository;
        private readonly IMapper mapper;
        private readonly IValidator<SignUpRequest> signUpValidator;
        private readonly IAuth0Client auth0Client;

        public UserServices(IUserRepository userRepository, IMapper mapper,
            IValidator<SignUpRequest> signUpValidator,
            IAuth0Client auth0Client)
        {
            this.userRepository = userRepository;
            this.mapper = mapper;
            this.signUpValidator = signUpValidator;
            this.auth0Client = auth0Client;
        }

        public IEnumerable<GetUserResult> GetUsers(GetUsersRequest dto)
        {
            var result = userRepository.GetUsers(dto);
            return mapper.Map<IEnumerable<GetUserResult>>(result);
        }

        public async Task SignUpAsync(SignUpRequest signupDto)
        {
            var validationResult = signUpValidator.Validate(signupDto);

            if(!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var encodedPassword = Base64.Encode(signupDto.Password);

            var user = mapper.Map<User>(signupDto);

            user.Password = encodedPassword;
            user.CreationDate = DateTime.Now;

            await userRepository.AddAsync(user);
        }

        public async Task<SignInResult> SignInAsync(SignInRequest dto)
        {
            dto.Password = Base64.Encode(dto.Password);

            var user = userRepository.GetUserByUsernameAndPassword(dto);

            if (user == null)
            {
                throw new Exception(ErrorMessage.LoginNotFound);
            }

            var result = await auth0Client.GetTokenAsync();

            var userSignedIn = mapper.Map<SignInResult>(user);
            userSignedIn.Token = result.Token;

            return userSignedIn;
        }
    }
}
