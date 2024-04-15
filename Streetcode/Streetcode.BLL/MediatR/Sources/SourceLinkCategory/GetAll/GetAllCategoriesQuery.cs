using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.Sources;

namespace Streetcode.BLL.MediatR.Sources.SourceLinkCategory.GetAll
{
    public record GetAllCategoriesQuery : IRequest<Result<IEnumerable<SourceLinkCategoryDto>>>
    {
        public GetAllCategoriesQuery()
        {
        }
    }
}
