using FluentResults;
using MediatR;

namespace Streetcode.BLL.MediatR.Media.ImageMain.GetBaseImage;

public record GetBaseImageMainQuery(int Id) : IRequest<Result<MemoryStream>>;