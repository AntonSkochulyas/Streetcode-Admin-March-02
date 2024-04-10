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
        // Max title length
        private readonly ushort _maxTitleLength;

        // Constructor
        public UpdateAuthorShipHyperLinkCommandValidator()
        {
            _maxTitleLength = 150;

            RuleFor(command => command.authorsHyperLink.Title)
                .MaximumLength(_maxTitleLength)
                .WithMessage($"Title length of author hyper link must not be longer than {_maxTitleLength} symbols.");
        }
    }
}
