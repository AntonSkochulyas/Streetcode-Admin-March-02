using FluentResults;
using MediatR;

namespace Streetcode.BLL.MediatR.Dictionaries.Delete
{
    public record DeleteDictionaryItemCommand(int Id) : IRequest<Result<Unit>>;
}
