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
                .WithMessage(MediaErrors.CreateImageCommandValidatorMimeTypeIsRequiredError)
                .MaximumLength(maxMimeTypeLength)
                .WithMessage(string.Format(MediaErrors.CreateImageCommandValidatorMimeTypeMaxLengthError, maxMimeTypeLength));

			RuleFor(command => command.Image.Title)
                .MaximumLength(maxMimeTypeLength)
                .WithMessage(string.Format(MediaErrors.CreateImageCommandValidatorTitleMaxLengthError, maxTitleLength));

			RuleFor(command => command.Image.Alt)
                .MaximumLength(maxMimeTypeLength)
                .WithMessage(string.Format(MediaErrors.CreateImageCommandValidatorAltMaxLengthError, maxAltLength));
        }
	}
}