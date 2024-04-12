// Necessary usings.
using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.News;

// Necessary namespaces.
namespace Streetcode.BLL.MediatR.Newss.GetNewsAndLinksByUrl
{
    /// <summary>
    /// Query, that requests a handler to get a news and links by give url from database.
    /// </summary>
    /// <param name="url"></param>
    public record GetNewsAndLinksByUrlQuery(string url) : IRequest<Result<NewsDtoWithURLs>>;
}
