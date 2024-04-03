using FluentResults;
using MediatR;

namespace Streetcode.BLL.MediatR.InfoBlocks.AuthorsInfoes.AuthorShips.Delete
{
    public record DeleteAuthorShipCommand(int Id) : IRequest<Result<Unit>>;
}
