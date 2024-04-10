// Necessary usings
using FluentResults;
using MediatR;

// Necessary namespaces
namespace Streetcode.BLL.MediatR.Dictionaries.Delete
{
    /// <summary>
    /// Command, that request handler to delete dictionary item with given id.
    /// </summary>
    /// <param name="Id">
    /// Dictionary item id to delete.
    /// </param>
    public record DeleteDictionaryItemCommand(int Id) : IRequest<Result<Unit>>;
}
