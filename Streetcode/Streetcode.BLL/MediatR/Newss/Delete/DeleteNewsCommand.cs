using FluentResults;
using MediatR;

namespace Streetcode.BLL.MediatR.Newss.Delete
{
    public record DeleteNewsCommand(int Id)
        : IRequest<Result<Unit>>;
}
