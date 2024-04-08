using Microsoft.AspNetCore.Mvc;
using Streetcode.BLL.MediatR.Transactions.TransactionLink.GetAll;
using Streetcode.BLL.MediatR.Transactions.TransactionLink.GetById;
using Streetcode.BLL.MediatR.Transactions.TransactionLink.GetByStreetcodeId;

namespace Streetcode.WebApi.Controllers.Transactions;

/// <summary>
/// Controller for handling transaction links.
/// </summary>
public class TransactLinksController : BaseApiController
{
    /// <summary>
    /// Gets all transaction links.
    /// </summary>
    /// <returns>The list of transaction links.</returns>
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return HandleResult(await Mediator.Send(new GetAllTransactLinksQuery()));
    }

    /// <summary>
    /// Gets a transaction link by streetcode ID.
    /// </summary>
    /// <param name="streetcodeId">The streetcode ID.</param>
    /// <returns>The transaction link.</returns>
    [HttpGet("{streetcodeId:int}")]
    public async Task<IActionResult> GetByStreetcodeId([FromRoute] int streetcodeId)
    {
        var res = await Mediator.Send(new GetTransactLinkByStreetcodeIdQuery(streetcodeId));
        return HandleResult(res);
    }

    /// <summary>
    /// Gets a transaction link by ID.
    /// </summary>
    /// <param name="id">The ID.</param>
    /// <returns>The transaction link.</returns>
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        return HandleResult(await Mediator.Send(new GetTransactLinkByIdQuery(id)));
    }
}
