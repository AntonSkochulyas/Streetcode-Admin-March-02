// Necessary usings.
using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.Sources;

// Necessary namespaces.
namespace Streetcode.BLL.MediatR.Sources.SourceLinkCategory.GetAll
{
    /// <summary>
    /// Query, that requests a handler to get all categories from database.
    /// </summary>
    public record GetAllCategoriesQuery : IRequest<Result<IEnumerable<SourceLinkCategoryDto>>>
    {
        public GetAllCategoriesQuery()
        {
        }
    }
}
