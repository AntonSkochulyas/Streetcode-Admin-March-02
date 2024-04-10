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
        // Max mime type length
		private readonly ushort maxMimeTypeLength;

        // Max title length
		private readonly ushort maxTitleLength;

        // Max alt length
		private readonly ushort maxAltLength;

        // Constructor
		public CreateImageCommandValidator()
		{
            maxTitleLength = 100;
            maxMimeTypeLength = 10;

            RuleFor(command => command.Image.MimeType)
                .NotEmpty()
                .WithMessage(MediaErrors.CreateImageCommandValidatorMimeTypeIsRequiredError)
                .MaximumLength(maxMimeTypeLength)
                .WithMessage(string.Format(MediaErrors.CreateImageCommandValidatorMimeTypeMaxLengthError, maxMimeTypeLength));

            RuleFor(command => command.Image.Title)
                .MaximumLength(maxTitleLength)
                .WithMessage(string.Format(MediaErrors.CreateImageCommandValidatorTitleMaxLengthError, maxTitleLength));

            RuleFor(command => command.Image.Alt)
                .MaximumLength(maxMimeTypeLength)
                .WithMessage(string.Format(MediaErrors.CreateImageCommandValidatorAltMaxLengthError, maxAltLength));
        }
	}
}