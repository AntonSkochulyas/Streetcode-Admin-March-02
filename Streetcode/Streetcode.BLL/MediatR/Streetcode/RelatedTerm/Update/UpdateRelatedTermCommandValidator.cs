﻿using FluentValidation;
using Streetcode.BLL.MediatR.Streetcode.RelatedTerm.Update;

namespace Streetcode.BLL.MediatR.Streetcode.RelatedTerm.Create
{
    public class UpdateRelatedTermCommandValidator : AbstractValidator<UpdateRelatedTermCommand>
    {
        public UpdateRelatedTermCommandValidator()
        {
            int maxWordLength = 50;

            RuleFor(command => command.RelatedTerm.Word)
                .NotEmpty()
                .WithMessage(StreetcodeErrors.UpdateRelatedTermHandlerWordIsRequiredError)
                .MaximumLength(maxWordLength)
                .WithMessage(string.Format(StreetcodeErrors.UpdateRelatedTermHandlerMaxWordLengthError, maxWordLength));

            RuleFor(command => command.RelatedTerm.TermId)
                .NotEmpty()
                .WithMessage(StreetcodeErrors.UpdateRelatedTermHandlerTermIdIsRequiredError);
        }
    }
}