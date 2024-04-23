// Necessary usings
using FluentValidation;

// Necessary namespaces
namespace Streetcode.BLL.MediatR.AdditionalContent.Tag.Create
{
    /// <summary>
    /// Validator, that validates a CreateTagCommand.
    /// </summary>
    public class CreateTagCommandValidator : AbstractValidator<CreateTagCommand>
    {
        // Constructor.
        public CreateTagCommandValidator()
        {
            // Max title length
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
