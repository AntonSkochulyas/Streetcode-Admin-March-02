// Necessary usings
using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.AdditionalContent.Subtitles;

// Necessary namespaces
namespace Streetcode.BLL.MediatR.AdditionalContent.GetById;

/// <summary>
/// Query, that request handler to get subtitle by given id from database.
/// </summary>
/// <param name="Id">
/// Id to find a subtitle.
/// </param>
public record GetSubtitleByIdQuery(int Id)
    : IRequest<Result<SubtitleDto>>;
