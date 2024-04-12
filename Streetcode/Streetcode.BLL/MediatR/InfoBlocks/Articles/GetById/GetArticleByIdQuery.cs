// Necessary usings.
using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.InfoBlocks.Articles;

// Necessary namespaces.
namespace Streetcode.BLL.MediatR.InfoBlocks.Articles.GetById
{
    /// <summary>
    /// Query, that request handler to get an article by given id.
    /// </summary>
    /// <param name="Id">
    /// Article id to get.
    /// </param>
    public record GetArticleByIdQuery(int Id)
        : IRequest<Result<ArticleDto>>;
}
