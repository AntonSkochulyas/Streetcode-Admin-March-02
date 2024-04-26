using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Streetcode.BLL.Dto.Team;
using Streetcode.BLL.MediatR.Team.TeamMembersLinks.Create;
using Streetcode.BLL.MediatR.Team.TeamMembersLinks.GetAll;

namespace Streetcode.WebApi.Controllers.Team
{
    /// <summary>
    /// Controller for managing team links.
    /// </summary>
    public class TeamLinkController : BaseApiController
    {
        /// <summary>
        /// Get all team links.
        /// </summary>
        /// <returns>A list of team links.</returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return HandleResult(await Mediator.Send(new GetAllTeamLinkQuery()));
        }

        /// <summary>
        /// Create a new team link.
        /// </summary>
        /// <param name="teamMemberLink">The team member link to create.</param>
        /// <returns>The created team link.</returns>
        [Authorize("Admin")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TeamMemberLinkDto teamMemberLink)
        {
            return HandleResult(await Mediator.Send(new CreateTeamLinkQuery(teamMemberLink)));
        }
    }
}
