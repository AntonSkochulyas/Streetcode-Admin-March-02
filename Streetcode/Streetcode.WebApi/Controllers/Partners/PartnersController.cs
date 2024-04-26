using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Streetcode.BLL.Dto.Partners;
using Streetcode.BLL.MediatR.Partners.Create;
using Streetcode.BLL.MediatR.Partners.GetAll;
using Streetcode.BLL.MediatR.Partners.GetAllPartnerShort;
using Streetcode.BLL.MediatR.Partners.GetById;
using Streetcode.BLL.MediatR.Partners.GetByStreetcodeId;

namespace Streetcode.WebApi.Controllers.Partners;

/// <summary>
/// Controller for managing partners.
/// </summary>
public class PartnersController : BaseApiController
{
    /// <summary>
    /// Get all partners.
    /// </summary>
    /// <returns>The list of partners.</returns>
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return HandleResult(await Mediator.Send(new GetAllPartnersQuery()));
    }

    /// <summary>
    /// Get all partners in short format.
    /// </summary>
    /// <returns>The list of partners in short format.</returns>
    [HttpGet]
    public async Task<IActionResult> GetAllShort()
    {
        return HandleResult(await Mediator.Send(new GetAllPartnersShortQuery()));
    }

    /// <summary>
    /// Get a partner by ID.
    /// </summary>
    /// <param name="id">The ID of the partner.</param>
    /// <returns>The partner with the specified ID.</returns>
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        return HandleResult(await Mediator.Send(new GetPartnerByIdQuery(id)));
    }

    /// <summary>
    /// Get partners by streetcode ID.
    /// </summary>
    /// <param name="streetcodeId">The streetcode ID.</param>
    /// <returns>The list of partners with the specified streetcode ID.</returns>
    [HttpGet("{streetcodeId:int}")]
    public async Task<IActionResult> GetByStreetcodeId([FromRoute] int streetcodeId)
    {
        return HandleResult(await Mediator.Send(new GetPartnersByStreetcodeIdQuery(streetcodeId)));
    }

    /// <summary>
    /// Create a new partner.
    /// </summary>
    /// <param name="partner">The partner to create.</param>
    /// <returns>The created partner.</returns>
    [Authorize("Admin")]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreatePartnerDto partner)
    {
        return HandleResult(await Mediator.Send(new CreatePartnerCommand(partner)));
    }

    /// <summary>
    /// Update a partner.
    /// </summary>
    /// <param name="partner">The partner to update.</param>
    /// <returns>The updated partner.</returns>
    [Authorize("Admin")]
    [HttpPut]
    public async Task<IActionResult> Update([FromBody] CreatePartnerDto partner)
    {
        return HandleResult(await Mediator.Send(new BLL.MediatR.Partners.Update.UpdatePartnerQuery(partner)));
    }

    /// <summary>
    /// Delete a partner by ID.
    /// </summary>
    /// <param name="id">The ID of the partner to delete.</param>
    /// <returns>The result of the deletion operation.</returns>
    [Authorize("Admin")]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        return HandleResult(await Mediator.Send(new BLL.MediatR.Partners.Delete.DeletePartnerQuery(id)));
    }
}
