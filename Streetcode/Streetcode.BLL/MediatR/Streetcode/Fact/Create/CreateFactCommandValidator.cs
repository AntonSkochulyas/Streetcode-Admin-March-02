using System;
using FluentValidation;

namespace Streetcode.BLL.MediatR.Streetcode.Fact.Create
{
	public class CreateFactCommandValidator : AbstractValidator<CreateFactCommand>
    {
		private readonly int maxTitleLength;
		private readonly int maxFactContent;

		public CreateFactCommandValidator()
		{
			maxTitleLength = 100;
			maxFactContent = 600;

			RuleFor(command => command.Fact.Title)
                .NotEmpty()
                .WithMessage(StreetcodeErrors.CreateFactCommandValidatorTitleIsRequiredError)
                .MaximumLength(maxTitleLength)
                .WithMessage(string.Format(StreetcodeErrors.CreateFactCommandValidatorTitleMaxLengthError, maxTitleLength));

			RuleFor(command => command.Fact.FactContent)
                .NotEmpty()
                .WithMessage(StreetcodeErrors.CreateFactCommandValidatorFactIsRequiredError)
                .MaximumLength(maxFactContent)
                .WithMessage(string.Format(StreetcodeErrors.CreateFactCommandValidatorFactContentMaxLengthError, maxFactContent));
        }
	}
}