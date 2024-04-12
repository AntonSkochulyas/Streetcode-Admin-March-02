using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.News;

namespace Streetcode.BLL.MediatR.Newss.GetById
{
    public record GetNewsByIdQuery(int Id)
        : IRequest<Result<NewsDto>>;
}
