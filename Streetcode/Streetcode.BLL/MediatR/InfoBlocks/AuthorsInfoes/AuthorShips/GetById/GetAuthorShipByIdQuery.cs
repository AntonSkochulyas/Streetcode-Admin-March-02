using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.InfoBlocks.AuthorsInfoes;

namespace Streetcode.BLL.MediatR.InfoBlocks.AuthorsInfoes.AuthorShips.GetById
{
    public record GetAuthorShipByIdQuery(int Id) : IRequest<Result<AuthorShipDto>>;
}
