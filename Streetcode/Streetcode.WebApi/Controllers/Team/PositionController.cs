using Microsoft.AspNetCore.Mvc;
using Streetcode.BLL.Dto.Team;
using Streetcode.BLL.MediatR.Team.Create;
using Streetcode.BLL.MediatR.Team.Position.GetAll;

namespace Streetcode.WebApi.Controllers.Team
{
    /// <summary>
    /// Controller for managing positions within a team.
    /// </summary>
    public class PositionController : BaseApiController
    {
        /// <summary>
        /// Retrieves all positions.
        /// </summary>
        /// <returns>A collection of all positions.</returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return HandleResult(await Mediator.Send(new GetAllPositionsQuery()));
        }

        /// <summary>
        /// Creates a new position.
        /// </summary>
        /// <param name="position">The position to create.</param>
        /// <returns>The created position.</returns>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PositionDto position)
        {
            return HandleResult(await Mediator.Send(new CreatePositionQuery(position)));
        }
    }
}
