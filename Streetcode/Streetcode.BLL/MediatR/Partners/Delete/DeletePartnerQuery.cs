// Necessary usings.
using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.Partners;

// Necessary namespaces.
namespace Streetcode.BLL.MediatR.Partners.Delete
{
    /// <summary>
    /// Query, that requests a handler to delete a partner by given id.
    /// </summary>
    /// <param name="id">
    /// Partner id to delete.
    /// </param>
    public record DeletePartnerQuery(int id) : IRequest<Result<PartnerDto>>;
}
