// Necessary usings.
using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.News;

// Necessary namespaces.
namespace Streetcode.BLL.MediatR.Newss.GetById
{
    /// <summary>
    /// Query, that requests a handler to get a news by given id.
    /// </summary>
    /// <param name="id">
    /// News id to get.
    /// </param>
    public record GetNewsByIdQuery(int Id)
        : IRequest<Result<NewsDto>>;
}
