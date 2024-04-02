using FluentValidation;

namespace Streetcode.BLL.MediatR.InfoBlocks.AuthorsInfoes.AuthorsHyperLinks.Create
{
    public sealed class CreateAuthorShipHyperLinkCommandValidator : AbstractValidator<CreateAuthorShipHyperLinkCommand>
    {
        public CreateAuthorShipHyperLinkCommandValidator()
        {
            int maxTitleLength = 150;

            RuleFor(command => command.newAuthorHyperLink.Title)
                .MaximumLength(maxTitleLength)
                .WithMessage("Title length of author hyper link must not be longer than 150 symbols.");
        }
    }
}
