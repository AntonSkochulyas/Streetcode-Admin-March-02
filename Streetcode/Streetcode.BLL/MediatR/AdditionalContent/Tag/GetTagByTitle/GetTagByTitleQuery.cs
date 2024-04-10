using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.AdditionalContent;

namespace Streetcode.BLL.MediatR.AdditionalContent.Tag.GetByStreetcodeId;

public record GetTagByTitleQuery(string Title) : IRequest<Result<TagDto>>;
