// Necessary usings.
using FluentResults;
using MediatR;

// Necessary namespaces.
namespace Streetcode.BLL.MediatR.InfoBlocks.InfoBlockss.Delete
{
    /// <summary>
    /// Command, that request a handler to delete infoblock.
    /// </summary>
    /// <param name="Id">
    /// Infoblock id to delete.
    /// </param>
    public record DeleteInfoBlockCommand(int Id) : IRequest<Result<Unit>>;
}
