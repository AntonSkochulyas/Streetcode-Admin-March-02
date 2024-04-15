// Necessary usings.
using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.AdditionalContent.Tag;

// Necessary namespaces.
namespace Streetcode.BLL.MediatR.AdditionalContent.Tag.GetByStreetcodeId;

/// <summary>
/// Query, that requests a handler to find a tags by streetcode id.
/// </summary>
/// <param name="StreetcodeId">
/// Id to find a tags.
/// </param>
public record GetTagByStreetcodeIdQuery(int StreetcodeId)
    : IRequest<Result<IEnumerable<StreetcodeTagDto>>>;
