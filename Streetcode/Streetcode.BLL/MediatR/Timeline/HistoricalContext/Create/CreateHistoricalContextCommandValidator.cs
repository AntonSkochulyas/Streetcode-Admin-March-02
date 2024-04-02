using FluentValidation;

namespace Streetcode.BLL.MediatR.Timeline.HistoricalContext.Create
{
    public sealed class CreateHistoricalContextCommandValidator : AbstractValidator<CreateHistoricalContextCommand>
    {
        private readonly ushort _maxTitleLength;
        public CreateHistoricalContextCommandValidator()
        {
            _maxTitleLength = 50;

            RuleFor(command => command.NewHistoricalContext.Title)
                .MaximumLength(_maxTitleLength)
                .WithMessage(string.Format(TimelineErrors.CreateHistoricalContextCommandValidatorMaxTitleLengthError, _maxTitleLength));
        }
    }
}
