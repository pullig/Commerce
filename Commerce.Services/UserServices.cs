using AutoMapper;
using Commerce.Domain.DTOs;
using Commerce.Domain.Entities;
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
        private readonly IValidator<SignUpDto> signUpValidator;

        public UserServices(IUserRepository userRepository, IMapper mapper,
            IValidator<SignUpDto> signUpValidator)
        {
            this.userRepository = userRepository;
            this.mapper = mapper;
            this.signUpValidator = signUpValidator;
        }

        public IEnumerable<GetUserAsyncResult> GetUsers(GetUsersDto dto)
        {
            var result = userRepository.GetUsers(dto);
            return mapper.Map<IEnumerable<GetUserAsyncResult>>(result);
        }

        public async Task SignUpAsync(SignUpDto signupDto)
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
    }
}
