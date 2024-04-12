// Necessary usings
using FluentResults;
using MediatR;

// Necessary namespaces
namespace Streetcode.BLL.MediatR.InfoBlocks.AuthorsInfoes.AuthorShips.Delete
{
    /// <summary>
    /// Command, that request a handler to delete authorship by given id.
    /// </summary>
    /// <param name="Id">
    /// Authorship id to delete.
    /// </param>
    public record DeleteAuthorShipCommand(int Id)
        : IRequest<Result<Unit>>;
}
