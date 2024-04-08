using Microsoft.AspNetCore.Mvc;
using Streetcode.BLL.Dto.Locations;

namespace Streetcode.WebApi.Controllers.Locations
{
    /// <summary>
    /// Controller for managing locations.
    /// </summary>
    public class LocationController : BaseApiController
    {
        /// <summary>
        /// Creates a new location.
        /// </summary>
        /// <param name="location">The location data.</param>
        /// <returns>The created location.</returns>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] LocationDto location)
        {
            return HandleResult(await Mediator.Send(new BLL.MediatR.Locations.Create.CreateLocationCommand(location)));
        }

        /// <summary>
        /// Deletes a location by ID.
        /// </summary>
        /// <param name="id">The ID of the location to delete.</param>
        /// <returns>The result of the operation.</returns>
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            return HandleResult(await Mediator.Send(new BLL.MediatR.Locations.Delete.DeleteLocationCommand(id)));
        }
    }
}
