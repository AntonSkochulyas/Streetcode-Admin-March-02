using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Streetcode.BLL.Dto.Media.Audio;
using Streetcode.BLL.MediatR.Media.Audio.Create;
using Streetcode.BLL.MediatR.Media.Audio.Delete;
using Streetcode.BLL.MediatR.Media.Audio.GetAll;
using Streetcode.BLL.MediatR.Media.Audio.GetBaseAudio;
using Streetcode.BLL.MediatR.Media.Audio.GetById;
using Streetcode.BLL.MediatR.Media.Audio.GetByStreetcodeId;

namespace Streetcode.WebApi.Controllers.Media;

/// <summary>
/// Controller for managing audio files.
/// </summary>
public class AudioController : BaseApiController
{
    /// <summary>
    /// Get all audio files.
    /// </summary>
    /// <returns>The list of all audio files.</returns>
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return HandleResult(await Mediator.Send(new GetAllAudiosQuery()));
    }

    /// <summary>
    /// Get audio files by streetcode ID.
    /// </summary>
    /// <param name="streetcodeId">The ID of the streetcode.</param>
    /// <returns>The list of audio files for the specified streetcode ID.</returns>
    [HttpGet("{streetcodeId:int}")]
    public async Task<IActionResult> GetByStreetcodeId([FromRoute] int streetcodeId)
    {
        return HandleResult(await Mediator.Send(new GetAudioByStreetcodeIdQuery(streetcodeId)));
    }

    /// <summary>
    /// Get audio file by ID.
    /// </summary>
    /// <param name="id">The ID of the audio file.</param>
    /// <returns>The audio file with the specified ID.</returns>
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        return HandleResult(await Mediator.Send(new GetAudioByIdQuery(id)));
    }

    /// <summary>
    /// Get base audio file by ID.
    /// </summary>
    /// <param name="id">The ID of the audio file.</param>
    /// <returns>The base audio file with the specified ID.</returns>
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetBaseAudio([FromRoute] int id)
    {
        return HandleResult(await Mediator.Send(new GetBaseAudioQuery(id)));
    }

    /// <summary>
    /// Create a new audio file.
    /// </summary>
    /// <param name="audio">The audio file to create.</param>
    /// <returns>The created audio file.</returns>
    [Authorize("Admin")]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] AudioFileBaseCreateDto audio)
    {
        return HandleResult(await Mediator.Send(new CreateAudioCommand(audio)));
    }

    /// <summary>
    /// Delete an audio file by ID.
    /// </summary>
    /// <param name="id">The ID of the audio file to delete.</param>
    /// <returns>The result of the delete operation.</returns>
    [Authorize("Admin")]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        return HandleResult(await Mediator.Send(new DeleteAudioCommand(id)));
    }
}
