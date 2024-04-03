using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.InfoBlocks.AuthorsInfoes.AuthorsHyperLinks;

namespace Streetcode.BLL.MediatR.InfoBlocks.AuthorsInfoes.AuthorsHyperLinks.Update
{
    public record UpdateAuthorShipHyperLinkCommand(AuthorShipHyperLinkDto? authorsHyperLink) : IRequest<Result<AuthorShipHyperLinkDto>>;
}
