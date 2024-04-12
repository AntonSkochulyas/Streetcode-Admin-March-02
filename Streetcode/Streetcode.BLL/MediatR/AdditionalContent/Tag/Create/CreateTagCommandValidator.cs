using FluentValidation;

namespace Streetcode.BLL.MediatR.AdditionalContent.Tag.Create
{
    internal class CreateTagCommandValidator : AbstractValidator<CreateTagCommand>
    {
        public CreateTagCommandValidator()
        {
            ushort maxTitleLength = 50;

            // Updated
            RuleFor(command => command.Tag.Title)
                .NotEmpty()
                .WithMessage(TagErrors.CreateTagCommandValidatorTitleIsRequiredError)
                .MaximumLength(maxTitleLength)
                .WithMessage(string.Format(TagErrors.CreateTagCommandValidatorTitleMaxLengthError, maxTitleLength));
        }
    }
}
