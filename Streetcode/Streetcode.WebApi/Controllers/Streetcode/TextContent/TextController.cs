using Microsoft.AspNetCore.Mvc;
using Streetcode.BLL.MediatR.Streetcode.Text.GetAll;
using Streetcode.BLL.MediatR.Streetcode.Text.GetById;
using Streetcode.BLL.MediatR.Streetcode.Text.GetByStreetcodeId;
using Streetcode.BLL.MediatR.Streetcode.Text.GetParsed;

namespace Streetcode.WebApi.Controllers.Streetcode.TextContent;

/// <summary>
/// Controller for managing text content.
/// </summary>
public class TextController : BaseApiController
{
    /// <summary>
    /// Get all text content.
    /// </summary>
    /// <returns>The list of all text content.</returns>
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return HandleResult(await Mediator.Send(new GetAllTextsQuery()));
    }

    /// <summary>
    /// Get text content by ID.
    /// </summary>
    /// <param name="id">The ID of the text content.</param>
    /// <returns>The text content with the specified ID.</returns>
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        return HandleResult(await Mediator.Send(new GetTextByIdQuery(id)));
    }

    /// <summary>
    /// Get text content by streetcode ID.
    /// </summary>
    /// <param name="streetcodeId">The streetcode ID.</param>
    /// <returns>The text content with the specified streetcode ID.</returns>
    [HttpGet("{streetcodeId:int}")]
    public async Task<IActionResult> GetByStreetcodeId([FromRoute] int streetcodeId)
    {
        return HandleResult(await Mediator.Send(new GetTextByStreetcodeIdQuery(streetcodeId)));
    }

    /// <summary>
    /// Get parsed text for admin preview.
    /// </summary>
    /// <param name="text">The text to be parsed.</param>
    /// <returns>The parsed text for admin preview.</returns>
    [HttpGet]
    public async Task<IActionResult> GetParsedText([FromQuery] string text)
    {
        return HandleResult(await Mediator.Send(new GetParsedTextForAdminPreviewCommand(text)));
    }
}
