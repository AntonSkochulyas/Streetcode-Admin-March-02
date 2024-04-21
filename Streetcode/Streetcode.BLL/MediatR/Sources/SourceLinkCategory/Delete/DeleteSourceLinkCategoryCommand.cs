// Necessary usings.
using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.Sources;

// Necessary namespaces.
namespace Streetcode.BLL.MediatR.Sources.SourceLinkCategory.Delete
{
    /// <summary>
    /// Query, that requests a handler to delete a source link category by given id.
    /// </summary>
    /// <param name="id">
    /// source link category id to delete.
    /// </param>
    public record DeleteSourceLinkCategoryCommand(int Id)
        : IRequest<Result<SourceLinkResponseDto>>;
}
