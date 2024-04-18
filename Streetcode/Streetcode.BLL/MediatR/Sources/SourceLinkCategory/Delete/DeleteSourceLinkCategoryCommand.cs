using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.Sources;

namespace Streetcode.BLL.MediatR.Sources.SourceLinkCategory.Delete
{
    public record DeleteSourceLinkCategoryCommand(int Id)
        : IRequest<Result<SourceLinkResponseDto>>;
}
