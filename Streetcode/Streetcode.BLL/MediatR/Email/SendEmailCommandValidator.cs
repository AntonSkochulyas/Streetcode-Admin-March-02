using FluentValidation;

namespace Streetcode.BLL.MediatR.Email
{
    public class SendEmailCommandValidator : AbstractValidator<SendEmailCommand>
    {
        private readonly ushort _fromMaxLength;
        private readonly ushort _contentMinLength;
        private readonly ushort _contentMaxLength;

        public SendEmailCommandValidator()
        {
            _fromMaxLength = 80;
            _contentMinLength = 1;
            _contentMaxLength = 500;

            RuleFor(command => command.Email.From)
                .NotEmpty().WithMessage("From is required.")
                .MaximumLength(_fromMaxLength).WithMessage($"From should not be longer than {_fromMaxLength}.");

            RuleFor(command => command.Email.Content)
                .NotEmpty().WithMessage("Content is required.")
                .Length(_contentMinLength, _contentMaxLength).WithMessage($"Content length must be between {_contentMinLength} and {_contentMaxLength} characters.");
        }
    }
}
