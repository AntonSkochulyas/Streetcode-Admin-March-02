﻿using FluentValidation;
using Streetcode.BLL.Dto.Streetcode;

namespace Streetcode.BLL.MediatR.Streetcode.Streetcode;

public class BaseStreetcodeCommandValidator : AbstractValidator<StreetcodeDto>
{
    public BaseStreetcodeCommandValidator()
    {
        int maxTeaserLength = 650;
        int maxTeaserLengthWithNewLine = 550;
        int maxUrlLength = 100;
        int maxShortSecriptionLength = 33;
        int maxDateStringLength = 50;
        int maxAliasLength = 50;
        int maxTitleLength = 100;
        int maxNumberOfTags = 50;

        RuleFor(command => command.Teaser)
            .Custom((teaser, context) =>
            {
                if(teaser is null)
                {
                    context.AddFailure($"Teaser can not be null.");
                }

                if (teaser.IndexOf('\n') != -1)
                {
                    if (teaser.Length > maxTeaserLengthWithNewLine)
                    {
                        context.AddFailure($"The length of teaser with new line of must not be longer than {maxTeaserLengthWithNewLine} symbols.");
                    }
                }
                else
                {
                    if (teaser.Length > maxTeaserLength)
                    {
                        context.AddFailure($"Teaser length of streetcode must not be longer than {{maxTeaserLength}} symbols.");
                    }
                }
            });

        RuleFor(command => command.TransliterationUrl)
            .MaximumLength(maxUrlLength)
            .WithMessage($"TransliterationUrl length of streetcode must not be longer than {maxUrlLength} symbols.");

        RuleFor(command => command.ShortDescription)
                .MaximumLength(maxShortSecriptionLength)
                .WithMessage($"ShortDescription length of streetcode must not be longer than {maxShortSecriptionLength} symbols.");

        RuleFor(command => command.DateString)
                .MaximumLength(maxDateStringLength)
                .WithMessage($"ShortDescription length of streetcode must not be longer than {maxDateStringLength} symbols.");

        RuleFor(command => command.Alias)
                .MaximumLength(maxAliasLength)
                .WithMessage($"ShortDescription length of streetcode must not be longer than {maxAliasLength} symbols.");

        RuleFor(command => command.Title)
                .MaximumLength(maxTitleLength)
                .WithMessage($"ShortDescription length of streetcode must not be longer than {maxTitleLength} symbols.");

        RuleFor(command => command.Tags)
            .Must(t => t.Count() <= maxNumberOfTags)
            .WithMessage($"Number of tags must not be more than {maxNumberOfTags}.");
    }
}
