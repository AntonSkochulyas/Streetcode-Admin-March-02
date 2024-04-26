using System;
using FluentValidation;
using Streetcode.BLL.MediatR.Fact.Update;

namespace Streetcode.BLL.MediatR.Streetcode.Fact.Update
{
	public class UpdateFactCommandValidator : AbstractValidator<UpdateFactCommand>
    {
        public UpdateFactCommandValidator()
		{
            int maxTitleLength = 100;
            int maxFactContent = 600;

            RuleFor(command => command.Fact.Title)
                .NotEmpty()
                .WithMessage(StreetcodeErrors.UpdateFactCommandValidatorTitleIsRequiredError)
                .MaximumLength(maxTitleLength)
                .WithMessage(string.Format(StreetcodeErrors.UpdateFactCommandValidatorTitleMaxLengthError, maxTitleLength));

            RuleFor(command => command.Fact.FactContent)
                .NotEmpty()
                .WithMessage(StreetcodeErrors.UpdateFactCommandValidatorFactIsRequiredError)
                .MaximumLength(maxFactContent)
                .WithMessage(string.Format(StreetcodeErrors.UpdateFactCommandValidatorFactContentMaxLengthError, maxFactContent));
        }
	}
}