using System;
using FluentValidation;

namespace Streetcode.BLL.MediatR.Media.Image.Create
{
	public class CreateImageCommandValidator : AbstractValidator<CreateImageCommand>
    {
		private readonly int maxBlobNameLength;
		private readonly int maxMimeTypeLength;
		private readonly int maxTitleLength;
		private readonly int maxAltLength;

		public CreateImageCommandValidator()
		{
			maxBlobNameLength = 100;
			maxMimeTypeLength = 10;

			RuleFor(command => command.Image.MimeType)
                .NotEmpty()
                .WithMessage("Mime Type is required.")
                .MaximumLength(maxMimeTypeLength)
                .WithMessage($"Mime Type length should not be longer than {maxMimeTypeLength} symbols.");

			RuleFor(command => command.Image.Title)
                .MaximumLength(maxMimeTypeLength)
                .WithMessage($"Title length should not be longer than {maxTitleLength} symbols.");

			RuleFor(command => command.Image.Alt)
                .MaximumLength(maxMimeTypeLength)
                .WithMessage($"Alt length should not be longer than {maxAltLength} symbols.");
        }
	}
}