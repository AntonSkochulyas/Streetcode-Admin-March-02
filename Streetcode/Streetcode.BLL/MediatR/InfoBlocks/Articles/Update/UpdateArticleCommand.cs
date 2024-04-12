// Necessary usings.
using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.InfoBlocks.Articles;

// Necessary namespaces.
namespace Streetcode.BLL.MediatR.InfoBlocks.Articles.Update
{
    /// <summary>
    /// Command, that request a handler to update an article.
    /// </summary>
    /// <param name="article">
    /// Article to update.
    /// </param>
    public record UpdateArticleCommand(ArticleDto Article)
        : IRequest<Result<ArticleDto>>;
}
