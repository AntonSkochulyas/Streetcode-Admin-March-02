// Necessary usings.
using FluentValidation;

// Necessary namespaces.
namespace Streetcode.BLL.MediatR.Streetcode.RelatedTerm.Create
{
    /// <summary>
    /// Validator, that validates a model inside CreateRelatedTermCommand.
    /// </summary>
    public class CreateRelatedTermCommandValidator : AbstractValidator<CreateRelatedTermCommand>
    {
        // Constructor
        public CreateRelatedTermCommandValidator()
        {
            // Word max length
            int maxWordLength = 50;

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