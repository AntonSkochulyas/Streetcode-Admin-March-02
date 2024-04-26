// Necessary usings.
using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.AdditionalContent.Subtitles;

// Necessary namespaces.
namespace Streetcode.BLL.MediatR.AdditionalContent.Subtitle.GetAll;

/// <summary>
/// Query, that request handler to returns all of subtitles in database.
/// </summary>
public record GetAllSubtitlesQuery : IRequest<Result<IEnumerable<SubtitleDto>>>;