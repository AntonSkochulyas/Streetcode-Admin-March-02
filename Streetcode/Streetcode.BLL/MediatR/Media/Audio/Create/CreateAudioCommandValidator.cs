// Necessary usings.
using FluentValidation;

// Necessary namespaces.
namespace Streetcode.BLL.MediatR.Media.Audio.Create
{
    /// <summary>
    /// Validator, that validates a model inside CreateAudioCommand.
    /// </summary>
    public class CreateAudioCommandValidator : AbstractValidator<CreateAudioCommand>
    {
        // Max title length
		private readonly int maxTitleLength;

        // Max blob name length
		private readonly int maxBlobNameLength;

        // Max mime type length
		private readonly int maxMimeTypeLength;

        // Constructor
		public CreateAudioCommandValidator()
		{
			maxTitleLength = 100;
			maxBlobNameLength = 100;
			maxMimeTypeLength = 10;

			RuleFor(command => command.Audio.Title)
                .NotEmpty()
                .WithMessage(MediaErrors.CreateAudioCommandValidatorTitleIsRequiredError)
                .MaximumLength(maxTitleLength)
                .WithMessage(string.Format(MediaErrors.CreateAudioCommandValidatorTitleMaxLengthError, maxTitleLength));

			RuleFor(command => command.Audio.MimeType)
                .NotEmpty()
                .WithMessage(MediaErrors.CreateAudioCommandValidatorBlobNameIsRequiredError)
                .MaximumLength(maxMimeTypeLength)
                .WithMessage(string.Format(MediaErrors.CreateAudioCommandValidatorBlobNameMaxLengthError, maxBlobNameLength));
        }
	}
}