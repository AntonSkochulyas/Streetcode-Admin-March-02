using FluentValidation;

namespace Streetcode.BLL.MediatR.Newss.Create
{
    internal class CreateNewsCommandValidator : AbstractValidator<CreateNewsCommand>
    {
        public CreateNewsCommandValidator()
        {
            int maxTitleLength = 150;
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
