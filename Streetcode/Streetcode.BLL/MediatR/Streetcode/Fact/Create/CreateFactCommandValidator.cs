// Necessary usings.
using FluentValidation;

// Necessaru namespaces.
namespace Streetcode.BLL.MediatR.Streetcode.Fact.Create
{
    /// <summary>
    /// Validator, that validates a model inside CreateFactCommand.
    /// </summary>
	public class CreateFactCommandValidator : AbstractValidator<CreateFactCommand>
    {
        // Constructor
        public CreateFactCommandValidator()
		{
            // Title max length
            int maxTitleLength = 100;

            // Fact max Content
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