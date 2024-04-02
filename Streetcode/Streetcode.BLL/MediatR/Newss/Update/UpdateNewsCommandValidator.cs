using FluentValidation;

namespace Streetcode.BLL.MediatR.Newss.Update
{
    internal class UpdateNewsCommandValidator : AbstractValidator<UpdateNewsCommand>
    {
        private readonly int _maxTitleLength;
        private readonly int _maxURLLength;
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
