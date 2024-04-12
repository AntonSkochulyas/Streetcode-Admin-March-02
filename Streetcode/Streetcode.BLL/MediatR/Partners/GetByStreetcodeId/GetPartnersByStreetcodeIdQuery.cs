// Necessary usings.
using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.Partners;

// Necessary namespaces.
namespace Streetcode.BLL.MediatR.Partners.GetByStreetcodeId;

/// <summary>
/// Query, that requests a handler to get a partners by given streetcode id.
/// </summary>
/// <param name="StreetcodeId">
/// Partnerts id to get.
/// </param>
public record GetPartnersByStreetcodeIdQuery(int StreetcodeId)
    : IRequest<Result<IEnumerable<PartnerDto>>>;
