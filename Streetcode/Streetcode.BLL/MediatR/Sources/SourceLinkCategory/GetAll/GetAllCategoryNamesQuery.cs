// Necessary usings.
using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.Sources;

// Necessary namespaces.
namespace Streetcode.BLL.MediatR.Sources.SourceLinkCategory.GetAll
{
    /// <summary>
    /// Query, that requests a handler to get all category names from database.
    /// </summary>
    public record GetAllCategoryNamesQuery : IRequest<Result<IEnumerable<CategoryWithNameDto>>>
    {
        public GetAllCategoryNamesQuery()
        {
        }
    }
}
