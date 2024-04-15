// Necessary usings.
using FluentValidation;

// Necessary namespaces.
namespace Streetcode.BLL.MediatR.InfoBlocks.AuthorsInfoes.AuthorsHyperLinks.Update
{
    /// <summary>
    /// Validator, that validates a model inside UpdateAuthorShipHyperLinkCommand.
    /// </summary>
    internal class UpdateAuthorShipHyperLinkCommandValidator : AbstractValidator<UpdateAuthorShipHyperLinkCommand>
    {
        // Constructor
        public UpdateAuthorShipHyperLinkCommandValidator()
        {
            // Max title length
            int maxTitleLength = 150;

            RuleFor(command => command.AuthorsHyperLink.Title)
                .MaximumLength(maxTitleLength)
                .WithMessage("Title length of author hyper link must not be longer than 150 symbols.");
        }
    }
}
