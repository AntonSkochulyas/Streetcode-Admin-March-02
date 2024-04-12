// Necessary usings.
using FluentResults;
using MediatR;

// Necessary namespaces.
namespace Streetcode.BLL.MediatR.InfoBlocks.AuthorsInfoes.AuthorsHyperLinks.Delete
{
    /// <summary>
    /// Command, that requests a handler to delete authorship hyperlink by given id.
    /// </summary>
    /// <param name="Id">
    /// Authorship hyperlink id to delete.
    /// </param>
    public record DeleteAuthorShipHyperLinkCommand(int Id)
        : IRequest<Result<Unit>>;
}
