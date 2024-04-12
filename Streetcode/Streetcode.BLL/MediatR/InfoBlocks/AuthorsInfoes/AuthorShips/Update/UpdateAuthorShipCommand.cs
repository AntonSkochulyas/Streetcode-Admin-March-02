using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.InfoBlocks.AuthorsInfoes;

namespace Streetcode.BLL.MediatR.InfoBlocks.AuthorsInfoes.AuthorShips.Update
{
    public record UpdateAuthorShipCommand(AuthorShipDto? AuthorShip)
        : IRequest<Result<AuthorShipDto>>;
}
