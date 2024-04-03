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
                .WithMessage("Title is required.")
                .MaximumLength(maxTitleLength)
                .WithMessage($"Title length should not be longer than {maxTitleLength} symbols.");

			RuleFor(command => command.Audio.MimeType)
                .NotEmpty()
                .WithMessage("Blob Name is required.")
                .MaximumLength(maxMimeTypeLength)
                .WithMessage($"Blob Name length should not be longer than {maxMimeTypeLength} symbols.");
        }
	}
}