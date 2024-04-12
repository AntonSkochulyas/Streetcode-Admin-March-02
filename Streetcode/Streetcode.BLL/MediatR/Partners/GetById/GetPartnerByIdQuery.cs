// Necessary usings.
using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.Partners;

// Necessary namespaces.
namespace Streetcode.BLL.MediatR.Partners.GetById;

/// <summary>
/// Query, that requests a handler to get a partner by given id.
/// </summary>
/// <param name="Id">
/// Partner id to get.
/// </param>
public record GetPartnerByIdQuery(int Id)
    : IRequest<Result<PartnerDto>>;
