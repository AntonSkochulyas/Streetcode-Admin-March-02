using FluentValidation;

namespace Streetcode.BLL.MediatR.Streetcode.Fact.Create
{
    public class CreateFactCommandValidator : AbstractValidator<CreateFactCommand>
    {
        private readonly ushort _maxTitleLength;
        private readonly ushort _maxFactContent;

        public CreateFactCommandValidator()
        {
            _maxTitleLength = 100;
            _maxFactContent = 600;

            RuleFor(command => command.Fact.Title)
                .NotEmpty()
                .WithMessage(StreetcodeErrors.CreateFactCommandValidatorTitleIsRequiredError)
                .MaximumLength(_maxTitleLength)
                .WithMessage(string.Format(StreetcodeErrors.CreateFactCommandValidatorTitleMaxLengthError, _maxTitleLength));

            RuleFor(command => command.Fact.FactContent)
                .NotEmpty()
                .WithMessage(StreetcodeErrors.CreateFactCommandValidatorFactIsRequiredError)
                .MaximumLength(_maxFactContent)
                .WithMessage(string.Format(StreetcodeErrors.CreateFactCommandValidatorFactContentMaxLengthError, _maxFactContent));
        }
    }
}