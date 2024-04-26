// Necessary usings.
using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.Streetcode;

// Necessary namespaces.
namespace Streetcode.BLL.MediatR.Streetcode.Streetcode.Delete
{
    /// <summary>
    /// Query, that requests a handler to delete a streetcode by given id.
    /// </summary>
    /// <param name="id">
    /// Streetcode id to delete.
    /// </param>
    public record DeleteStreetcodeCommand(int Id) : IRequest<Result<StreetcodeDto>>;
}
