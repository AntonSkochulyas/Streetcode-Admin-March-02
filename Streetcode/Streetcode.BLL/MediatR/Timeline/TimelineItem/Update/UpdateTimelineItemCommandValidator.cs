using FluentValidation;

namespace Streetcode.BLL.MediatR.Timeline.TimelineItem.Update
{
    internal class UpdateTimelineItemCommandValidator : AbstractValidator<UpdateTimelineItemCommand>
    {
        private readonly int _titleMaxLength;
        private readonly int _descriptionMaxLength;

        public UpdateTimelineItemCommandValidator()
        {
            _titleMaxLength = 100;
            _descriptionMaxLength = 600;

            RuleFor(command => command.TimelineItem.Date)
                .NotEmpty()
                .WithMessage(TimelineErrors.UpdateTimelineItemCommandValidatorDateIsRequiredError);

            RuleFor(command => command.TimelineItem.DateViewPattern)
                .NotEmpty()
                .WithMessage(TimelineErrors.CreateTimelineItemCommandValidatorDataViewPatternIsRequiredError);

            RuleFor(command => command.TimelineItem.Title)
                .NotEmpty()
                .WithMessage(TimelineErrors.UpdateTimelineItemCommandValidatorTitleIsRequiredError)
                .MaximumLength(_titleMaxLength)
                .WithMessage(string.Format(TimelineErrors.UpdateTimelineItemCommandValidatorTitleMaxLengthError, _titleMaxLength));

            RuleFor(command => command.TimelineItem.Description)
                .MaximumLength(_descriptionMaxLength)
                .WithMessage(string.Format(TimelineErrors.UpdateTimelineItemCommandValidatorDescriptionMaxLengthError, _descriptionMaxLength));
        }
    }
}