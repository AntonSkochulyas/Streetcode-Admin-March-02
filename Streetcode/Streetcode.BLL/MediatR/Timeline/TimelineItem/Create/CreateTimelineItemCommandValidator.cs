using FluentValidation;

namespace Streetcode.BLL.MediatR.Timeline.TimelineItem.Create
{
    internal class CreateTimelineItemCommandValidator : AbstractValidator<CreateTimelineItemCommand>
    {
        public CreateTimelineItemCommandValidator()
        {
            int titleMaxLength = 100;
            int descriptionMaxLength = 600;

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
        }
    }
}