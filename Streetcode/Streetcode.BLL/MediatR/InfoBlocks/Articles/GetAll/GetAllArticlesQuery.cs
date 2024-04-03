using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.InfoBlocks.Articles;

namespace Streetcode.BLL.MediatR.InfoBlocks.Articles.GetAll
{
    public record GetAllArticlesQuery : IRequest<Result<IEnumerable<ArticleDto>>>;
}
