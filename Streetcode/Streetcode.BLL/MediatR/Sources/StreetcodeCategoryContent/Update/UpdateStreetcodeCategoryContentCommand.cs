using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.Sources;

namespace Streetcode.BLL.MediatR.Sources.StreetcodeCategoryContent.Update
{
    public record UpdateStreetcodeCategoryContentCommand(StreetcodeCategoryContentDto StreetcodeCategoryContentDto) : IRequest<Result<StreetcodeCategoryContentDto>>;
}
