using FluentValidation;

namespace Streetcode.BLL.MediatR.Timeline.TimelineItem.Update
{
    internal class UpdateTimelineItemCommandValidator : AbstractValidator<UpdateTimelineItemCommand>
    {
        public UpdateTimelineItemCommandValidator()
        {
            int titleMaxLength = 100;
            int descriptionMaxLength = 600;

            RuleFor(command => command.TimelineItem.Date)
                .NotEmpty()
                .WithMessage(TimelineErrors.UpdateTimelineItemCommandValidatorDateIsRequiredError);

            RuleFor(command => command.TimelineItem.DateViewPattern)
                .NotEmpty()
                .WithMessage(TimelineErrors.CreateTimelineItemCommandValidatorDataViewPatternIsRequiredError);

            RuleFor(command => command.TimelineItem.Title)
                .NotEmpty()
                .WithMessage(TimelineErrors.UpdateTimelineItemCommandValidatorTitleIsRequiredError)
                .MaximumLength(titleMaxLength)
                .WithMessage(string.Format(TimelineErrors.UpdateTimelineItemCommandValidatorTitleMaxLengthError, titleMaxLength));

            RuleFor(command => command.TimelineItem.Description)
                .MaximumLength(descriptionMaxLength)
                .WithMessage(string.Format(TimelineErrors.UpdateTimelineItemCommandValidatorDescriptionMaxLengthError, descriptionMaxLength));
        }
    }
}