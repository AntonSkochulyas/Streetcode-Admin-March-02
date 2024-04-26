// Necesasry usings.
using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.News;

// Necessary namespaces.
namespace Streetcode.BLL.MediatR.Newss.Create
{
    /// <summary>
    /// Command, that requests a handler to create a news.
    /// </summary>
    /// <param name="newNews">
    /// New news.
    /// </param>
    public record CreateNewsCommand(NewsCreateDto NewNews)
        : IRequest<Result<NewsDto>>;
}
