// Necessary usings.
using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.Sources;

// Necessary namespaces.
namespace Streetcode.BLL.MediatR.Sources.StreetcodeCategoryContent.Create
{
    /// <summary>
    /// Command, that requests a handler to create a new streetcode category content.
    /// </summary>
    /// <param name="newStreetcodeCategoryContent">
    /// New streetcode category content.
    /// </param>
    public record CreateStreetcodeCategoryContentCommand(StreetcodeCategoryContentDto StreetcodeCategoryContentDto) : IRequest<Result<CategoryContentCreatedDto>>;
}
