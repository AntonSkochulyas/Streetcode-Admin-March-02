using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.News;

namespace Streetcode.BLL.MediatR.Newss.Update
{
    public record UpdateNewsCommand(NewsDto News)
        : IRequest<Result<NewsDto>>;
}
