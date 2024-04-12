using FluentValidation;

namespace Streetcode.BLL.MediatR.Timeline.HistoricalContext.Create
{
    public sealed class CreateHistoricalContextCommandValidator : AbstractValidator<CreateHistoricalContextCommand>
    {
        public CreateHistoricalContextCommandValidator()
        {
            ushort maxTitleLength = 50;

            RuleFor(command => command.NewHistoricalContext.Title)
                .MaximumLength(maxTitleLength)
                .WithMessage(string.Format(TimelineErrors.CreateHistoricalContextCommandValidatorMaxTitleLengthError, maxTitleLength));
        }
    }
}
