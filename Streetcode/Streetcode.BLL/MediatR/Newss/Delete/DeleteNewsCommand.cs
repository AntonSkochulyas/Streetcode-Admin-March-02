// Necessary usings.
using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.News;

// Necessary namespaces.
namespace Streetcode.BLL.MediatR.Newss.Delete
{
    /// <summary>
    /// Command, that requests a handler to delete a news.
    /// </summary>
    /// <param name="id">
    /// News id to delete.
    /// </param>
    public record DeleteNewsCommand(int Id)
        : IRequest<Result<NewsDto>>;
}
