// Necessary usings.
using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.News;

// Necessary namespaces.
namespace Streetcode.BLL.MediatR.Newss.SortedByDateTime
{
    /// <summary>
    /// Query, that requests a handler to sort a news by date time.
    /// </summary>
    public record SortedByDateTimeQuery() : IRequest<Result<List<NewsDto>>>;
}
