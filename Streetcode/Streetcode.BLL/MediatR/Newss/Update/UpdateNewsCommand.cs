// Necessary usings.
using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.News;

// Necessary namespaces.
namespace Streetcode.BLL.MediatR.Newss.Update
{
    /// <summary>
    /// Command, that requests a handler to 
    /// </summary>
    /// <param name="news"></param>
    public record UpdateNewsCommand(NewsDto News)
        : IRequest<Result<NewsDto>>;
}
