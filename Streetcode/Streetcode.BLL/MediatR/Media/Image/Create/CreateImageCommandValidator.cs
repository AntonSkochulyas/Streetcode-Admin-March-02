using FluentValidation;

namespace Streetcode.BLL.MediatR.Media.Image.Create
{
	public class CreateImageCommandValidator : AbstractValidator<CreateImageCommand>
    {
		public CreateImageCommandValidator()
		{
            int maxMimeTypeLength = 10;
            int maxTitleLength = 50;
            int maxAltLength = 50;

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