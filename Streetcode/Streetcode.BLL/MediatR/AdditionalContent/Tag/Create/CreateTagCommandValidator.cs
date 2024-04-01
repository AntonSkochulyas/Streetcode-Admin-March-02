using FluentValidation;

namespace Streetcode.BLL.MediatR.AdditionalContent.Tag.Create
{
    internal class CreateTagCommandValidator : AbstractValidator<CreateTagCommand>
    {
        private readonly ushort _maxTitleLength;
        public CreateTagCommandValidator()
        {
            _maxTitleLength = 50;

            // Updated
            RuleFor(command => command.tag.Title)
                .NotEmpty()
                .WithMessage(TagErrors.CreateTagCommandValidatorTitleIsRequiredError)
                .MaximumLength(_maxTitleLength)
                .WithMessage(string.Format(TagErrors.CreateTagCommandValidatorTitleMaxLengthError, _maxTitleLength));
        }
    }
}
