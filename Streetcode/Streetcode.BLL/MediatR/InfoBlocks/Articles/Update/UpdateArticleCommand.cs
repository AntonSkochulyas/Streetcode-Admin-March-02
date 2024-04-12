using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.InfoBlocks.Articles;

namespace Streetcode.BLL.MediatR.InfoBlocks.Articles.Update
{
    public record UpdateArticleCommand(ArticleDto Article)
        : IRequest<Result<ArticleDto>>;
}
