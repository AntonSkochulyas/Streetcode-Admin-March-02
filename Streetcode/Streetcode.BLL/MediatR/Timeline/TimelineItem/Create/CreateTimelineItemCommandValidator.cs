// Necessary usings.
using FluentValidation;

// Necessary namespaces.
namespace Streetcode.BLL.MediatR.Timeline.TimelineItem.Create
{
    /// <summary>
    /// Validator, that validates a model inside CreateTimelineItemCommand.
    /// </summary>
    internal class CreateTimelineItemCommandValidator : AbstractValidator<CreateTimelineItemCommand>
    {
        // Constructor
        public CreateTimelineItemCommandValidator()
        {
            // Title max length
            int titleMaxLength = 100;

            // Description max length
            int descriptionMaxLength = 600;

            int descriptionContextLength = 50;

            RuleFor(command => command.TimelineItem.Date)
                .NotEmpty()
                .WithMessage(TimelineErrors.CreateTimelineItemCommandValidatorDateIsRequiredError);

            RuleFor(command => command.TimelineItem.DateViewPattern)
                .NotEmpty()
                .WithMessage(TimelineErrors.CreateTimelineItemCommandValidatorDataViewPatternIsRequiredError);

            RuleFor(command => command.TimelineItem.Title)
                .NotEmpty()
                .WithMessage(TimelineErrors.CreateTimelineItemCommandValidatorTitleIsRequiredError)
                .MaximumLength(titleMaxLength)
                .WithMessage(string.Format(TimelineErrors.CreateTimelineItemCommandValidatorTitleMaxLengthError, titleMaxLength));

            RuleFor(command => command.TimelineItem.Description)
                .MaximumLength(descriptionMaxLength)
                .WithMessage(string.Format(TimelineErrors.CreateTimelineItemCommandValidatorDescriptionMaxLengthError, descriptionMaxLength));

            RuleFor(command => command.TimelineItem.Context)
                .MaximumLength(descriptionContextLength)
                .WithMessage(string.Format(TimelineErrors.CreateTimelineItemCommandValidatorDescriptionMaxLengthError, descriptionContextLength));
        }
    }
}