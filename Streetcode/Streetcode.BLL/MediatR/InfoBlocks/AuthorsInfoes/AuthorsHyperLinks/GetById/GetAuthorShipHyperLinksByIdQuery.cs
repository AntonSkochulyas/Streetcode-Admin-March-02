using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.InfoBlocks.AuthorsInfoes.AuthorsHyperLinks;

namespace Streetcode.BLL.MediatR.InfoBlocks.AuthorsInfoes.AuthorsHyperLinks.GetById
{
    public record GetAuthorShipHyperLinksByIdQuery(int Id)
        : IRequest<Result<AuthorShipHyperLinkDto>>;
}
