using System;
using FluentValidation;
using Streetcode.BLL.MediatR.Media.Audio.Create;

namespace Streetcode.BLL.MediatR.Media.Image.Create
{
	public class CreateImageCommandValidator : AbstractValidator<CreateImageCommand>
    {
        private readonly int maxTitleLength;
        private readonly int maxAltLength;
        private readonly int maxBlobNameLength;
        private readonly int maxMimeTypeLength;

        public CreateImageCommandValidator()
		{
            maxTitleLength = 150;
            maxAltLength = 400;
            maxBlobNameLength = 100;
            maxMimeTypeLength = 10;

            RuleFor(command => command.Image.Title)
                .NotEmpty()
                .WithMessage("Title is required.")
                .MaximumLength(maxTitleLength)
                .WithMessage($"Title length should not be longer than {maxTitleLength} symbols.");

            RuleFor(command => command.Image.Alt)
                .NotEmpty()
                .WithMessage("Alt is required.")
                .MaximumLength(maxTitleLength)
                .WithMessage($"Alt length should not be longer than {maxAltLength} symbols.");

            RuleFor(command => command.Image.MimeType)
                .NotEmpty()
                .WithMessage("Mime type is required.")
                .MaximumLength(maxTitleLength)
                .WithMessage($"Mime Type length should not be longer than {maxMimeTypeLength} symbols.");
        }
	}
}