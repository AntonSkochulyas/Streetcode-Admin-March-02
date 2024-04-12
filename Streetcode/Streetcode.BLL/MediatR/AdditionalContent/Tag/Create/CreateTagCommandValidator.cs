// Necessary usings
using FluentValidation;

// Necessary namespaces
namespace Streetcode.BLL.MediatR.AdditionalContent.Tag.Create
{
    /// <summary>
    /// Validator, that validates a CreateTagCommand.
    /// </summary>
    internal class CreateTagCommandValidator : AbstractValidator<CreateTagCommand>
    {
        // Max title length
        private readonly ushort _maxTitleLength;

        // Constructor 
        public CreateTagCommandValidator()
        {
            _maxTitleLength = 50;

            RuleFor(command => command.tag.Title)
                .NotEmpty()
                .WithMessage(TagErrors.CreateTagCommandValidatorTitleIsRequiredError)
                .MaximumLength(_maxTitleLength)
                .WithMessage(string.Format(TagErrors.CreateTagCommandValidatorTitleMaxLengthError, _maxTitleLength));
        }
    }
}
