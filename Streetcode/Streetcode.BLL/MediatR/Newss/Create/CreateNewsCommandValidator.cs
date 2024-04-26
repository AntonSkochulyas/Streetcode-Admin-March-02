// Necessary usings.
using FluentValidation;

// Necessary namespaces.
namespace Streetcode.BLL.MediatR.Newss.Create
{
    /// <summary>
    /// Validator, that validates a model inside a CreateNewsCommand.
    /// </summary>
    internal class CreateNewsCommandValidator : AbstractValidator<CreateNewsCommand>
    {
        // Constructor
        public CreateNewsCommandValidator()
        {
            // Max title length
            int maxTitleLength = 150;

            // Max URL length
            int maxURLLength = 100;

            RuleFor(command => command.NewNews.Title)
                .NotEmpty()
                .WithMessage(NewsErrors.CreateNewsCommandValidatorTitleIsRequiredError)
                .MaximumLength(maxTitleLength)
                .WithMessage(string.Format(NewsErrors.CreateNewsCommandValidatorTitleMaxLengthError, maxTitleLength));

            RuleFor(command => command.NewNews.Text)
               .NotEmpty()
               .WithMessage(NewsErrors.CreateNewsCommandValidatorTextIsRequiredError);

            RuleFor(command => command.NewNews.URL)
               .NotEmpty()
               .WithMessage(NewsErrors.CreateNewsCommandValidatorURLIsRequiredError)
               .MaximumLength(maxURLLength)
               .WithMessage(string.Format(NewsErrors.CreateNewsCommandValidatorURLMaxLengthError, maxURLLength));

            RuleFor(command => command.NewNews.CreationDate)
                .NotEmpty()
                .WithMessage(NewsErrors.CreateNewsCommandValidatorCreationDateIsRequiredError);
        }
    }
}
