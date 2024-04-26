// Necessary usings.
using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.Dictionaries;

// Necessary namespaces.
namespace Streetcode.BLL.MediatR.Dictionaries.GetById
{
    /// <summary>
    /// Query, that requests a handler get dictionary item by given id.
    /// </summary>
    /// <param name="Id">
    /// Dictionary item id to find.
    /// </param>
    public record GetDictionaryItemByIdQuery(int Id)
        : IRequest<Result<DictionaryItemDto>>;
}
