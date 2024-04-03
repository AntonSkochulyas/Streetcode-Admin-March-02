using FluentResults;
using MediatR;

namespace Streetcode.BLL.MediatR.InfoBlocks.Articles.Delete
{
    public record DeleteArticleCommand(int Id) : IRequest<Result<Unit>>;
}
