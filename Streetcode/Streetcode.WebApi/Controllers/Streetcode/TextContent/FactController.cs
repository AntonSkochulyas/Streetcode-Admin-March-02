using Microsoft.AspNetCore.Mvc;
using Streetcode.BLL.Dto.Streetcode.TextContent.Fact;
using Streetcode.BLL.MediatR.Fact.ResetOrderNumbers;
using Streetcode.BLL.MediatR.Fact.Update;
using Streetcode.BLL.MediatR.Streetcode.Fact.Create;
using Streetcode.BLL.MediatR.Streetcode.Fact.Delete;
using Streetcode.BLL.MediatR.Streetcode.Fact.GetAll;
using Streetcode.BLL.MediatR.Streetcode.Fact.GetById;
using Streetcode.BLL.MediatR.Streetcode.Fact.GetByStreetcodeId;

namespace Streetcode.WebApi.Controllers.Streetcode.TextContent;

/// <summary>
/// Controller for managing facts.
/// </summary>
public class FactController : BaseApiController
{
    /// <summary>
    /// Get all facts.
    /// </summary>
    /// <returns>The list of facts.</returns>
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return HandleResult(await Mediator.Send(new GetAllFactsQuery()));
    }

    /// <summary>
    /// Get a fact by its ID.
    /// </summary>
    /// <param name="id">The ID of the fact.</param>
    /// <returns>The fact with the specified ID.</returns>
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        return HandleResult(await Mediator.Send(new GetFactByIdQuery(id)));
    }

    /// <summary>
    /// Get facts by streetcode ID.
    /// </summary>
    /// <param name="streetcodeId">The ID of the streetcode.</param>
    /// <returns>The list of facts associated with the specified streetcode ID.</returns>
    [HttpGet("{streetcodeId:int}")]
    public async Task<IActionResult> GetByStreetcodeId([FromRoute] int streetcodeId)
    {
        return HandleResult(await Mediator.Send(new GetFactByStreetcodeIdQuery(streetcodeId)));
    }

    /// <summary>
    /// Create a new fact.
    /// </summary>
    /// <param name="position">The fact data.</param>
    /// <returns>The created fact.</returns>
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] FactBaseDto position)
    {
        return HandleResult(await Mediator.Send(new CreateFactCommand(position)));
    }

    /// <summary>
    /// Update an existing fact.
    /// </summary>
    /// <param name="fact">The updated fact data.</param>
    /// <returns>The updated fact.</returns>
    [HttpPut]
    public async Task<IActionResult> Update([FromBody] FactDto fact)
    {
        return HandleResult(await Mediator.Send(new UpdateFactCommand(fact)));
    }

    /// <summary>
    /// Reset the order numbers of facts for a specific streetcode.
    /// </summary>
    /// <param name="streetcodeId">The ID of the streetcode.</param>
    /// <returns>The updated facts with reset order numbers.</returns>
    [HttpPut("{streetcodeId:int}")]
    public async Task<IActionResult> UpdateOrderNumbers([FromRoute] int streetcodeId)
    {
        return HandleResult(await Mediator.Send(new ResetFactOrderNumbersCommand(streetcodeId)));
    }

    /// <summary>
    /// Delete a fact by its ID.
    /// </summary>
    /// <param name="id">The ID of the fact.</param>
    /// <returns>The result of the deletion operation.</returns>
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        return HandleResult(await Mediator.Send(new DeleteFactCommand(id)));
    }
}
