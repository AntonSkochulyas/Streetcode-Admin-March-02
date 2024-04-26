// Necessary usings
using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.InfoBlocks.Articles;

// Necessary namespaces
namespace Streetcode.BLL.MediatR.InfoBlocks.Articles.Delete
{
    /// <summary>
    /// Command, that request a handler to delte article by given id.
    /// </summary>
    /// <param name="Id"></param>
    public record DeleteArticleCommand(int Id)
        : IRequest<Result<ArticleDto>>;
}
