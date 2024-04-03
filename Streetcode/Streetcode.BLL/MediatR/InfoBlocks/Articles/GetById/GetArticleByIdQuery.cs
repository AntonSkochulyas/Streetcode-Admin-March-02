using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.InfoBlocks.Articles;

namespace Streetcode.BLL.MediatR.InfoBlocks.Articles.GetById
{
    public record GetArticleByIdQuery(int Id) : IRequest<Result<ArticleDto>>;
}
