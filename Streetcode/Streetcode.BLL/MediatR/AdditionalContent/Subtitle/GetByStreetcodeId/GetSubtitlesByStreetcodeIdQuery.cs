// Necessary usings.
using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.AdditionalContent.Subtitles;

// Necessary namespaces
namespace Streetcode.BLL.MediatR.AdditionalContent.Subtitle.GetByStreetcodeId
{
    /// <summary>
    /// Query, that request handler to get subtitle by streetcode id.
    /// </summary>
    /// <param name="StreetcodeId">
    /// Param to finding a model.
    /// </param>
    public record GetSubtitlesByStreetcodeIdQuery(int StreetcodeId) : IRequest<Result<SubtitleDto>>;
}
