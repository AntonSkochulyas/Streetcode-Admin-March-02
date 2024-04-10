// Necessary usings.
using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.Partners;

// Necessary namespaces.
namespace Streetcode.BLL.MediatR.Partners.GetAllPartnerShort
{
    /// <summary>
    /// Query, that requests handler to get all partners short from database.
    /// </summary>
    public record GetAllPartnersShortQuery : IRequest<Result<IEnumerable<PartnerShortDto>>>;
}
