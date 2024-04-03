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
                .WithMessage("Word is required.")
                .MaximumLength(maxWordLength)
                .WithMessage($"Word length should not be longer than {maxWordLength} symbols.");

            RuleFor(command => command.RelatedTerm.TermId)
                .NotEmpty()
                .WithMessage("Term Id is required.");
        }
    }
}