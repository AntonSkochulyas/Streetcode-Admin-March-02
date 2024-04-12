using FluentValidation;

namespace Streetcode.BLL.MediatR.Newss.Update
{
    internal class UpdateNewsCommandValidator : AbstractValidator<UpdateNewsCommand>
    {
        public UpdateNewsCommandValidator()
        {
            int maxTitleLength = 150;
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
