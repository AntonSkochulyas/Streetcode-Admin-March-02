using FluentValidation;

namespace Streetcode.BLL.MediatR.Media.ImageMain.Create
{
	public class CreateImageMainCommandValidator : AbstractValidator<CreateImageMainCommand>
    {
		public CreateImageMainCommandValidator()
		{
            int maxMimeTypeLength = 10;
            int maxTitleLength = 100;
            int maxAltLength = 100;

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