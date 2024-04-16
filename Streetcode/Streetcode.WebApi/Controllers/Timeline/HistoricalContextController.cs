using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Streetcode.BLL.Dto.Timeline;
using Streetcode.BLL.MediatR.Timeline.HistoricalContext.Create;
using Streetcode.BLL.MediatR.Timeline.HistoricalContext.GetAll;

namespace Streetcode.WebApi.Controllers.Timeline
{
    /// <summary>
    /// Controller for managing historical context data.
    /// </summary>
    public class HistoricalContextController : BaseApiController
    {
        /// <summary>
        /// Retrieves all historical context data.
        /// </summary>
        /// <returns>All historical context data.</returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return HandleResult(await Mediator.Send(new GetAllHistoricalContextQuery()));
        }

        /// <summary>
        /// Creates a new historical context.
        /// </summary>
        /// <param name="historicalContext">The historical context to create.</param>
        /// <returns>The created historical context.</returns>
        [Authorize("Admin")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] HistoricalContextDto historicalContext)
        {
            return HandleResult(await Mediator.Send(new CreateHistoricalContextCommand(historicalContext)));
        }
    }
}
