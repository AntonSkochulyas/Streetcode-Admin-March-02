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
        // Max title length
        private readonly ushort _maxTitleLength;

        // Constructor
        public CreateAuthorShipHyperLinkCommandValidator()
        {
            _maxTitleLength = 150;

            RuleFor(command => command.newAuthorHyperLink.Title)
                .MaximumLength(_maxTitleLength)
                .WithMessage($"Title length of author hyper link must not be longer than {_maxTitleLength} symbols.");
        }
    }
}
