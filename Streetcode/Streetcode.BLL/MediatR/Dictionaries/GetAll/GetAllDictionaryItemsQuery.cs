// Necessary usings.
using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.Dictionaries;

// Necessary namespaces.
namespace Streetcode.BLL.MediatR.Dictionaries.GetAll
{
    /// <summary>
    /// Query, that requests a handler to get all dictionary items.
    /// </summary>
    public record GetAllDictionaryItemsQuery : IRequest<Result<IEnumerable<DictionaryItemDto>>>;
}
