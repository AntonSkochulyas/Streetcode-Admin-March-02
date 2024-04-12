using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.Dictionaries;

namespace Streetcode.BLL.MediatR.Dictionaries.Create
{
    public record CreateDictionaryItemCommand(DictionaryItemDto? NewDictionaryItem)
        : IRequest<Result<DictionaryItemDto>>;
}
