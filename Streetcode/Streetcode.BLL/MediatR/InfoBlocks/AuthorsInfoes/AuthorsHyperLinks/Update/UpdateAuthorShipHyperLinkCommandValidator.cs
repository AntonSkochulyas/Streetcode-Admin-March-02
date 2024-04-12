using FluentValidation;

namespace Streetcode.BLL.MediatR.InfoBlocks.AuthorsInfoes.AuthorsHyperLinks.Update
{
    internal class UpdateAuthorShipHyperLinkCommandValidator : AbstractValidator<UpdateAuthorShipHyperLinkCommand>
    {
        public UpdateAuthorShipHyperLinkCommandValidator()
        {
            int maxTitleLength = 150;

            RuleFor(command => command.AuthorsHyperLink.Title)
                .MaximumLength(maxTitleLength)
                .WithMessage("Title length of author hyper link must not be longer than 150 symbols.");
        }
    }
}
