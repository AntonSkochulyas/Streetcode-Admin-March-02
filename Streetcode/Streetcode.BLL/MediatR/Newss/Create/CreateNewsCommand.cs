using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.News;

namespace Streetcode.BLL.MediatR.Newss.Create
{
    public record CreateNewsCommand(NewsDto NewNews)
        : IRequest<Result<NewsDto>>;
}
