// Necessary usings.
using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.InfoBlocks.AuthorsInfoes;

// Necesasry namespaces.
namespace Streetcode.BLL.MediatR.InfoBlocks.AuthorsInfoes.AuthorShips.Create
{
    /// <summary>
    /// Command, that request a handler to create a new authorship.
    /// </summary>
    /// <param name="newAuthorShip">
    /// Authorship to create.
    /// </param>
    public record CreateAuthorShipCommand(AuthorShipCreateDto? NewAuthorShip)
        : IRequest<Result<AuthorShipDto>>;
}
