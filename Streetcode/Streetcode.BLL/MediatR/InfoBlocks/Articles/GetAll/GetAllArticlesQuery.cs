// Necessary usings.
using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.InfoBlocks.Articles;

// Necessary namespaces.
namespace Streetcode.BLL.MediatR.InfoBlocks.Articles.GetAll
{
    /// <summary>
    /// Query, that requests a handler to get all articles from the database.
    /// </summary>
    public record GetAllArticlesQuery : IRequest<Result<IEnumerable<ArticleDto>>>;
}
