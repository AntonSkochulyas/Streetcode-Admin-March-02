using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.Dictionaries;

namespace Streetcode.BLL.MediatR.Dictionaries.GetById
{
    public record GetDictionaryItemByIdQuery(int Id) : IRequest<Result<DictionaryItemDto>>;
}
