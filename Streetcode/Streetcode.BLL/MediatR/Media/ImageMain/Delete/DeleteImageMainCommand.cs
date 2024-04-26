using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.Media.Images;

namespace Streetcode.BLL.MediatR.Media.ImageMain.Delete;

public record DeleteImageMainCommand(int Id)
    : IRequest<Result<ImageMainDto>>;
