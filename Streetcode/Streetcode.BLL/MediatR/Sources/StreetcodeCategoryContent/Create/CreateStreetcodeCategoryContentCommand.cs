using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.Sources;

namespace Streetcode.BLL.MediatR.Sources.StreetcodeCategoryContent.Create
{
    public record CreateStreetcodeCategoryContentCommand(StreetcodeCategoryContentDto StreetcodeCategoryContentDto) : IRequest<Result<CategoryContentCreatedDto>>;
}
