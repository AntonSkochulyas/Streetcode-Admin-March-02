using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.Sources;

namespace Streetcode.BLL.MediatR.Sources.StreetcodeCategoryContent.Delete
{
    public record DeleteStreetcodeCategoryContentCommand(int sourceLinkCategoryId, int streetcodeId) : IRequest<Result<StreetcodeCategoryContentDto>>;
}
