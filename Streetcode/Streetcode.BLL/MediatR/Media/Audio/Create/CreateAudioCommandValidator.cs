using FluentValidation;

namespace Streetcode.BLL.MediatR.Media.Audio.Create
{
	public class CreateAudioCommandValidator : AbstractValidator<CreateAudioCommand>
    {
		public CreateAudioCommandValidator()
		{
            int maxTitleLength = 100;
            int maxBlobNameLength = 100;
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