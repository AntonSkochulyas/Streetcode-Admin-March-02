using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.Streetcode;

namespace Streetcode.BLL.MediatR.Streetcode.Streetcode.Delete;

public record DeleteStreetcodeCommand(int Id) : IRequest<Result<StreetcodeDto>>;
