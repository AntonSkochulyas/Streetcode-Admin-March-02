using System;
using FluentValidation;
using Streetcode.BLL.MediatR.Streetcode.RelatedTerm.Update;

namespace Streetcode.BLL.MediatR.Streetcode.RelatedTerm.Create
{
    public class UpdateRelatedTermCommandValidator : AbstractValidator<UpdateRelatedTermCommand>
    {
        private readonly int maxWordLength;

        public UpdateRelatedTermCommandValidator()
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