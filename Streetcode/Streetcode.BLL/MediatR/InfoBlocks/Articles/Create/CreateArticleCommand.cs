using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.InfoBlocks.Articles;

namespace Streetcode.BLL.MediatR.InfoBlocks.Articles.Create
{
    public record CreateArticleCommand(ArticleDto? NewArticle)
        : IRequest<Result<ArticleDto>>;
}
