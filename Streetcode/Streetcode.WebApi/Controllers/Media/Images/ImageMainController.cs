using Microsoft.AspNetCore.Mvc;
using Streetcode.BLL.Dto.Media.Images;
using Streetcode.BLL.MediatR.Media.ImageMain.Create;
using Streetcode.BLL.MediatR.Media.ImageMain.Delete;
using Streetcode.BLL.MediatR.Media.ImageMain.GetAll;
using Streetcode.BLL.MediatR.Media.ImageMain.GetBaseImage;
using Streetcode.BLL.MediatR.Media.ImageMain.GetById;

namespace Streetcode.WebApi.Controllers.Media.Images;

/// <summary>
/// Controller for managing main image operations.
/// </summary>
public class ImageMainController : BaseApiController
{
    /// <summary>
    /// Get all main images.
    /// </summary>
    /// <returns>The list of all images.</returns>
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return HandleResult(await Mediator.Send(new GetAllImagesMainQuery()));
    }

    /// <summary>
    /// Get main image by ID.
    /// </summary>
    /// <param name="id">The ID of the image.</param>
    /// <returns>The image with the specified ID.</returns>
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        return HandleResult(await Mediator.Send(new GetImageMainByIdQuery(id)));
    }

    /// <summary>
    /// Create a new main image.
    /// </summary>
    /// <param name="image">The image data.</param>
    /// <returns>The created image.</returns>
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] ImageFileBaseCreateDto image)
    {
        return HandleResult(await Mediator.Send(new CreateImageMainCommand(image)));
    }

    /// <summary>
    /// Delete an image by ID.
    /// </summary>
    /// <param name="id">The ID of the image to delete.</param>
    /// <returns>The result of the delete operation.</returns>
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        return HandleResult(await Mediator.Send(new DeleteImageMainCommand(id)));
    }

    /// <summary>
    /// Get the base image by ID.
    /// </summary>
    /// <param name="id">The ID of the base image.</param>
    /// <returns>The base image with the specified ID.</returns>
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetBaseImage([FromRoute] int id)
    {
        return HandleResult(await Mediator.Send(new GetBaseImageMainQuery(id)));
    }
}
