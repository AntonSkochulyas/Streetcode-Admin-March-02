using FluentValidation;

namespace Streetcode.BLL.MediatR.Users.Authenticate.Register.RegisterAdmin
{
    public class RegisterAdminCommandValidator : AbstractValidator<RegisterAdminCommand>
    {
        private readonly ushort _loginMinLength;
        private readonly ushort _loginMaxLength;
        public RegisterAdminCommandValidator()
        {
            _loginMinLength = 6;
            _loginMaxLength = 32;

            RuleFor(x => x.RegisterModelDto.Username)
                   .NotEmpty().WithMessage("Login is required.")
                   .Length(_loginMinLength, _loginMaxLength).WithMessage($"Login must be between {_loginMinLength} and {_loginMaxLength} characters.");

            RuleFor(x => x.RegisterModelDto.Password)
           .NotEmpty().WithMessage("Password is required.")
           .Matches(@"^(?=.*[A-Z])(?=.*[a-zA-Z0-9_])[a-zA-Z0-9_]+$")
           .WithMessage("Password must contain at least one uppercase letter and consist only of English letters, digits, and underscores.");
        }
    }
}
