using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.News;

namespace Streetcode.BLL.MediatR.Newss.GetByUrl
{
    public record GetNewsByUrlQuery(string Url)
        : IRequest<Result<NewsDto>>;
}
