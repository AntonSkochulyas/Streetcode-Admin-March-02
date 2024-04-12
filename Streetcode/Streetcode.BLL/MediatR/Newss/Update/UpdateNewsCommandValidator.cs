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
        // Max title length
        private readonly int _maxTitleLength;

        // Max URL length
        private readonly int _maxURLLength;

        // Parametric constructor
        public UpdateNewsCommandValidator()
        {
            _maxTitleLength = 150;
            _maxURLLength = 100;

            RuleFor(command => command.news.Title)
                .NotEmpty()
                .WithMessage(NewsErrors.UpdateNewsCommandValidatorTitleIsRequiredError)
                .MaximumLength(_maxTitleLength)
                .WithMessage(string.Format(NewsErrors.UpdateNewsCommandValidatorTitleMaxLengthError, _maxTitleLength));

            RuleFor(command => command.news.Text)
               .NotEmpty()
               .WithMessage(NewsErrors.UpdateNewsCommandValidatorTextIsRequiredError);

            RuleFor(command => command.news.URL)
               .NotEmpty()
               .WithMessage(NewsErrors.UpdateNewsCommandValidatorURlIsRequiredError)
               .MaximumLength(_maxURLLength)
               .WithMessage(NewsErrors.UpdateNewsCommandValidatorURlMaxLengthError);

            RuleFor(command => command.news.CreationDate)
                .NotEmpty()
                .WithMessage(NewsErrors.UpdateNewsCommandValidatorCreationDateIsRequiredError);
        }
    }
}
