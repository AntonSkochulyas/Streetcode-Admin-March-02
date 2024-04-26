using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.Sources;

namespace Streetcode.BLL.MediatR.Sources.StreetcodeCategoryContent.GetCategoryContentByStreetcodeId
{
    public record GetCategoryContentByStreetcodeIdQuery(int StreetcodeId, int CategoryId)
        : IRequest<Result<StreetcodeCategoryContentDto>>;
}
