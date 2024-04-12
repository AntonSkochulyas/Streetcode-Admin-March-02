using FluentResults;
using MediatR;

namespace Streetcode.BLL.MediatR.InfoBlocks.InfoBlockss.Delete
{
    public record DeleteInfoBlockCommand(int Id)
        : IRequest<Result<Unit>>;
}
