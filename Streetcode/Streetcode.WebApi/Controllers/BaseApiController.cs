using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Streetcode.BLL.MediatR.ResultVariations;

namespace Streetcode.WebApi.Controllers;

/// <summary>
/// Base API controller for handling common functionality and dependencies.
/// </summary>
[ApiController]
[Route("api/[controller]/[action]")]
public class BaseApiController : ControllerBase
{
    private IMediator? _mediator;

    /// <summary>
    /// Gets the mediator instance for handling MediatR requests.
    /// </summary>
    /// <value>
    /// The mediator instance for handling MediatR requests.
    /// </value>
    protected IMediator Mediator => _mediator ??=
        HttpContext.RequestServices.GetService<IMediator>()!;

    /// <summary>
    /// Handles the result of a MediatR request and returns the appropriate ActionResult.
    /// </summary>
    /// <typeparam name="T">The type of the result value.</typeparam>
    /// <param name="result">The result of the MediatR request.</param>
    /// <returns>The ActionResult based on the result.</returns>
    protected ActionResult HandleResult<T>(Result<T> result)
    {
        if (result.IsSuccess)
        {
            if (result is NullResult<T>)
            {
                return Ok(result.Value);
            }

            return (result.Value is null) ?
                NotFound("Found result matching null") : Ok(result.Value);
        }

        return BadRequest(result.Reasons);
    }
}
