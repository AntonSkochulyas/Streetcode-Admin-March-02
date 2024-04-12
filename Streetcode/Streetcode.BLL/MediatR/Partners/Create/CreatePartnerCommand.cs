// Necessary usings.
using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.Partners;

// Necessary namespaces.
namespace Streetcode.BLL.MediatR.Partners.Create
{
    /// <summary>
    /// Command, that requests a handler to create a new partner.
    /// </summary>
    /// <param name="newPartner">
    /// New partner.
    /// </param>
    public record CreatePartnerCommand(CreatePartnerDto NewPartner)
        : IRequest<Result<PartnerDto>>;
}
