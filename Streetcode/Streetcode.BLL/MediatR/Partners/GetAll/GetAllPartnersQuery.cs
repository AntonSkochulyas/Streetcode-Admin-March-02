// Necessary usings.
using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.Partners;

// Necessary namespaces.
namespace Streetcode.BLL.MediatR.Partners.GetAll;

/// <summary>
/// Query, that requests a handler to get all partners from database.
/// </summary>
public record GetAllPartnersQuery : IRequest<Result<IEnumerable<PartnerDto>>>;
