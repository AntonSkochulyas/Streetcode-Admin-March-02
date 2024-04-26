using Microsoft.AspNetCore.Mvc;
using Streetcode.BLL.Dto.Media.Images;
using Streetcode.BLL.MediatR.Media.Image.GetAll;
using Streetcode.BLL.MediatR.Media.Image.GetBaseImage;
using Streetcode.BLL.MediatR.Media.Image.GetById;
using Streetcode.BLL.MediatR.Media.Image.GetByStreetcodeId;
using Streetcode.BLL.MediatR.Media.Image.Create;
using Streetcode.BLL.MediatR.Media.Image.Delete;

namespace Streetcode.WebApi.Controllers.Media.Images;

/// <summary>
/// Controller for managing image operations.
/// </summary>
public class ImageController : BaseApiController
{
    /// <summary>
    /// Get all images.
    /// </summary>
    /// <returns>The list of all images.</returns>
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return HandleResult(await Mediator.Send(new GetAllImagesQuery()));
    }

    /// <summary>
    /// Get images by streetcode ID.
    /// </summary>
    /// <param name="streetcodeId">The ID of the streetcode.</param>
    /// <returns>The list of images for the specified streetcode ID.</returns>
    [HttpGet("{streetcodeId:int}")]
    public async Task<IActionResult> GetByStreetcodeId([FromRoute] int streetcodeId)
    {
        return HandleResult(await Mediator.Send(new GetImageByStreetcodeIdQuery(streetcodeId)));
    }

    /// <summary>
    /// Get image by ID.
    /// </summary>
    /// <param name="id">The ID of the image.</param>
    /// <returns>The image with the specified ID.</returns>
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        return HandleResult(await Mediator.Send(new GetImageByIdQuery(id)));
    }

    /// <summary>
    /// Create a new image.
    /// </summary>
    /// <param name="image">The image data.</param>
    /// <returns>The created image.</returns>
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] ImageFileBaseCreateDto image)
    {
        return HandleResult(await Mediator.Send(new CreateImageCommand(image)));
    }

    /// <summary>
    /// Delete an image by ID.
    /// </summary>
    /// <param name="id">The ID of the image to delete.</param>
    /// <returns>The result of the delete operation.</returns>
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        return HandleResult(await Mediator.Send(new DeleteImageCommand(id)));
    }

    /// <summary>
    /// Get the base image by ID.
    /// </summary>
    /// <param name="id">The ID of the base image.</param>
    /// <returns>The base image with the specified ID.</returns>
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetBaseImage([FromRoute] int id)
    {
        return HandleResult(await Mediator.Send(new GetBaseImageQuery(id)));
    }
}
