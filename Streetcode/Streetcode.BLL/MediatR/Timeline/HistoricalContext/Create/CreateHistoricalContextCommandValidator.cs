// Necessary usings.
using FluentValidation;

// Necessary namespaces.
namespace Streetcode.BLL.MediatR.Timeline.HistoricalContext.Create
{
    /// <summary>
    /// Validator, that validates a model inside CreateHistoricalContextCommand.
    /// </summary>
    public sealed class CreateHistoricalContextCommandValidator : AbstractValidator<CreateHistoricalContextCommand>
    {
        // Constructor
        public CreateHistoricalContextCommandValidator()
        {
            // Title max length
            ushort maxTitleLength = 50;

            RuleFor(command => command.NewHistoricalContext.Title)
                .MaximumLength(maxTitleLength)
                .WithMessage(string.Format(TimelineErrors.CreateHistoricalContextCommandValidatorMaxTitleLengthError, maxTitleLength));
        }
    }
}
