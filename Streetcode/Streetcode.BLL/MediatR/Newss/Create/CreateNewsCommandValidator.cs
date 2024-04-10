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
        // Max title length
        private readonly int _maxTitleLength;

        // Max URL length
        private readonly int _maxURLLength;

        // Constructor
        public CreateNewsCommandValidator()
        {
            _maxTitleLength = 150;
            _maxURLLength = 100;

            RuleFor(command => command.newNews.Title)
                .NotEmpty()
                .WithMessage(NewsErrors.CreateNewsCommandValidatorTitleIsRequiredError)
                .MaximumLength(_maxTitleLength)
                .WithMessage(string.Format(NewsErrors.CreateNewsCommandValidatorTitleMaxLengthError, _maxTitleLength));

            RuleFor(command => command.newNews.Text)
               .NotEmpty()
               .WithMessage(NewsErrors.CreateNewsCommandValidatorTextIsRequiredError);

            RuleFor(command => command.newNews.URL)
               .NotEmpty()
               .WithMessage(NewsErrors.CreateNewsCommandValidatorURLIsRequiredError)
               .MaximumLength(_maxURLLength)
               .WithMessage(string.Format(NewsErrors.CreateNewsCommandValidatorURLMaxLengthError, _maxURLLength));

            RuleFor(command => command.newNews.CreationDate)
                .NotEmpty()
                .WithMessage(NewsErrors.CreateNewsCommandValidatorCreationDateIsRequiredError);
        }
    }
}
