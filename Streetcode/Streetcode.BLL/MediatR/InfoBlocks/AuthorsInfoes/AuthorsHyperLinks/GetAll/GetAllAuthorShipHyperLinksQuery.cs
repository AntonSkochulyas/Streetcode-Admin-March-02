using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.InfoBlocks.AuthorsInfoes.AuthorsHyperLinks;

namespace Streetcode.BLL.MediatR.InfoBlocks.AuthorsInfoes.AuthorsHyperLinks.GetAll
{
    public record GetAllAuthorShipHyperLinksQuery : IRequest<Result<IEnumerable<AuthorShipHyperLinkDto>>>;
}
