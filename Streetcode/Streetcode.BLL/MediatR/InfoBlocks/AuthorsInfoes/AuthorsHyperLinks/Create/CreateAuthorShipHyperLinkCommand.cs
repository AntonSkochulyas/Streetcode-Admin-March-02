using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.InfoBlocks.AuthorsInfoes.AuthorsHyperLinks;

namespace Streetcode.BLL.MediatR.InfoBlocks.AuthorsInfoes.AuthorsHyperLinks.Create
{
    public record CreateAuthorShipHyperLinkCommand(AuthorShipHyperLinkDto? newAuthorHyperLink) : IRequest<Result<AuthorShipHyperLinkDto>>;
}
