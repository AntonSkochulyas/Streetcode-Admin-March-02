// Necessary usings.
using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.Sources;

// Necessary namespaces.
namespace Streetcode.BLL.MediatR.Sources.StreetcodeCategoryContent.Delete
{
    /// <summary>
    /// Query, that requests a handler to delete a streetcode category content by given id of source link category and streetcode.
    /// </summary>
    /// <param name="streetcodeId">
    /// Streetcode category content id to delete.
    /// </param>
    public record DeleteStreetcodeCategoryContentCommand(int sourceLinkCategoryId, int streetcodeId) : IRequest<Result<StreetcodeCategoryContentDto>>;
}
