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
		public CreateAudioCommandValidator()
		{
            // Max title length
            int maxTitleLength = 100;

            // Max blob name length
            int maxBlobNameLength = 100;

            // Max mime type length
            int maxMimeTypeLength = 10;

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