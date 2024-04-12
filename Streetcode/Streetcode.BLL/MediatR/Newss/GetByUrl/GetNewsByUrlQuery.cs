// Necessary usings.
using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.News;

// Necessary namespaces.
namespace Streetcode.BLL.MediatR.Newss.GetByUrl
{
    /// <summary>
    /// Query, that requests a handler to get a news by given url.
    /// </summary>
    /// <param name="url">
    /// New url to get
    /// </param>
    public record GetNewsByUrlQuery(string url) : IRequest<Result<NewsDto>>;
}
