using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.Streetcode.TextContent.Fact;

namespace Streetcode.BLL.MediatR.Streetcode.Fact.Delete;

public record DeleteFactCommand(int Id)
    : IRequest<Result<FactDto>>;