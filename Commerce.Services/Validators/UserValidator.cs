using Commerce.Domain.DTOs;
using Commerce.Domain.Interfaces.Repositories;
using FluentValidation;

namespace Commerce.Services.Validators
{
    public class SignUpValidator : AbstractValidator<SignUpRequest>
    {
        private readonly IUserRepository userRepository;
        public SignUpValidator(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
            RuleFor(u => u.Username).Must(username =>
            {
                return VerifyUsername(username);
            }).
            WithMessage(ErrorMessage.UsernameExists);

            RuleFor(u => u.EmailAddress).Must(emailAddress =>
            {
                return VerifyEmailAddress(emailAddress);
            }).
            WithMessage(ErrorMessage.EmailAddressExists);
        }

        private bool VerifyUsername(string username)
        {
            return !userRepository.VerifyIfUsernameExists(username);
        }

        private bool VerifyEmailAddress(string emailAddress)
        {
            return !userRepository.VerifyIfEmailAddressExists(emailAddress);
        }
    }
}
