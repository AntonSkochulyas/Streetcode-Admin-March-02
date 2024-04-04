using System;
using FluentValidation;

namespace Streetcode.BLL.MediatR.Media.Audio.Create
{
	public class CreateAudioCommandValidator : AbstractValidator<CreateAudioCommand>
    {
		private readonly int maxTitleLength;
		private readonly int maxBlobNameLength;
		private readonly int maxMimeTypeLength;

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