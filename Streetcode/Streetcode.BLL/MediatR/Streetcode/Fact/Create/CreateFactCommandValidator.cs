using System;
using FluentValidation;

namespace Streetcode.BLL.MediatR.Streetcode.Fact.Create
{
	public class CreateFactCommandValidator : AbstractValidator<CreateFactCommand>
    {
		public CreateFactCommandValidator()
		{
			int maxTitleLength = 100;
			int maxFactContent = 600;

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