using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.Media.Images;

namespace Streetcode.BLL.MediatR.Media.ImageMain.GetById;

public record GetImageMainByIdQuery(int Id) : IRequest<Result<ImageMainDto>>;
