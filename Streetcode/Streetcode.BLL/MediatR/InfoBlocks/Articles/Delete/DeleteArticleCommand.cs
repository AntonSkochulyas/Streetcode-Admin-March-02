// Necessary usings
using FluentResults;
using MediatR;

// Necessary namespaces
namespace Streetcode.BLL.MediatR.InfoBlocks.Articles.Delete
{
    /// <summary>
    /// Command, that request a handler to delte article by given id.
    /// </summary>
    /// <param name="Id"></param>
    public record DeleteArticleCommand(int Id) : IRequest<Result<Unit>>;
}
