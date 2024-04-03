using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.Dictionaries;

namespace Streetcode.BLL.MediatR.Dictionaries.GetAll
{
    public record GetAllDictionaryItemsQuery : IRequest<Result<IEnumerable<DictionaryItemDto>>>;
}
