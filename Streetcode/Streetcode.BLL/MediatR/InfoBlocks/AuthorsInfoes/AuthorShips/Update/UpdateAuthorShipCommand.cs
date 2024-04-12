// Necessary usings.
using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.InfoBlocks.AuthorsInfoes;

// Necessary namespaces.
namespace Streetcode.BLL.MediatR.InfoBlocks.AuthorsInfoes.AuthorShips.Update
{
    /// <summary>
    /// Command, that requests a handler to update an authorship.
    /// </summary>
    /// <param name="authorShip">
    /// Updated authorship.
    /// </param>
    public record UpdateAuthorShipCommand(AuthorShipDto? AuthorShip)
        : IRequest<Result<AuthorShipDto>>;
}
