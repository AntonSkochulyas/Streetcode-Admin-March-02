using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.News;

namespace Streetcode.BLL.MediatR.Newss.GetNewsAndLinksByUrl
{
    public record GetNewsAndLinksByUrlQuery(string Url)
        : IRequest<Result<NewsDtoWithURLs>>;
}
