using System;
using FluentValidation;

namespace Streetcode.BLL.MediatR.Streetcode.RelatedTerm.Create
{
    public class CreateRelatedTermCommandValidator : AbstractValidator<CreateRelatedTermCommand>
    {
        private readonly int maxWordLength;

        public CreateRelatedTermCommandValidator()
        {
            maxWordLength = 50;

            RuleFor(command => command.RelatedTerm.Word)
                .NotEmpty()
                .WithMessage(StreetcodeErrors.CreateRelatedTermHandlerWordIsRequiredError)
                .MaximumLength(maxWordLength)
                .WithMessage(string.Format(StreetcodeErrors.CreateRelatedTermHandlerMaxWordLengthError, maxWordLength));

            RuleFor(command => command.RelatedTerm.TermId)
                .NotEmpty()
                .WithMessage(StreetcodeErrors.CreateRelatedTermHandlerTermIdIsRequiredError);
        }
    }
}