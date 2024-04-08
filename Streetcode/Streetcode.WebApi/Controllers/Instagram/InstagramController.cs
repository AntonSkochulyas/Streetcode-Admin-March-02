using Microsoft.AspNetCore.Mvc;
using Streetcode.BLL.MediatR.Instagram.GetAll;

namespace Streetcode.WebApi.Controllers.Instagram;

/// <summary>
/// Represents a controller for managing Instagram posts.
/// </summary>
public class InstagramController : BaseApiController
{
    /// <summary>
    /// Retrieves all Instagram posts.
    /// </summary>
    /// <returns>The list of Instagram posts.</returns>
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return HandleResult(await Mediator.Send(new GetAllPostsQuery()));
    }
}
