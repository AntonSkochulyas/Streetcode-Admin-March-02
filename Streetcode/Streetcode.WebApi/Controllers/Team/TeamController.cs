using Microsoft.AspNetCore.Mvc;
using Streetcode.BLL.MediatR.Team.GetAll;
using Streetcode.BLL.MediatR.Team.GetById;

namespace Streetcode.WebApi.Controllers.Team
{
    /// <summary>
    /// Controller for managing teams.
    /// </summary>
    public class TeamController : BaseApiController
    {
        /// <summary>
        /// Gets all teams.
        /// </summary>
        /// <returns>The list of all teams.</returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return HandleResult(await Mediator.Send(new GetAllTeamQuery()));
        }

        /// <summary>
        /// Gets all main teams.
        /// </summary>
        /// <returns>The list of all main teams.</returns>
        [HttpGet]
        public async Task<IActionResult> GetAllMain()
        {
            return HandleResult(await Mediator.Send(new GetAllMainTeamQuery()));
        }

        /// <summary>
        /// Gets a team by its ID.
        /// </summary>
        /// <param name="id">The ID of the team.</param>
        /// <returns>The team with the specified ID.</returns>
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            return HandleResult(await Mediator.Send(new GetByIdTeamQuery(id)));
        }
    }
}
