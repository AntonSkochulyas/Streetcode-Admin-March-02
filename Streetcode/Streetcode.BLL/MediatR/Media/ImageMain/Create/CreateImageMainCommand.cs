using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.Media.Images;

namespace Streetcode.BLL.MediatR.Media.ImageMain.Create;

public record CreateImageMainCommand(ImageFileBaseCreateDto Image)
    : IRequest<Result<ImageMainDto>>;
