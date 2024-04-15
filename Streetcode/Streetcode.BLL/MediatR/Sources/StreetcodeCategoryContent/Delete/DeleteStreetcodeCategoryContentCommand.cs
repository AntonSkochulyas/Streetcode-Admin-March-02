using FluentResults;
using MediatR;

namespace Streetcode.BLL.MediatR.Sources.StreetcodeCategoryContent.Delete
{
    public record DeleteStreetcodeCategoryContentCommand(int sourceLinkCategoryId, int streetcodeId) : IRequest<Result<Unit>>;
}
