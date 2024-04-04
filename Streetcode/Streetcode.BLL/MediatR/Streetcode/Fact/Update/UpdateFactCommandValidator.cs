using System;
using FluentValidation;
using Streetcode.BLL.MediatR.Fact.Update;

namespace Streetcode.BLL.MediatR.Streetcode.Fact.Update
{
	public class UpdateFactCommandValidator : AbstractValidator<UpdateFactCommand>
    {
        private readonly int maxTitleLength;
        private readonly int maxFactContent;

        public UpdateFactCommandValidator()
		{
            maxTitleLength = 100;
            maxFactContent = 600;

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