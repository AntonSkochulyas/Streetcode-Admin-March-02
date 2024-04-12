// Necessary usings.
using FluentValidation;

// Necessary namespaces.
namespace Streetcode.BLL.MediatR.InfoBlocks.AuthorsInfoes.AuthorsHyperLinks.Create
{
    /// <summary>
    /// Validator, that validates a model inside CreateAuthorShipHyperLinkCommand.
    /// </summary>
    public sealed class CreateAuthorShipHyperLinkCommandValidator : AbstractValidator<CreateAuthorShipHyperLinkCommand>
    {
        // Constructor
        public CreateAuthorShipHyperLinkCommandValidator()
        {
            // Max title length
            ushort maxTitleLength = 150;

            RuleFor(command => command.NewAuthorHyperLink.Title)
                .MaximumLength(maxTitleLength)
                .WithMessage("Title length of author hyper link must not be longer than 150 symbols.");
        }
    }
}
