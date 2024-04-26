// Necessary usings.
using FluentValidation;

// Necessary namespaces.
namespace Streetcode.BLL.MediatR.Newss.Update
{
    /// <summary>
    /// Validator, that validates a model inside UpdateNewsCommand.
    /// </summary>
    internal class UpdateNewsCommandValidator : AbstractValidator<UpdateNewsCommand>
    {
        // Parametric constructor
        public UpdateNewsCommandValidator()
        {
            // Max title length
            int maxTitleLength = 150;

            // Max URL length
            int maxURLLength = 100;

            RuleFor(command => command.News.Title)
                .NotEmpty()
                .WithMessage(NewsErrors.UpdateNewsCommandValidatorTitleIsRequiredError)
                .MaximumLength(maxTitleLength)
                .WithMessage(string.Format(NewsErrors.UpdateNewsCommandValidatorTitleMaxLengthError, maxTitleLength));

            RuleFor(command => command.News.Text)
               .NotEmpty()
               .WithMessage(NewsErrors.UpdateNewsCommandValidatorTextIsRequiredError);

            RuleFor(command => command.News.URL)
               .NotEmpty()
               .WithMessage(NewsErrors.UpdateNewsCommandValidatorURlIsRequiredError)
               .MaximumLength(maxURLLength)
               .WithMessage(NewsErrors.UpdateNewsCommandValidatorURlMaxLengthError);

            RuleFor(command => command.News.CreationDate)
                .NotEmpty()
                .WithMessage(NewsErrors.UpdateNewsCommandValidatorCreationDateIsRequiredError);
        }
    }
}
