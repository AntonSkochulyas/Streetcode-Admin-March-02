using FluentValidation;

namespace Streetcode.BLL.MediatR.Users.UsersAddittionalInfoes.Update
{
    public class UpdateUserAdditionalInfoCommandValidator : AbstractValidator<UpdateUserAdditionalInfoCommand>
    {
        private readonly ushort _firstNameMinLength;
        private readonly ushort _firstNameMaxLength;

        private readonly ushort _secondNameMinLength;
        private readonly ushort _secondNameMaxLength;

        private readonly ushort _thirdNameMinLength;
        private readonly ushort _thirdNameMaxLength;

        private readonly ushort _emailMinLength;
        private readonly ushort _emailMaxLength;

        private readonly ushort _minAge;
        private readonly ushort _maxAge;

        public UpdateUserAdditionalInfoCommandValidator()
        {
            _firstNameMinLength = 2;
            _firstNameMaxLength = 32;

            _secondNameMinLength = 5;
            _secondNameMaxLength = 32;

            _thirdNameMinLength = 5;
            _thirdNameMaxLength = 32;

            _emailMinLength = 3;
            _emailMaxLength = 32;

            _minAge = 14;
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
