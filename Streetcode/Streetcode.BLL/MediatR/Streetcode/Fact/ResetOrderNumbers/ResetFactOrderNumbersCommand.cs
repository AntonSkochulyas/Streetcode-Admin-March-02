using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.Streetcode.TextContent.Fact;

namespace Streetcode.BLL.MediatR.Fact.ResetOrderNumbers;

public record ResetFactOrderNumbersCommand(int StreetcodeId)
    : IRequest<Result<IEnumerable<FactDto>>>;