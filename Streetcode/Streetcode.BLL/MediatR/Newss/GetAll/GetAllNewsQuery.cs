// Necessary usings.
using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.News;

// Necessary namespaces.
namespace Streetcode.BLL.MediatR.Newss.GetAll
{
    /// <summary>
    /// Query, that requests a handler to get all news from database.
    /// </summary>
    public record GetAllNewsQuery : IRequest<Result<IEnumerable<NewsDto>>>
    {
        public GetAllNewsQuery()
        {
        }
    }
}
