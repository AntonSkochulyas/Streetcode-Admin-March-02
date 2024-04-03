using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.Dictionaries;

namespace Streetcode.BLL.MediatR.Dictionaries.Update
{
    public record UpdateDictionaryItemCommand(DictionaryItemDto dictionaryItem) : IRequest<Result<DictionaryItemDto>>;
}
