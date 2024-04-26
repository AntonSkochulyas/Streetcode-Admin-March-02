using Microsoft.AspNetCore.Mvc;
using Streetcode.BLL.MediatR.Streetcode.Term.GetAll;
using Streetcode.BLL.MediatR.Streetcode.Term.GetById;

namespace Streetcode.WebApi.Controllers.Streetcode.TextContent;

/// <summary>
/// Controller for managing terms.
/// </summary>
public class TermController : BaseApiController
{
    /// <summary>
    /// Gets all terms.
    /// </summary>
    /// <returns>A collection of terms.</returns>
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return HandleResult(await Mediator.Send(new GetAllTermsQuery()));
    }

    /// <summary>
    /// Gets a term by its ID.
    /// </summary>
    /// <param name="id">The ID of the term.</param>
    /// <returns>The term with the specified ID.</returns>
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        return HandleResult(await Mediator.Send(new GetTermByIdQuery(id)));
    }
}
