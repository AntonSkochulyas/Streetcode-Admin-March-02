using System;
using FluentValidation;
using Streetcode.BLL.MediatR.Team.Create;

namespace Streetcode.BLL.MediatR.Timeline.TimelineItem.Create
{
    internal class CreateTimelineItemCommandValidator : AbstractValidator<CreateTimelineItemCommand>
    {
        private readonly int _titleMaxLength;
        private readonly int _descriptionMaxLength;

        public CreateTimelineItemCommandValidator()
        {
            _titleMaxLength = 100;
            _descriptionMaxLength = 600;

            RuleFor(command => command.TimelineItem.Date)
                .NotEmpty()
                .WithMessage(TimelineErrors.CreateTimelineItemCommandValidatorDateIsRequiredError);

            RuleFor(command => command.TimelineItem.DateViewPattern)
                .NotEmpty()
                .WithMessage(TimelineErrors.CreateTimelineItemCommandValidatorDataViewPatternIsRequiredError);

            RuleFor(command => command.TimelineItem.Title)
                .NotEmpty()
                .WithMessage(TimelineErrors.CreateTimelineItemCommandValidatorTitleIsRequiredError)
                .MaximumLength(_titleMaxLength)
                .WithMessage(string.Format(TimelineErrors.CreateTimelineItemCommandValidatorTitleMaxLengthError, _titleMaxLength));

            RuleFor(command => command.TimelineItem.Description)
                .MaximumLength(_descriptionMaxLength)
                .WithMessage(string.Format(TimelineErrors.CreateTimelineItemCommandValidatorDescriptionMaxLengthError, _descriptionMaxLength));
        }
    }
}