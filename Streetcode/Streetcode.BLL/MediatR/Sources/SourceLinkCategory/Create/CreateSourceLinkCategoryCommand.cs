using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.Sources;

namespace Streetcode.BLL.MediatR.Sources.SourceLinkCategory
{
    public record CreateSourceLinkCategoryCommand(CreateSourceLinkDto SourceLinkCategoryContentDto)
        : IRequest<Result<SourceLinkCategoryDto>>;
}