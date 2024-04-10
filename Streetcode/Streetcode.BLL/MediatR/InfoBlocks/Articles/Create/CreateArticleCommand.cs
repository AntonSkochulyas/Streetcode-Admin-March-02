// Necessary usings.
using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.InfoBlocks.Articles;

// Necessary namespaces.
namespace Streetcode.BLL.MediatR.InfoBlocks.Articles.Create
{
    /// <summary>
    /// Command, that request a handler to create a new article.
    /// </summary>
    /// <param name="newArticle">
    /// New article to create.
    /// </param>
    public record CreateArticleCommand(ArticleDto? newArticle) : IRequest<Result<ArticleDto>>;
}
