// Necessary usings.
using FluentValidation;

// Necessary namespaces.
namespace Streetcode.BLL.MediatR.Users.UsersAddittionalInfoes.Create
{
    /// <summary>
    /// Validator, that validates a model inside CreateUserAdditionalInfoCommand.
    /// </summary>
    public class CreateUserAdditionalInfoCommandValidator : AbstractValidator<CreateUserAdditionalInfoCommand>
    {
        // Minimum length allowed for the first name.
        private readonly ushort _firstNameMinLength;

        // Maximum length allowed for the first name.
        private readonly ushort _firstNameMaxLength;

        // Minimum length allowed for the second name.
        private readonly ushort _secondNameMinLength;

        // Maximum length allowed for the second name.
        private readonly ushort _secondNameMaxLength;

        // Minimum length allowed for the third name.
        private readonly ushort _thirdNameMinLength;

        // Maximum length allowed for the third name.
        private readonly ushort _thirdNameMaxLength;

        // Minimum length allowed for the email address.
        private readonly ushort _emailMinLength;

        // Maximum length allowed for the email address.
        private readonly ushort _emailMaxLength;

        // Minimum age allowed for the user.
        private readonly ushort _minAge;

        // Maximum age allowed for the user.
        private readonly ushort _maxAge;

        // Constructor
        public CreateUserAdditionalInfoCommandValidator()
        {
            // First Name min length
            _firstNameMinLength = 2;

            // First Name max length
            _firstNameMaxLength = 32;

            // Second Name min length
            _secondNameMinLength = 5;

            // Second Name max length
            _secondNameMaxLength = 32;

            // Third Name min length
            _thirdNameMinLength = 5;

            // Third Name max length
            _thirdNameMaxLength = 32;

            // Email min length
            _emailMinLength = 3;

            // Email max length
            _emailMaxLength = 32;

            // Age min
            _minAge = 14;

            // Age max
            _maxAge = 99;

            RuleFor(command => command.UserAdditionalInfoDto.Phone)
                .NotEmpty().WithMessage("Phone number is required.")
                .Matches(@"^\+?(\d{10,13})$").WithMessage("Invalid phone number format.");

            RuleFor(command => command.UserAdditionalInfoDto.FirstName)
               .NotEmpty().WithMessage("First name is required.")
               .MinimumLength(_firstNameMinLength).WithMessage($"First name should be at least {_firstNameMinLength} characters long.")
               .MaximumLength(_firstNameMaxLength).WithMessage($"First name must be a maximum of {_firstNameMaxLength} characters in length.");

            RuleFor(command => command.UserAdditionalInfoDto.FirstName)
             .NotEmpty().WithMessage("Second name is required.")
             .MinimumLength(_secondNameMinLength).WithMessage($"Second name should be at least {_secondNameMinLength} characters long.")
             .MaximumLength(_secondNameMaxLength).WithMessage($"Second name must be a maximum of {_secondNameMaxLength} characters in length.");

            RuleFor(command => command.UserAdditionalInfoDto.FirstName)
             .NotEmpty().WithMessage("Third name is required.")
             .MinimumLength(_secondNameMinLength).WithMessage($"Third name should be at least {_thirdNameMinLength} characters long.")
             .MaximumLength(_secondNameMaxLength).WithMessage($"Third name must be a maximum of {_thirdNameMaxLength} characters in length.");

            RuleFor(command => command.UserAdditionalInfoDto.Email)
                .NotEmpty().WithMessage("Email is required.")
                .MinimumLength(_emailMinLength).WithMessage($"Email should be at least {_emailMinLength} characters long.")
                .MaximumLength(_emailMaxLength).WithMessage($"Email must be a maximum of {_emailMaxLength} characters in length.")
                .EmailAddress().WithMessage("Invalid email format.");

            RuleFor(command => command.UserAdditionalInfoDto.Age)
                .NotEmpty().WithMessage("Age is required.")
                .InclusiveBetween(_minAge, _maxAge).WithMessage($"Age must be between {_minAge} and {_maxAge}.");
        }
    }
}
