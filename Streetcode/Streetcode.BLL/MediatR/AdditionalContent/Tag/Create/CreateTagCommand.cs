using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.AdditionalContent;
using Streetcode.BLL.Dto.AdditionalContent.Tag;

namespace Streetcode.BLL.MediatR.AdditionalContent.Tag.Create
{
  public record CreateTagCommand(CreateTagDto Tag)
        : IRequest<Result<TagDto>>;
}
