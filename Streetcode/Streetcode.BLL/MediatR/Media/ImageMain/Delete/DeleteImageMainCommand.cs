using FluentResults;
using MediatR;

namespace Streetcode.BLL.MediatR.Media.ImageMain.Delete;

public record DeleteImageMainCommand(int Id) : IRequest<Result<Unit>>;
