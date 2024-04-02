using FluentValidation;

namespace Streetcode.BLL.MediatR.Newss.Create
{
    internal class CreateNewsCommandValidator : AbstractValidator<CreateNewsCommand>
    {
        private readonly int _maxTitleLength;
        private readonly int _maxURLLength;
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
