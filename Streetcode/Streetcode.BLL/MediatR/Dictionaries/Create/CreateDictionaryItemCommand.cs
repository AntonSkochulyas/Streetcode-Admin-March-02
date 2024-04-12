// Necessary usings
using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.Dictionaries;

// Necessary namespaces
namespace Streetcode.BLL.MediatR.Dictionaries.Create
{
    /// <summary>
    /// Command, that requests handler to create new dictionary item.
    /// </summary>
    /// <param name="newDictionaryItem">
    /// New dictionary item to add in database.
    /// </param>
    public record CreateDictionaryItemCommand(DictionaryItemDto? newDictionaryItem) : IRequest<Result<DictionaryItemDto>>;
}
