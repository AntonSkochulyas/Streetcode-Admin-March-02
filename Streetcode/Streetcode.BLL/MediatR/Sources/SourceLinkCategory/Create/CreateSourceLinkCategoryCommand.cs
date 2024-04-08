using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.Sources;

namespace Streetcode.BLL.MediatR.Sources.SourceLinkCategory
{
    public record CreateSourceLinkCategoryCommand(CreateSourceLinkCategoryContentDto SourceLinkCategoryContentDto) : IRequest<Result<SourceLinkCategoryDto>>;
}