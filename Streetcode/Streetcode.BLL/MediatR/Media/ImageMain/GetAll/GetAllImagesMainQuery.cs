using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.Media.Images;

namespace Streetcode.BLL.MediatR.Media.ImageMain.GetAll;

public record GetAllImagesMainQuery : IRequest<Result<IEnumerable<ImageMainDto>>>;