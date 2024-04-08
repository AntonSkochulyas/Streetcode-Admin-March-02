using Microsoft.AspNetCore.Mvc;
using Streetcode.BLL.Dto.Toponyms;
using Streetcode.BLL.MediatR.Toponyms.GetAll;
using Streetcode.BLL.MediatR.Toponyms.GetById;
using Streetcode.BLL.MediatR.Toponyms.GetByStreetcodeId;

namespace Streetcode.WebApi.Controllers.Toponyms;

/// <summary>
/// Controller for handling requests related to Toponyms.
/// </summary>
public class ToponymController : BaseApiController
{
    /// <summary>
    /// Retrieves all Toponyms.
    /// </summary>
    /// <param name="request">The request parameters.</param>
    /// <returns>The result of the operation.</returns>
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] GetAllToponymsRequestDto request)
    {
        return HandleResult(await Mediator.Send(new GetAllToponymsQuery(request)));
    }

    /// <summary>
    /// Retrieves a Toponym by its ID.
    /// </summary>
    /// <param name="id">The ID of the Toponym.</param>
    /// <returns>The retrieved toponym.</returns>
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        return HandleResult(await Mediator.Send(new GetToponymByIdQuery(id)));
    }

    /// <summary>
    /// Retrieves Toponyms by Streetcode ID.
    /// </summary>
    /// <param name="streetcodeId">The Streetcode ID.</param>
    /// <returns>The retrieved toponym with specified Streetcode ID.</returns>
    [HttpGet("{streetcodeId:int}")]
    public async Task<IActionResult> GetByStreetcodeId([FromRoute] int streetcodeId)
    {
        return HandleResult(await Mediator.Send(new GetToponymsByStreetcodeIdQuery(streetcodeId)));
    }
}
