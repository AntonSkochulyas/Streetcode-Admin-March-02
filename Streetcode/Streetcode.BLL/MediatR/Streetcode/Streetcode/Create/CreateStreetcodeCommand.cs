using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.Streetcode;

namespace Streetcode.BLL.MediatR.Streetcode.Streetcode.Create;

public record CreateStreetcodeCommand(BaseStreetcodeDto Streetcode) : IRequest<Result<StreetcodeDto>>
{
}
