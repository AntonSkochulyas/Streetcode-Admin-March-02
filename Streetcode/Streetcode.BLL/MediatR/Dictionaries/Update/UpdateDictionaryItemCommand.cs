// Necessary usings.
using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.Dictionaries;

// Necessary namespaces.
namespace Streetcode.BLL.MediatR.Dictionaries.Update
{
    /// <summary>
    /// Command, that request handler to update a dictionary item.
    /// </summary>
    /// <param name="dictionaryItem">
    /// Updated dictionary item.
    /// </param>
    public record UpdateDictionaryItemCommand(DictionaryItemDto dictionaryItem)
        : IRequest<Result<DictionaryItemDto>>;
}
