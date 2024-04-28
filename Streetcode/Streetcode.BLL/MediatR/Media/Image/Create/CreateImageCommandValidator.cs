// Necessary usings.
using FluentValidation;

// Necessary namespaces.
namespace Streetcode.BLL.MediatR.Media.Image.Create
{
    /// <summary>
    /// Validator, that validates a model inside CreateImageCommand.
    /// </summary>
    public class CreateImageCommandValidator : AbstractValidator<CreateImageCommand>
    {
        // Constructor
        public CreateImageCommandValidator()
        {
            // Max title length
            int maxTitleLength = 100;

            // Max mime type length
            int maxMimeTypeLength = 10;

            // Max alt length
            int maxAltLength = 50;

            RuleFor(command => command.Image.MimeType)
                .NotEmpty()
                .WithMessage(MediaErrors.CreateImageCommandValidatorMimeTypeIsRequiredError)
                .MaximumLength(maxMimeTypeLength)
                .WithMessage(string.Format(MediaErrors.CreateImageCommandValidatorMimeTypeMaxLengthError, maxMimeTypeLength));

            RuleFor(command => command.Image.Title)
                .NotEmpty()
                .WithMessage(MediaErrors.CreateImageCommandValidatorTitleMaxLengthError)
                .MaximumLength(maxTitleLength)
                .WithMessage(string.Format(MediaErrors.CreateImageCommandValidatorTitleMaxLengthError, maxTitleLength));

            RuleFor(command => command.Image.Alt)
                .MaximumLength(maxAltLength)
                .WithMessage(string.Format(MediaErrors.CreateImageCommandValidatorAltMaxLengthError, maxAltLength));
        }
    }
}