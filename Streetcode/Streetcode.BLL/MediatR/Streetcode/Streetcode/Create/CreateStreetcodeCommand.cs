// Necessary usings.
using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.Streetcode;

// Necessary namespaces.
namespace Streetcode.BLL.MediatR.Streetcode.Streetcode.Create
{
    /// <summary>
    /// Command, that requests a handler to create a new streetcode.
    /// </summary>
    /// <param name="newStreetcode">
    /// New streetcode.
    /// </param>
    public record CreateStreetcodeCommand(BaseStreetcodeDto Streetcode) : IRequest<Result<StreetcodeDto>>
    {
    }
}
