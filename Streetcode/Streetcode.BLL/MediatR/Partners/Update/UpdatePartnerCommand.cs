// Necessary usings.
using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.Partners;

// Necessary namespaces.
namespace Streetcode.BLL.MediatR.Partners.Update
{
    /// <summary>
    /// Query, that requests a handler to update partner.
    /// </summary>
    /// <param name="Partner">
    /// Updated partner.
    /// </param>
    public record UpdatePartnerCommand(CreatePartnerDto Partner)
        : IRequest<Result<PartnerDto>>;
}
