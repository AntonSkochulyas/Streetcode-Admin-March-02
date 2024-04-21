// Necessary usings.
using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.Sources;

// Necessary namespaces.
namespace Streetcode.BLL.MediatR.Sources.SourceLinkCategory
{
    /// <summary>
    /// Command, that requests a handler to create a new source link.
    /// </summary>
    /// <param name="newSourceLink">
    /// New source link.
    /// </param>
    public record CreateSourceLinkCategoryCommand(CreateSourceLinkDto SourceLinkCategoryContentDto)
        : IRequest<Result<SourceLinkCategoryDto>>;
}